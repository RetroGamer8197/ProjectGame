using OpenTK.Mathematics;

namespace Game1
{
    public class HUD
    {
        public List<HUD_Element> HUD_Elements;
        public float[] HUD_GL;

        public HUD()
        {
            HUD_Elements = [];
            HUD_GL = [];
        }
        public void AddHUD_Element(HUD_Element element)
        {
            HUD_Elements.Add(element);
        }

        public void SyncHUD_GL(float aspectRatio)
        {
            HUD_GL = GenerateGL_Data(aspectRatio);
        }

        private float[] GenerateGL_Data(float aspectRatio)
        {
            List<float> GL_Data = [];

            foreach (HUD_Element element in HUD_Elements)
            {
                GL_Data.AddRange(element.GenerateGL_Data(aspectRatio));
            }

            return [.. GL_Data];
        }
    }

    public class HUD_Element(Vector2 centre, Vector2 scale, bool doesAspectRatioAffect, HUD_Element.HUD_ElementType thisElementType)
    {
        Vector2 centreCoord = centre;
        Vector2 widthHeightScale = scale;
        bool aspectRatioAffect = doesAspectRatioAffect;
        public HUD_ElementType elementType = thisElementType;
        public enum HUD_ElementType
        {
            Crosshair, HealthBar, Menu, Icon
        }

        public virtual float[] GenerateGL_Data(float aspectRatio)
        {
            return [];
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

    public class Crosshair() : HUD_Element((0, 0), (1, 1), false, HUD_ElementType.Crosshair)
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

    public class HealthBar(float maxValueIn, Vector3 centreIn, int textureIndexIn) : HUD_Element((0, 0), (1, 1), true, HUD_ElementType.HealthBar)
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

    public class AmmoUsageIndicator : HealthBar
    {
        public AmmoUsageIndicator() : base(1.0f, (0.7f, -0.8f, 0), 3)
        {
        }
    }
}