using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace Game1
{

    public class Entity : Object
    {
        public Vector3 Position;
        public Vector3 origin;
        public Vector2 scale;
        public int textureIndex, healthChange;
        public bool pathfinding;
        public Entity(Vector3 position, Vector2 scaleIn, int textureIndexIn, int healthChangeIn, bool pathfindingIn)
        {
            objectType = ObjectType.Entity;
            Position = position;
            scale = scaleIn;
            textureIndex = textureIndexIn;
            healthChange = healthChangeIn;
            pathfinding = pathfindingIn;
            origin = position;
        }

        public float[] GenerateOpenGLData(Vector3 playerRotation)
        {
            List<float> openGLData = [];

            Vector3 front = Matrix3.CreateFromQuaternion(Quaternion.FromEulerAngles(playerRotation)) * new Vector3(0.0f, 0.0f, 1.0f);

            Plane entitySprite = new(Position, front, scale.X, scale.Y, textureIndex, 1.0f);

            Triangle[] triangletemps = entitySprite.ConvertToTriangles();
            foreach (Triangle triangle in triangletemps)
            {
                for (int i = 0; i < 3; i++)
                {
                    openGLData.AddRange(triangle.coordinates[i].X, triangle.coordinates[i].Y, triangle.coordinates[i].Z, triangle.textureCoordinates[i].X, triangle.textureCoordinates[i].Y, triangle.directionIndex);
                }
            }
            return [.. openGLData];
        }

    }

    public class Enemy(Vector3 position, Vector2 scaleIn, int textureIndexIn, int healthChangeIn, bool pathfindingIn) : Entity(position, scaleIn, textureIndexIn, healthChangeIn, pathfindingIn)
    {
        public override void Tick(Vector3 playerPosition, ref float health)
        {

            /*if (Position.Z > 5)
            {
                direction = -1;
            }
            else if (Position.Z < 0)
            {
                direction = 1;
            }
            Position += direction * Vector3.UnitZ * Game.speed * 0.25f;*/
            Vector3 PositionOld = Position;

            Vector3 directionVector = (playerPosition.X - Position.X, 0, playerPosition.Z - Position.Z);

            if (directionVector.LengthSquared > 25f)
            {
                directionVector = (new Random().Next(-5, 5), 0, new Random().Next(-5, 5));
            }
            else
            {
                /*if (new Random().Next(0, 50) > 30)
                {
                    Position += Vector3.Normalize(Vector3.Cross(directionVector, Vector3.UnitY)) * new Random().Next(-1, 2) * Game.speed / 4;
                }*/
            }

            if (directionVector.LengthSquared > 0)
            {
                directionVector.Normalize();
            }

            Position += directionVector * Game.speed / 4;

            if (float.IsNaN(Position.X))
            {
                Console.WriteLine();
            }
            if ((Position - playerPosition).LengthSquared < 0.1f)
            {
                Position = origin;
            }
        }
    }

    public class Item(Vector3 position, Vector2 scaleIn, int textureIndexIn, int healthChangeIn, bool pathfindingIn) : Entity(position, scaleIn, textureIndexIn, healthChangeIn, pathfindingIn)
    {
        public override void Tick(Vector3 playerPosition, ref float health)
        {
            health += healthChange;
        }
    }

}