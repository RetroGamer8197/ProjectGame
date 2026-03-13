using System.Reflection.Metadata.Ecma335;
using OpenTK.Graphics.ES20;
using OpenTK.Mathematics;

namespace Game1
{
    public class HUD
    {
        public Dictionary<string, HUD_Element> HUD_Elements;
        public float[] HUD_GL, TextElementGL;

        public HUD()
        {
            HUD_Elements = [];
            HUD_GL = [];
            TextElementGL = [];
        }
        public void AddHUD_Element(string name, HUD_Element element)
        {
            HUD_Elements.Add(name, element);
        }

        public void SyncHUD_GL(float aspectRatio)
        {
            GenerateGL_Data(aspectRatio);
        }

        public void UpdateElements(ref Player player, float deltaTime)
        {
            // both the pause menu and HUD use this class, but this function will only work if all the following objects are present
            string[] dependencies = ["weaponicon", "ammotext", "healthbar", "ammoindicator", "armorbar", "messagebox"];
            bool allDependenciesPresent = true;

            foreach (string dependency in dependencies)
            {
                if (!HUD_Elements.ContainsKey(dependency))
                {
                    allDependenciesPresent = false;
                }
            }

            // if all the objects needed are present, we know this is the HUD
            if (allDependenciesPresent)
            {
                HUD_Elements["weaponicon"].UpdateValue(player.weaponIndex);
                HUD_Elements["ammotext"].QueueValue(player.GetCurrentWeaponUsageString());

                HUD_Elements["healthbar"].UpdateValue(player.Health);
                HUD_Elements["ammoindicator"].UpdateValue(player.GetCurrentWeaponMagUsage());
                HUD_Elements["armorbar"].UpdateValue(player.Armor);
                HUD_Elements["messagebox"].UpdateValue((float)Math.Clamp(deltaTime, 0.0f, 0.1f));

                foreach (HUD_Element element in HUD_Elements.Values)
                {
                    element.CheckIfEnabled(player.Inventory);
                }
            }
        }

        public void LevelReset()
        {
            HUD_Elements["messagebox"].UpdateValue(false);
        }

        public void CallNewLevel()
        {
            HUD_Elements["messagebox"].UpdateValue(true);
        }

        public void QueueMessage(string message)
        {
            HUD_Elements["messagebox"].QueueValue(message);
        }

        private void GenerateGL_Data(float aspectRatio)
        {
            List<float> HUD_GL_Data = [], Text_GL_Data = [];

            foreach (HUD_Element element in HUD_Elements.Values)
            {
                if (element.elementType != HUD_Element.HUD_ElementType.Text)
                {
                    HUD_GL_Data.AddRange(element.GenerateGL_Data(aspectRatio));
                }
                else
                {
                    Text_GL_Data.AddRange(element.GenerateGL_Data(aspectRatio));
                }
            }

            HUD_GL = [.. HUD_GL_Data];
            TextElementGL = [.. Text_GL_Data];
        }
    }

    public class HUD_Element(Vector2 centre, Vector2 scale, bool doesAspectRatioAffect, HUD_Element.HUD_ElementType thisElementType, bool isEnabledStart)
    {
        protected Vector2 centreCoord = centre;
        protected Vector2 widthHeightScale = scale;
        protected bool aspectRatioAffect = doesAspectRatioAffect;
        public HUD_ElementType elementType = thisElementType;
        public bool enabled = isEnabledStart;
        public enum HUD_ElementType
        {
            Crosshair, HealthBar, Menu, Icon, Text
        }

        public virtual float[] GenerateGL_Data(float aspectRatio)
        {
            return [];
        }

        public virtual void CheckIfEnabled(List<HeldItem> inventory)
        {

        }

        public virtual void UpdateValue(float input)
        {

        }

        public virtual void UpdateValue(bool input)
        {

        }

        public virtual bool CheckBoolValue()
        {
            return false;
        }

        public virtual void QueueValue(string input)
        {

        }

        public virtual void DecreaseValue(float value)
        {

        }

