using System.Reflection.Metadata.Ecma335;
using Game2;
using OpenTK.Mathematics;

namespace Game1
{
    public class Object
    {
        
        public enum ObjectType
        {
            None = 0, Triangle = 1, Plane = 2, Entity = 3, Cube = 4, Button = 5, Door = 6, EndButton = 7
        }
        public ObjectType objectType;
        
        public virtual bool CheckCollision(Vector3 input, Vector3 playerScale)
        {
            return false;
        }

        public virtual void Tick(Vector3 playerPosition, ref float health, float deltaTime, ref Level level, ref Player player)
        {

        }

        public virtual void HandleClickedOn(float attackDamage)
        {

        }

        public virtual void HandleInteract(ref List<HeldItem> heldItems, ref HUD HUD_Object)
        {

        }

        public virtual bool CheckClickedCollision(Vector3 input, float stepScale, out float distanceFrom)
        {
            distanceFrom = float.MaxValue;
            return false;
        }

        public virtual void CheckObjectStateIsCorrect(ref Level level)
        {
            
        }

        public virtual float[] GenerateFloatData(Vector3 playerRotation)
        {
            return[];
        }
    }

    public class Triangle : Object
    {
        public Vector3[] coordinates;
        public Vector2[] textureCoordinates;
        public float directionIndex;

        public Triangle(Vector3 v1, Vector3 v2, Vector3 v3, Vector2[] textureCoordinatesIn, float directionshade)
        {
            objectType = ObjectType.Triangle;
            coordinates = [v1, v2, v3];

            textureCoordinates = new Vector2[3];
            directionIndex = directionshade;
            for (int i = 0; i < textureCoordinatesIn.Length; i++)
            {
                textureCoordinates[i] = textureCoordinatesIn[i];
            }
        }

        public static void LoadFromFile(out Triangle triangleOutput, ref BinaryReader levelReader)
        {
            Vector3 v1, v2, v3;
            Vector2 tc1, tc2, tc3;
            byte textureIndex;
            float directionIndex;
            v1 = CustomVector3Extension.ReadVector3FromFile(ref levelReader);;
            v2 = CustomVector3Extension.ReadVector3FromFile(ref levelReader);;
            v3 = CustomVector3Extension.ReadVector3FromFile(ref levelReader);;

            tc1 = new(levelReader.ReadSingle(), levelReader.ReadSingle());
            tc2 = new(levelReader.ReadSingle(), levelReader.ReadSingle());
            tc3 = new(levelReader.ReadSingle(), levelReader.ReadSingle());

            textureIndex = levelReader.ReadByte();
            directionIndex = levelReader.ReadSingle();

            triangleOutput = new(v1, v2, v3, [tc1, tc2, tc3], directionIndex);
        }

        public override float[] GenerateFloatData(Vector3 playerRotation)
        {
            List<float> floatData = [];
            for (int i = 0; i < 3; i++)
            {
                floatData.Add(coordinates[i].X);
                floatData.Add(coordinates[i].Y);
                floatData.Add(coordinates[i].Z);
                floatData.Add(textureCoordinates[i].X);
                floatData.Add(textureCoordinates[i].Y);
                floatData.Add(directionIndex);
            }

            return [..floatData];
        }
    }

    public class Plane : Object
    {
        public Vector3 centre, normal;
        public readonly float width, height;
        public int textureIndex;
        public readonly float directionIndex;

        private Vector3 UnitRight, UnitUp;

        public Vector3[] RectangleCoordinates = new Vector3[4];

        public Plane(Vector3 centreIn, Vector3 normalIn, float widthIn, float heightIn, int textureIndexIn, float directionIndexIn)
        {
            if (normalIn != Vector3.UnitY && normalIn != -Vector3.UnitY)
            {
                UnitRight = Vector3.Normalize(Vector3.Cross(normalIn, -Vector3.UnitY));
            }
            else
            {
                UnitRight = Vector3.Normalize(Vector3.Cross(normalIn, -Vector3.UnitZ));
            }
            UnitUp = Vector3.Normalize(Vector3.Cross(-UnitRight, normalIn));


            objectType = ObjectType.Plane;

            centre = centreIn;
            normal = normalIn;
            width = widthIn;
            height = heightIn;
            textureIndex = textureIndexIn;
            directionIndex = directionIndexIn;

            RectangleCoordinates[0] = centre - (UnitUp * 0.5f * height) - (UnitRight * 0.5f * width);
            RectangleCoordinates[1] = centre + (UnitUp * 0.5f * height) - (UnitRight * 0.5f * width);
            RectangleCoordinates[2] = centre + (UnitUp * 0.5f * height) + (UnitRight * 0.5f * width);
            RectangleCoordinates[3] = centre - (UnitUp * 0.5f * height) + (UnitRight * 0.5f * width);
        }

