using System.Reflection.Metadata.Ecma335;
using OpenTK.Mathematics;

namespace Game1
{
    public class HUD
    {
        public List<HUD_Element> HUD_Elements;
        public float[] HUD_GL, TextElementGL;

        public HUD()
        {
            HUD_Elements = [];
            HUD_GL = [];
            TextElementGL = [];
        }
        public void AddHUD_Element(HUD_Element element)
        {
            HUD_Elements.Add(element);
        }

        public void SyncHUD_GL(float aspectRatio)
        {
            GenerateGL_Data(aspectRatio);
        }

        private void GenerateGL_Data(float aspectRatio)
        {
            List<float> HUD_GL_Data = [], Text_GL_Data = [];

            foreach (HUD_Element element in HUD_Elements)
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
            foreach (Triangle triangle in p1.ConvertToTriangles())
            {
                for (int i = 0; i < 3; i++)
                {
                    gl_data.AddRange(triangle.coordinates[i].X, triangle.coordinates[i].Y, triangle.coordinates[i].Z, triangle.textureCoordinates[i].X, triangle.textureCoordinates[i].Y, triangle.directionIndex);
                }
            }

            Plane p2 = new((0, 0, 0), (0, 0, 1), 0.0625f, 0.0078125f, 1, 1.0f);
            foreach (Triangle triangle in p2.ConvertToTriangles())
            {
                for (int i = 0; i < 3; i++)
                {
                    gl_data.AddRange(triangle.coordinates[i].X, triangle.coordinates[i].Y, triangle.coordinates[i].Z, triangle.textureCoordinates[i].X, triangle.textureCoordinates[i].Y, triangle.directionIndex);
                }
            }
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
            foreach (Triangle triangle in p1.ConvertToTriangles())
            {
                for (int i = 0; i < 3; i++)
                {
                    gl_data.AddRange(triangle.coordinates[i].X, triangle.coordinates[i].Y, triangle.coordinates[i].Z, triangle.textureCoordinates[i].X, triangle.textureCoordinates[i].Y, triangle.directionIndex);
                }
            }

            Plane p2 = new(centre + ((0.49f * 0.5f * (shownValue / maxValue)) - (0.49f * 0.5f), 0, 0), (0, 0, 1), 0.49f * (shownValue / maxValue), 0.040f, textureIndex, 1.0f);
            foreach (Triangle triangle in p2.ConvertToTriangles())
            {
                for (int i = 0; i < 3; i++)
                {
                    gl_data.AddRange(triangle.coordinates[i].X, triangle.coordinates[i].Y, triangle.coordinates[i].Z, triangle.textureCoordinates[i].X, triangle.textureCoordinates[i].Y, triangle.directionIndex);
                }
            }
            return [.. gl_data];
        }
    }

    public class Background(int textureIndexIn) : HUD_Element((-0.6f, 0.0f), (0.8f, 2.0f), false, HUD_ElementType.Menu, true)
    {
        private readonly int textureIndex = textureIndexIn;
        public override float[] GenerateGL_Data(float aspectRatio)
        {
            List<float> gl_data = [];

            Plane p2 = new((centreCoord.X, centreCoord.Y, 0f), (0, 0, 1), widthHeightScale.X, widthHeightScale.Y, textureIndex, 1.0f);
            foreach (Triangle triangle in p2.ConvertToTriangles())
            {
                for (int i = 0; i < 3; i++)
                {
                    gl_data.AddRange(triangle.coordinates[i].X, triangle.coordinates[i].Y, triangle.coordinates[i].Z, triangle.textureCoordinates[i].X, triangle.textureCoordinates[i].Y, triangle.directionIndex);
                }
            }
            return [.. gl_data];
        }
    }

    public class ItemIcon : HUD_Element
    {
        readonly int textureIndex;
        readonly HeldItem checkItem;
        public ItemIcon(Vector2 position, Vector2 scale, int textureIndexIn, HeldItem checkItemIn) : base(position, scale, true, HUD_ElementType.Icon, false)
        {
            textureIndex = textureIndexIn;
            checkItem = checkItemIn;
        }

        public override float[] GenerateGL_Data(float aspectRatio)
        {
            if (!enabled)
            {
                return [];
            }

            List<float> gl_data = [];

            Plane p2 = new((centreCoord.X, centreCoord.Y, 0f), (0, 0, 1), widthHeightScale.X, widthHeightScale.Y * aspectRatio, textureIndex, 1.0f);
            foreach (Triangle triangle in p2.ConvertToTriangles())
            {
                for (int i = 0; i < 3; i++)
                {
                    gl_data.AddRange(triangle.coordinates[i].X, triangle.coordinates[i].Y, triangle.coordinates[i].Z, triangle.textureCoordinates[i].X, triangle.textureCoordinates[i].Y, triangle.directionIndex);
                }
            }
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

            Plane p2 = new((centreCoord.X, centreCoord.Y, 0f), (0, 0, 1), widthHeightScale.X, widthHeightScale.Y * aspectRatio, selectedWeapon + 8, 1.0f);
            foreach (Triangle triangle in p2.ConvertToTriangles())
            {
                for (int i = 0; i < 3; i++)
                {
                    gl_data.AddRange(triangle.coordinates[i].X, triangle.coordinates[i].Y, triangle.coordinates[i].Z, triangle.textureCoordinates[i].X, triangle.textureCoordinates[i].Y, triangle.directionIndex);
                }
            }
            return [.. gl_data];
        }
    }
    
    public class TextElement: HUD_Element
    {
        Vector2 topLeftCoordinate;
        float size;
        public string text;
        readonly int[] coordinateIndices = [0, 1, 2, 2, 0, 3];

        public TextElement(Vector2 alignCoordinate, float textSize, string characters, bool centreAlign) : base ((0,0), (1,1), false, HUD_ElementType.Text, false)
        {
            size = textSize;
            text = characters;

            if (centreAlign)
            {
                topLeftCoordinate = alignCoordinate - (Vector2.UnitX * (characters.Length * size / 2));
            } else
            {
                topLeftCoordinate = alignCoordinate;
            }
        }
        
        public override float[] GenerateGL_Data(float aspectRatio)
        {
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

    }

    public class AmmoUsageIndicator : HealthBar
    {
        public AmmoUsageIndicator() : base(1.0f, (0.7f, -0.8f, 0), 3)
        {
        }
    }
}