        public virtual void IncreaseValue(float value)
        {

        }
    }

    public class Crosshair() : HUD_Element((0, 0), (1, 1), false, HUD_ElementType.Crosshair, true)
    {

        public override float[] GenerateGL_Data(float aspectRatio)
        {
            List<float> gl_data = [];

            Plane p1 = new((0, 0, 0), (0, 0, 1), 0.0078125f, 0.0625f, 1, 1.0f);

            Plane p2 = new((0, 0, 0), (0, 0, 1), 0.0625f, 0.0078125f, 1, 1.0f);
            
            gl_data.AddRange(p1.GenerateFloatData((0,0,0)));
            gl_data.AddRange(p2.GenerateFloatData((0,0,0)));

            return [.. gl_data];
        }
    }

    public class HealthBar(float maxValueIn, Vector3 centreIn, int textureIndexIn) : HUD_Element((0, 0), (1, 1), true, HUD_ElementType.HealthBar, true)
    {
        float shownValue = maxValueIn;
        readonly float maxValue = maxValueIn;
        readonly int textureIndex = textureIndexIn;
        Vector3 centre = centreIn;
        public override void UpdateValue(float value)
        {
            shownValue = value;
        }
        public override void DecreaseValue(float value)
        {
            shownValue -= value;
            if (shownValue < 0)
            {
                shownValue = 0;
            }
        }
        public override void IncreaseValue(float value)
        {
            shownValue += value;
            if (shownValue > maxValue)
            {
                shownValue = maxValue;
            }
        }
        public override float[] GenerateGL_Data(float aspectRatio)
        {
            List<float> gl_data = [];

            Plane p1 = new(centre, (0, 0, 1f), 0.5f, 0.05f, 1, 1.0f);

            Plane p2 = new(centre + ((0.49f * 0.5f * (shownValue / maxValue)) - (0.49f * 0.5f), 0, 0), (0, 0, 1), 0.49f * (shownValue / maxValue), 0.040f, textureIndex, 1.0f);
            
            gl_data.AddRange(p1.GenerateFloatData((0,0,0)));
            gl_data.AddRange(p2.GenerateFloatData((0,0,0)));

            return [.. gl_data];
        }
    }

    public class Background(int textureIndexIn) : HUD_Element((-0.6f, 0.0f), (0.8f, 2.0f), false, HUD_ElementType.Menu, true)
    {
        private readonly int textureIndex = textureIndexIn;
        public override float[] GenerateGL_Data(float aspectRatio)
        {
            List<float> gl_data = [];

            Plane rect = new((centreCoord.X, centreCoord.Y, 0f), (0, 0, 1), widthHeightScale.X, widthHeightScale.Y, textureIndex, 1.0f);

            gl_data.AddRange(rect.GenerateFloatData((0,0,0)));

            return [.. gl_data];
        }
    }

    public class ItemIcon(Vector2 position, Vector2 scale, int textureIndexIn, HeldItem checkItemIn) : HUD_Element(position, scale, true, HUD_ElementType.Icon, false)
    {
        readonly int textureIndex = textureIndexIn;
        readonly HeldItem checkItem = checkItemIn;

        public override float[] GenerateGL_Data(float aspectRatio)
        {
            if (!enabled)
            {
                return [];
            }

            List<float> gl_data = [];

            Plane rect = new((centreCoord.X, centreCoord.Y, 0f), (0, 0, 1), widthHeightScale.X, widthHeightScale.Y * aspectRatio, textureIndex, 1.0f);
            
            gl_data.AddRange(rect.GenerateFloatData((0,0,0)));

            return [.. gl_data];
        }

        public override void CheckIfEnabled(List<HeldItem> inventory)
        {
            bool contains = false;
            foreach (HeldItem item in inventory)
            {
                if (item.color == checkItem.color && item.itemType == checkItem.itemType)
                {
                    contains = true;
                }
            }
            enabled = contains;
        }
    }
    