        public static void LoadFromFile(out Plane plane, ref BinaryReader levelReader)
        {
            Vector3 centreTemp, normalTemp;
            float widthTemp, heightTemp;
            int textureIndexTemp;

            float directionIndexIn;

            centreTemp = CustomVector3Extension.ReadVector3FromFile(ref levelReader);;
            normalTemp = CustomVector3Extension.ReadVector3FromFile(ref levelReader);;
            widthTemp = levelReader.ReadSingle();
            heightTemp = levelReader.ReadSingle();
            textureIndexTemp = levelReader.ReadInt32();
            directionIndexIn = levelReader.ReadSingle();

            plane = new(centreTemp, normalTemp, widthTemp, heightTemp, textureIndexTemp, directionIndexIn);
        }

        public override float[] GenerateFloatData(Vector3 playerRotation)
        {
            List<float> floatData = [];

            Vector2[] TextureCoordinates =
            [
                new(textureIndex % 4 * 0.25f + 0.00390625f, (3.00390625f - (textureIndex >> 2)) * 0.25f),
                new(textureIndex % 4 * 0.25f + 0.00390625f, (3.99609375f - (textureIndex >> 2)) * 0.25f),
                new((0.99609375f + (textureIndex % 4)) * 0.25f, (3.99609375f - (textureIndex >> 2)) * 0.25f),
                new((0.99609375f + (textureIndex % 4)) * 0.25f, (3.00390625f - (textureIndex >> 2)) * 0.25f),
            ];

            floatData.AddRange(new Triangle(RectangleCoordinates[0], RectangleCoordinates[1], RectangleCoordinates[2], TextureCoordinates[0..3], directionIndex).GenerateFloatData((0,0,0)));
            floatData.AddRange(new Triangle(RectangleCoordinates[0], RectangleCoordinates[2], RectangleCoordinates[3], [TextureCoordinates[0], TextureCoordinates[2], TextureCoordinates[3]], directionIndex).GenerateFloatData((0,0,0)));
            return [..floatData];
        }

