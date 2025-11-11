using System.Data.Common;
using OpenTK.Graphics.ES11;
using OpenTK.Mathematics;

namespace Game1
{
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