    public class WeaponIcon : HUD_Element
    {
        public int selectedWeapon = 0;
        public WeaponIcon() : base((0.7f, -0.6f), (0.2f, 0.2f), true, HUD_ElementType.Icon, true)
        {

        }

        public override void UpdateValue(float input)
        {
            selectedWeapon = (int)input;
        }
        
        public override float[] GenerateGL_Data(float aspectRatio)
        {
            if (!enabled)
            {
                return [];
            }

            List<float> gl_data = [];

            Plane rect = new((centreCoord.X, centreCoord.Y, 0f), (0, 0, 1), widthHeightScale.X, widthHeightScale.Y * aspectRatio, selectedWeapon + 8, 1.0f);
            
            gl_data.AddRange(rect.GenerateFloatData((0,0,0)));
            
            return [.. gl_data];
        }
    }

    public class TextElement : HUD_Element
    {
        Vector2 topLeftCoordinate;
        Vector2 alignCoordinate;
        readonly float size;
        readonly bool centreAlign;
        protected string text;
        static private readonly int[] coordinateIndices = [0, 1, 2, 2, 0, 3];

        public TextElement(Vector2 alignCoordinateIn, float textSize, string characters, bool centreAlignIn) : base((0, 0), (1, 1), false, HUD_ElementType.Text, false)
        {
            size = textSize;
            text = characters;
            centreAlign = centreAlignIn;
            alignCoordinate = alignCoordinateIn;

            if (centreAlign)
            {
                topLeftCoordinate = alignCoordinate - (Vector2.UnitX * (characters.Length * size / 2));
            }
            else
            {
                topLeftCoordinate = alignCoordinate;
            }
        }

        public override float[] GenerateGL_Data(float aspectRatio)
        {
            if (centreAlign)
            {
                topLeftCoordinate = alignCoordinate - (Vector2.UnitX * (text.Length * size / 2));
            }
            else
            {
                topLeftCoordinate = alignCoordinate;
            }

            List<float> vertexData = [];
            int i = 0;
            foreach (char c in text)
            {
                Vector2[] textureCoordinates = Text.ReturnNewCharUV(c);
                Vector2[] vertices = [
                    topLeftCoordinate + (Vector2.UnitX * size * i),
                    topLeftCoordinate + (Vector2.UnitX * size * i) + (Vector2.UnitY * size * aspectRatio),
                    topLeftCoordinate + (Vector2.UnitX * size * (i + 1)) + (Vector2.UnitY * size * aspectRatio),
                    topLeftCoordinate + (Vector2.UnitX * size * (i + 1)),
                ];
                foreach (int index in coordinateIndices)
                {
                    vertexData.AddRange(vertices[index].X, vertices[index].Y, 0.0f, textureCoordinates[index].X, textureCoordinates[index].Y, 1.0f);
                }
                i++;
            }

            return [.. vertexData];
        }

        public override void QueueValue(string input)
        {
            text = input;
        }

    }

    public class MessageBox(Vector2 alignCoordinate, float textSize) : TextElement(alignCoordinate, textSize, "", false)
    {
        private float DurationOfMessage = 5.0f;
        private bool LevelState = false;
        Queue<string> messageQueue = [];

        public override void QueueValue(string newMessage)
        {
            messageQueue.Enqueue(newMessage);
        }

        public override void UpdateValue(float input)
        {
            DurationOfMessage += input;
        }

        public override void UpdateValue(bool input)
        {
            LevelState = input;
        }

        public override bool CheckBoolValue()
        {
            return LevelState;
        }

        public override void CheckIfEnabled(List<HeldItem> inventory)
        {
            if (DurationOfMessage > 3.0f)
            {
                if (messageQueue.Count > 0)
                {
                    text = messageQueue.Dequeue();
                    DurationOfMessage = 0.0f;
                }
                else
                {
                    text = "";
                    DurationOfMessage = 10.0f;
                }
            }
        }
    }
    
    public class AmmoText(Vector2 alignCoordinate, float textSize) : TextElement(alignCoordinate, textSize, "", true)
    {
        public override void QueueValue(string newMessage)
        {
            text = newMessage;
        }
    }
}