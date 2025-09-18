using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Game1
{

    public class Entity : Object
    {
        public enum EntityType
        {
            None, Enemy, Item
        }
        public Vector3 Position;
        public Vector3 origin;
        public Vector2 scale;
        public int textureIndex, healthChange;
        public bool pathfinding;
        public EntityType entityType;
        public bool alive = true;
        public Entity(Vector3 position, Vector2 scaleIn, int textureIndexIn, int healthChangeIn, bool pathfindingIn, EntityType entityTypeIn)
        {
            objectType = ObjectType.Entity;
            entityType = entityTypeIn;
            Position = position;
            scale = scaleIn;
            textureIndex = textureIndexIn;
            healthChange = healthChangeIn;
            pathfinding = pathfindingIn;
            origin = position;
        }

        public virtual float[] GenerateOpenGLData(Vector3 playerRotation)
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

    public class Enemy(Vector3 position, Vector2 scaleIn, int textureIndexIn, int healthChangeIn, float maxHealth, bool pathfindingIn) : Entity(position, scaleIn, textureIndexIn, healthChangeIn, pathfindingIn, EntityType.Enemy)
    {
        float health = maxHealth;
        public new bool alive = true;
        public override void Tick(Vector3 playerPosition, ref float health, float deltaTime)
        {
            if (!alive)
            {
                return;
            }
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

            Position += directionVector * Game.speed * deltaTime / 4;

            if (float.IsNaN(Position.X))
            {
                Console.WriteLine();
            }
            if ((Position - playerPosition).LengthSquared < 0.1f)
            {
                Position = origin;
            }
        }

        public override void HandleClickedOn(float attackDamage)
        {
            base.HandleClickedOn(attackDamage);

            health -= attackDamage;
            if (health <= 0)
            {
                alive = false;
            }
        }

        public override bool CheckClickedCollision(Vector3 input, float stepScale, out float distanceFrom)
        {
            float distanceAllowedXZ = new Vector2(scale.X / 2, scale.X / 2).LengthSquared;
            float distanceAllowedY = scale.Y;
            Vector3 VectorDistanceFrom = Position - input;
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

        public override float[] GenerateOpenGLData(Vector3 playerRotation)
        {
            if (!alive)
            {
                return [];
            }

            return base.GenerateOpenGLData(playerRotation);
        }
    }

    public class Item(Vector3 position, Vector2 scaleIn, int textureIndexIn, int healthChangeIn, bool pathfindingIn) : Entity(position, scaleIn, textureIndexIn, healthChangeIn, pathfindingIn, EntityType.Item)
    {
        public new bool alive = true;
        public override void Tick(Vector3 playerPosition, ref float health, float deltaTime)
        {
            health += healthChange;
        }
    }

}