        public override bool CheckCollision(Vector3 coordinate, Vector3 playerScale)
        {
            float distanceToPlane, distanceToCentre;
            float d = Vector3.Dot(normal, centre);
            if (normal.Length > 0)
            {
                distanceToPlane = Math.Abs((normal.X * coordinate.X) + (normal.Y * coordinate.Y) + (normal.Z * coordinate.Z) - d) / normal.Length;
            }
            else
            {
                return false;
            }

            if (distanceToPlane < 0.1f)
            {
                Vector3 vectorToCentre = centre - coordinate;
                distanceToCentre = vectorToCentre.Length;

                if (distanceToCentre < ((width / 2 * UnitRight) + (height / 2 * UnitUp)).Length)
                {
                    float areaSum = 0;
                    float totalArea = width * height;

                    areaSum += CustomVector3Extension.CalculateArea(RectangleCoordinates[0], coordinate, RectangleCoordinates[3]);
                    areaSum += CustomVector3Extension.CalculateArea(RectangleCoordinates[3], coordinate, RectangleCoordinates[2]);
                    areaSum += CustomVector3Extension.CalculateArea(RectangleCoordinates[2], coordinate, RectangleCoordinates[1]);
                    areaSum += CustomVector3Extension.CalculateArea(coordinate, RectangleCoordinates[1], RectangleCoordinates[0]);

                    if (areaSum < 1.05 * totalArea)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public class Button : Plane
    {
        public readonly HeldItem.Colors buttonColor;
        public bool active = true;
        public readonly int activeTextureIndex;
        public readonly int inactiveTextureIndex;

        public Button(Vector3 centreIn, Vector3 normalIn, float widthIn, float heightIn, int textureIndexIn, int inactiveTextureIndexIn, float directionIndexIn, HeldItem.Colors color) : base(centreIn, normalIn, widthIn, heightIn, textureIndexIn, directionIndexIn)
        {
            buttonColor = color;
            activeTextureIndex = textureIndexIn;
            inactiveTextureIndex = inactiveTextureIndexIn;
            objectType = ObjectType.Button;
        }

        public static void LoadFromFile(out Button button, ref BinaryReader levelReader)
        {

            Vector3 centreTemp, normalTemp;
            float widthTemp, heightTemp;
            int activeTextureIndex, inactiveTextureIndex;
            HeldItem.Colors color;

            float directionIndexIn;

            centreTemp = CustomVector3Extension.ReadVector3FromFile(ref levelReader);
            normalTemp = CustomVector3Extension.ReadVector3FromFile(ref levelReader);
            widthTemp = levelReader.ReadSingle();
            heightTemp = levelReader.ReadSingle();
            activeTextureIndex = levelReader.ReadInt32();
            inactiveTextureIndex = levelReader.ReadInt32();
            directionIndexIn = levelReader.ReadSingle();
            color = (HeldItem.Colors)levelReader.ReadByte();

            button = new(centreTemp, normalTemp, widthTemp, heightTemp, activeTextureIndex, inactiveTextureIndex, directionIndexIn, color);
        }

        public override bool CheckClickedCollision(Vector3 input, float stepScale, out float distanceFrom)
        {
            float distanceAllowedXZ = new Vector2(width / 2, width / 2).LengthSquared;
            float distanceAllowedY = height;
            Vector3 VectorDistanceFrom = centre - input;
            distanceFrom = VectorDistanceFrom.LengthSquared;
            if (new Vector2(VectorDistanceFrom.X, VectorDistanceFrom.Z).LengthSquared < distanceAllowedXZ && VectorDistanceFrom.Y < distanceAllowedY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override float[] GenerateFloatData(Vector3 playerRotation)
        {
            if (active)
            {
                textureIndex = activeTextureIndex;
            }
            else
            {
                textureIndex = inactiveTextureIndex;
            }

            return base.GenerateFloatData((0,0,0));
        }

        public override void HandleInteract(ref List<HeldItem> heldItems, ref HUD HUD_Object)
        {
            base.HandleInteract(ref heldItems, ref HUD_Object);

            foreach (HeldItem item in heldItems)
            {
                if (item.color == buttonColor)
                {
                    active = !active;
                    return;
                }
            }

            string[] colors = ["red", "green", "blue", "yellow", "colorless"];

            HUD_Object.QueueMessage("You need the " + colors[(int)buttonColor] + " keycard to open this door");

        }
    }

    public class LevelEndButton : Button
    {
        public LevelEndButton(Vector3 centreIn, Vector3 normalIn, float widthIn, float heightIn, int textureIndexIn, int inactiveTextureIndexIn, float directionIndexIn) : base(centreIn, normalIn, widthIn, heightIn, textureIndexIn, inactiveTextureIndexIn, directionIndexIn, HeldItem.Colors.None)
        {
            objectType = ObjectType.EndButton;
        }

        public static void LoadFromFile(out LevelEndButton button, ref BinaryReader levelReader)
        {

            Vector3 centreTemp, normalTemp;
            float widthTemp, heightTemp;
            int activeTextureIndex, inactiveTextureIndex;

            float directionIndexIn;

            centreTemp = CustomVector3Extension.ReadVector3FromFile(ref levelReader);
            normalTemp = CustomVector3Extension.ReadVector3FromFile(ref levelReader);
            widthTemp = levelReader.ReadSingle();
            heightTemp = levelReader.ReadSingle();
            activeTextureIndex = levelReader.ReadInt32();
            inactiveTextureIndex = levelReader.ReadInt32();
            directionIndexIn = levelReader.ReadSingle();

            button = new(centreTemp, normalTemp, widthTemp, heightTemp, activeTextureIndex, inactiveTextureIndex, directionIndexIn);
        }

        public override void HandleInteract(ref List<HeldItem> heldItems, ref HUD HUD_Object)
        {
            HUD_Object.QueueMessage("Level complete!");
            HUD_Object.LevelReset();
        }
    }

    public class Cube : Object
    {
        public float scaleX, scaleY, scaleZ;
        public Vector3 centre;
        readonly Vector3[] vertexCoordinates;
        public readonly int textureIndex;
        // vertex indices references the cube coordinates that are generated in the initialiser
        readonly int[] vertexindices = [
            0, 1, 2, 1, 2, 3,       // negative x face
            4, 5, 6, 5, 6, 7,       // positive x face
            0, 1, 4, 1, 4, 5,       // positive y face
            2, 3, 6, 3, 6, 7,       // negative y face
            1, 3, 5, 3, 5, 7,       // positive z face
            0, 2, 4, 2, 4, 6        // negative z face
        ];
        readonly float[] directions = [0.7f, 0.9f, 1.0f, 0.6f, 0.8f, 0.8f];
        readonly int[] textureCoordIndices = [      // used to reference the correct texute coordinates when a cube is generated by the program

            2, 0, 3, 0, 3, 1,       // negative x face
            0, 2, 1, 2, 1, 3,       // positive x face
            1, 0, 3, 0, 3, 2,       // positive y face
            0, 1, 2, 1, 2, 3,       // negative y face
            2, 3, 0, 3, 0, 1,       // positive z face
            0, 1, 2, 1, 2, 3,       // negative z face

        ];
        private  readonly Vector2[] textureCoordinates;

        public Cube(Vector3 centrein, float scaleXin, float scaleYin, float scaleZin, int TextureIndexIn)
        {
            objectType = ObjectType.Cube;
            scaleX = scaleXin;
            scaleY = scaleYin;
            scaleZ = scaleZin;
            centre = centrein;

            textureIndex = TextureIndexIn;

            /// textures in OpenGL are stored as an image and addressed by a (0,0) to (1,1) UV space.
            /// this project uses a texture atlas to store all the textures used so a texture index in the atlas 
            /// must be converted to UV space before OpenGL can process it
            textureCoordinates = [
                new(textureIndex % 4 * 0.25f + 0.00390625f, (3.99f - (textureIndex >> 2)) * 0.25f),
                new(textureIndex % 4 * 0.25f + 0.00390625f, (3.00390625f - (textureIndex >> 2)) * 0.25f),
                new((0.99609375f + (textureIndex % 4)) * 0.25f, (3.99609375f - (textureIndex >> 2)) * 0.25f),
                new((0.99609375f + (textureIndex % 4)) * 0.25f, (3.00390625f - (textureIndex >> 2)) * 0.25f),
            ];
            /// the texture atlas is a 256x256 image. As bilinear filtering is used on the world atlas, the textures need to be
            /// one pixel offset from the edge on all sides to prevent bleed from neighbouring textures. One pixel is
            /// 0.00390625f so this is used to describe a 62x62 area of the texture atlas which will sample from the whole 64x64 texture

            // vertex coordinates are used when converting this object to the format used by OpenGL and the shaders
            vertexCoordinates = [
                centre + (Vector3.UnitX * scaleX / 2) + (Vector3.UnitY * scaleY / 2) - (Vector3.UnitZ * scaleZ / 2),
                centre + (Vector3.UnitX * scaleX / 2) + (Vector3.UnitY * scaleY / 2) + (Vector3.UnitZ * scaleZ / 2),
                centre + (Vector3.UnitX * scaleX / 2) - (Vector3.UnitY * scaleY / 2) - (Vector3.UnitZ * scaleZ / 2),
                centre + (Vector3.UnitX * scaleX / 2) - (Vector3.UnitY * scaleY / 2) + (Vector3.UnitZ * scaleZ / 2),
                centre - (Vector3.UnitX * scaleX / 2) + (Vector3.UnitY * scaleY / 2) - (Vector3.UnitZ * scaleZ / 2),
                centre - (Vector3.UnitX * scaleX / 2) + (Vector3.UnitY * scaleY / 2) + (Vector3.UnitZ * scaleZ / 2),
                centre - (Vector3.UnitX * scaleX / 2) - (Vector3.UnitY * scaleY / 2) - (Vector3.UnitZ * scaleZ / 2),
                centre - (Vector3.UnitX * scaleX / 2) - (Vector3.UnitY * scaleY / 2) + (Vector3.UnitZ * scaleZ / 2)
            ];
        }

        public override bool CheckCollision(Vector3 input, Vector3 playerScale)
        {
            // this checks if any vertex of the player's hitbox (an AABB defined by input and player scale) lies within the bounds of the AABB of the cube
            if (centre.X + scaleX / 2 > input.X - (playerScale.X / 2) && centre.X - scaleX / 2 < input.X + (playerScale.X / 2))
            {
                if (centre.Y + scaleY / 2 > input.Y - (playerScale.Y / 2) && centre.Y - scaleY / 2 < input.Y + (playerScale.Y / 2))
                {
                    if (centre.Z + scaleZ / 2 > input.Z - (playerScale.Z / 2) && centre.Z - scaleZ / 2 < input.Z + (playerScale.Z / 2))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override bool CheckClickedCollision(Vector3 input, float stepScale, out float distanceFrom)
        {
            bool isCollision = CheckCollision(input, new(stepScale));

            if (isCollision) {
                distanceFrom = 0;
            } else {
                distanceFrom = float.MaxValue;
            }

            return isCollision;
        }

        public override float[] GenerateFloatData(Vector3 playerRotation)
        {
            List<float> floatData = [];

            for (int i = 0; i < vertexindices.Length / 3; i++)
            {
                Triangle triTemp = new(vertexCoordinates[vertexindices[i * 3]], vertexCoordinates[vertexindices[(i * 3) + 1]], vertexCoordinates[vertexindices[(i * 3) + 2]], [textureCoordinates[textureCoordIndices[(i * 3) + 0]], textureCoordinates[textureCoordIndices[(i * 3) + 1]], textureCoordinates[textureCoordIndices[(i * 3) + 2]]], directions[i / 2]);
                floatData.AddRange(triTemp.GenerateFloatData((0,0,0)));
            }

            return [..floatData];
        }

        public static void LoadFromFile(out Cube cube, ref BinaryReader levelReader)
        {
            Vector3 centreTemp;
            float scaleX, scaleY, scaleZ;
            int TextureIndexIn;

            centreTemp = CustomVector3Extension.ReadVector3FromFile(ref levelReader);
            scaleX = levelReader.ReadSingle();
            scaleY = levelReader.ReadSingle();
            scaleZ = levelReader.ReadSingle();
            TextureIndexIn = levelReader.ReadInt32();

            cube = new(centreTemp, scaleX, scaleY, scaleZ, TextureIndexIn);
        }
    }

    public class Door : Cube
    {
        public HeldItem.Colors ActivatorColor;
        public bool defaultState, currentState;
        public Door(Vector3 centrein, float scaleXin, float scaleYin, float scaleZin, int TextureIndexIn, HeldItem.Colors color, bool defaultStateIn) : base(centrein, scaleXin, scaleYin, scaleZin, TextureIndexIn)
        {
            defaultState = defaultStateIn;
            ActivatorColor = color;
            objectType = ObjectType.Door;
        }

        public override void CheckObjectStateIsCorrect(ref Level level)
        {
            foreach (Object o in level.levelObjects)
            {
                if (o.objectType == ObjectType.Button)
                {
                    Button tempButton = (Button)o;

                    if (tempButton.buttonColor == ActivatorColor)
                    {
                        currentState = defaultState ^ tempButton.active;
                    }
                }
            }
        }

        public override bool CheckCollision(Vector3 input, Vector3 playerScale)
        {
            if (!currentState)
            {
                return false;
            }
            return base.CheckCollision(input, playerScale);
        }

        public override float[] GenerateFloatData(Vector3 playerRotation)
        {
            if (!currentState)
            {
                return [];
            } else
            {
                return base.GenerateFloatData((0,0,0));
            }
        }

        public static void LoadFromFile(out Door door, ref BinaryReader levelReader)
        {
            Vector3 centreTemp;
            float scaleX, scaleY, scaleZ;
            int TextureIndexIn;
            HeldItem.Colors color;
            bool defaultState;

            centreTemp = CustomVector3Extension.ReadVector3FromFile(ref levelReader);
            scaleX = levelReader.ReadSingle();
            scaleY = levelReader.ReadSingle();
            scaleZ = levelReader.ReadSingle();
            TextureIndexIn = levelReader.ReadInt32();
            color = (HeldItem.Colors)levelReader.ReadByte();
            defaultState = levelReader.ReadBoolean();

            door = new(centreTemp, scaleX, scaleY, scaleZ, TextureIndexIn, color, defaultState);
        }
    }
}
