using OpenTK.Graphics.ES11;
using OpenTK.Mathematics;

namespace Game1
{

    public abstract class Text
    {
        const int charsAcross = 16, charsDown = 8;
        const string characters = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
        public static Vector2[] ReturnCharacterUV(char c)
        {
            int index = characters.IndexOf(c);
            if (index == -1)
            {
                return [(0, 0), (0, 0), (0, 0), (0, 0)];
            }
            float x1 = (index % 16) * (32f / 512f);
            float y1 = 1 - ((index >> 3) * (8 / 256f));
            float x2 = ((index % 16) + 1) * (32f / 512f);
            float y2 = 1 - (((index + 16) >> 3) * (8 / 256f));

            return [
                (x1, y1), (x2, y1), (x2, y2), (x1, y2)
            ];

        }
        
        public static Vector2[] ReturnNewCharUV(char c)
        {
            int textureIndex = characters.IndexOf(c);
            Vector2[] TextureCoordinates =
            [
                new(textureIndex % 16 * 0.0625f, (7 - (textureIndex >> 4)) * 0.125f),
                new(textureIndex % 16 * 0.0625f, (8 - (textureIndex >> 4)) * 0.125f),
                new((1 + (textureIndex % 16)) * 0.0625f, (8 - (textureIndex >> 4)) * 0.125f),
                new((1 + (textureIndex % 16)) * 0.0625f, (7 - (textureIndex >> 4)) * 0.125f),

                /*new(textureIndex % 4 * 0.25f, (3 - (textureIndex >> 2)) * 0.25f),
                new(textureIndex % 4 * 0.25f, (4 - (textureIndex >> 2)) * 0.25f),
                new((1 + (textureIndex % 4)) * 0.25f, (4 - (textureIndex >> 2)) * 0.25f),
                new((1 + (textureIndex % 4)) * 0.25f, (3 - (textureIndex >> 2)) * 0.25f),*/
            ];

            return TextureCoordinates;
        }
    }

    /*public class TextElement(Vector2 textTopLeft, float textSize, string characters)
    {
        Vector2 topLeftCoordinate = textTopLeft;
        float size = textSize;
        public string text = characters;

        int[] coordinateIndices = [0, 1, 2, 2, 1, 3];
        
        public float[] GenerateVertexData()
        {
            List<float> vertexData = [];
            int i = 0;
            foreach (char c in text)
            {
                Vector2[] textureCoordinates = Text.ReturnCharacterUV(c);
                Vector2[] vertices = [
                    topLeftCoordinate + (Vector2.UnitX * size * i),
                    topLeftCoordinate + (Vector2.UnitX * size * (i + 1)),
                    topLeftCoordinate + (Vector2.UnitX * size * (i + 1)) + (Vector2.UnitY * size),
                    topLeftCoordinate + (Vector2.UnitX * size * i) + (Vector2.UnitY * size),
                ];
                foreach (int index in coordinateIndices)
                {
                    vertexData.AddRange(vertices[index].X, vertices[index].Y, 0.0f, textureCoordinates[index].X, textureCoordinates[index].Y, 1.0f);
                }
            }

            return [.. vertexData];
        }

    }*/

}