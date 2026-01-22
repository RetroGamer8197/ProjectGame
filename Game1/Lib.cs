using OpenTK.Audio.OpenAL;
using OpenTK.Mathematics;

namespace Game1
{
    public abstract class CustomVector3Extension
    {
        public static float CalculateArea(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float length1, length2;
            double angle;

            length1 = (p1 - p2).Length;
            length2 = (p1 - p3).Length;

            angle = Math.Acos(Vector3.Dot(p1 - p2, p1 - p3) / (length1 * length2));

            return (float)(0.5 * Math.Sin(angle) * length1 * length2);

        }

        public static void WriteVector3ToFile(Vector3 vectorIn, ref BinaryWriter file)
        {
            file.Write(vectorIn.X);
            file.Write(vectorIn.Y);
            file.Write(vectorIn.Z);
        }

        public static Vector3 ReadVector3FromFile(ref BinaryReader file)
        {
            return (file.ReadSingle(), file.ReadSingle(), file.ReadSingle());
        }
    }

    public abstract class CustomMatrix4
    {
        public static Matrix4 GenerateViewMatrix(Vector3 c, Vector3 front, Vector3 up)
        {
            Matrix4 result;

            Vector3 z = -Vector3.Normalize(front);
            Vector3 x = Vector3.Normalize(Vector3.Cross(up, z));
            Vector3 y = Vector3.Normalize(Vector3.Cross(z, x));
            Vector3 d = new(
                -((c.X * x.X) + (c.Y * x.Y) + (c.Z * x.Z)),
                -((c.X * y.X) + (c.Y * y.Y) + (c.Z * y.Z)),
                -((c.X * z.X) + (c.Y * z.Y) + (c.Z * z.Z))
            ); // position of camerax

            result = new(){
                M11 = x.X, M12 = y.X, M13 = z.X, M14 = 0,
                M21 = x.Y, M22 = y.Y, M23 = z.Y, M24 = 0,
                M31 = x.Z, M32 = y.Z, M33 = z.Z, M34 = 0,
                M41 = d.X, M42 = d.Y, M43 = d.Z, M44 = 1,
            };

            return result;
        }

        public static Matrix4 MakeFrustum(float fovX, float aspectRatio, float front, float back)
        {
            float right = front * (float)Math.Tan(fovX/2);  // half width of near plane
            float top = right / aspectRatio;                // half height of near plane

            // params: left, right, bottom, top, near(front), far(back)
            Matrix4 matrix = new()
            {
                M11 = front / right,
                M22 = front / top,
                M33 = -(back + front) / (back - front),
                M34 = -1,
                M43 = -(2 * back * front) / (back - front),
                M44 = 0
            };
            return matrix;
        }
    }

    public abstract class Text
    {
        const int charsAcross = 16, charsDown = 8;
        const string characters = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
        
        public static Vector2[] ReturnNewCharUV(char c)
        {
            int textureIndex = Math.Clamp(characters.IndexOf(c), 0, (charsAcross * charsDown) - 1);
            int lsrOffset = (int)Math.Log2(charsAcross);
            float acrossScale = 1f / charsAcross, downScale = 1f / charsDown;
            Vector2[] TextureCoordinates =
            [
                new(textureIndex % charsAcross * acrossScale, (charsDown - 1 - (textureIndex >> lsrOffset)) * downScale),
                new(textureIndex % charsAcross * acrossScale, (charsDown - (textureIndex >> lsrOffset)) * downScale),
                new((1 + (textureIndex % charsAcross)) * acrossScale, (charsDown - (textureIndex >> lsrOffset)) * downScale),
                new((1 + (textureIndex % charsAcross)) * acrossScale, (charsDown - 1 - (textureIndex >> lsrOffset)) * downScale),
            ];

            return TextureCoordinates;
        }
    }


}