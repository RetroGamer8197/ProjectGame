using OpenTK.Mathematics;

namespace Game1
{
    public class HUD
    {
        List<HUD_Element> HUD_Elements;
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

    public class HUD_Element
    {
        Vector2 centreCoord;
        Vector2 widthHeightScale;
        bool aspectRatioAffect;
        public HUD_Element(Vector2 centre, Vector2 scale, bool doesAspectRatioAffect)
        {
            centreCoord = centre;
            widthHeightScale = scale;
            aspectRatioAffect = doesAspectRatioAffect;
        }
        public virtual float[] GenerateGL_Data(float aspectRatio)
        {
            return [];
        }
    }

    public class Crosshair() : HUD_Element ((0,0), (1,1), false)
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
            return [
                ..gl_data];
        }
    }

}