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