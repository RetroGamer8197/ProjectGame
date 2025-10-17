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
        protected bool alive = true;
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
        public virtual bool getAliveState()
        {
            return alive;
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
        public float maxHealth = maxHealth;

        public override void Tick(Vector3 playerPosition, ref float health, float deltaTime, ref Level level, ref Player player)
        {
            if (!alive)
            {
                return;
            }
            Vector3 directionVector = (Position.X - playerPosition.X, 0, Position.Z - playerPosition.Z);

            if (RaycastToPlayer(playerPosition, ref level) || (directionVector.LengthSquared > 25))
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
                if (!player.Invincibility)
                {
                    player.Health -= 5;
                    player.Invincibility = true;
                    player.InvincibilityTimer = 0.5f;
                }
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

        public bool RaycastToPlayer(Vector3 playerPosition, ref Level level)
        {
            float tMaxX, tMaxY, tMaxZ, tDeltaX, tDeltaY, tDeltaZ;
            float stepScale = 1 / 100f;
            bool validRaycast = false;
            Vector3 direction = Vector3.Normalize(playerPosition - Position);
            float length;
            float stepX = float.Sign(direction.X) * stepScale, stepY = float.Sign(direction.Y) * stepScale, stepZ = float.Sign(direction.Z) * stepScale;
            Vector3 checkPosition = Player.FloorPosition(Position, stepScale);
            Vector3 RealPosition = Position;
            int closestObject;
            float closestDistance;

            float modulus = 1;

            tDeltaX = Math.Abs(stepScale / direction.X);
            tDeltaY = Math.Abs(stepScale / direction.Y);
            tDeltaZ = Math.Abs(stepScale / direction.Z);

            if (checkPosition.X - RealPosition.X == 0 && checkPosition.Y - RealPosition.Y == 0 && checkPosition.Y - RealPosition.Y == 0)
            {
                tMaxX = tDeltaX;
                tMaxY = tDeltaY;
                tMaxZ = tDeltaZ;
            }
            else
            {
                tMaxX = Math.Abs((checkPosition.X + stepX - RealPosition.X) % modulus / direction.X);
                tMaxY = Math.Abs((checkPosition.Y + stepY - RealPosition.Y) % modulus / direction.Y);
                tMaxZ = Math.Abs((checkPosition.Z + stepZ - RealPosition.Z) % modulus / direction.Z);
            }

            do
            {
                if (tMaxX < tMaxY)
                {
                    if (tMaxX < tMaxZ)
                    {
                        tMaxX += tDeltaX;
                        checkPosition.X += stepX;
                    }
                    else
                    {
                        tMaxZ += tDeltaZ;
                        checkPosition.Z += stepZ;
                    }
                }
                else
                {
                    if (tMaxY < tMaxZ)
                    {
                        tMaxY += tDeltaY;
                        checkPosition.Y += stepY;
                    }
                    else
                    {
                        tMaxZ += tDeltaZ;
                        checkPosition.Z += stepZ;
                    }
                }

                closestObject = -1;
                closestDistance = float.MaxValue;
                for (int i = 0; i < level.levelObjects.Count; i++)
                {
                    if (level.levelObjects[i].CheckClickedCollision(checkPosition, stepScale, out float currentDistance))
                    {
                        if (currentDistance < closestDistance)
                        {
                            closestObject = i;
                        }
                        if (closestObject != -1)
                        {
                            validRaycast = true;
                        }
                    }
                }

                length = (float.Min(float.Min(tMaxX, tMaxY), tMaxZ) * direction).Length;

            } while (!validRaycast && length <= (playerPosition - Position).Length);

            return validRaycast;
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

    public class Item(Vector3 position, Vector2 scaleIn, int textureIndexIn, int healthChangeIn, bool pathfindingIn, Item.ItemsEnum returnItemIn) : Entity(position, scaleIn, textureIndexIn, healthChangeIn, pathfindingIn, EntityType.Item)
    {
        public enum ItemsEnum
        {
            RedKeycard, GreenKeycard, BlueKeycard, YellowKeycard, SmallMedkit, LargeMedkit, SmallAmmo, ShellPack, MediumAmmo, LargeAmmo,
        }

        public ItemsEnum returnItem = returnItemIn;

        public override void Tick(Vector3 playerPosition, ref float health, float deltaTime, ref Level level, ref Player player)
        {
            if (!alive)
            {
                return;
            }

            if (float.IsNaN(Position.X))
            {
                Console.WriteLine();
            }
            if ((Position - playerPosition).LengthSquared < 0.1f)
            {
                switch (returnItem)
                {
                    case ItemsEnum.RedKeycard:
                        player.Inventory.Add(new HeldItem() { color = HeldItem.Colors.Red, itemType = HeldItem.ItemTypes.Keycard });
                        alive = false;
                        break;
                    case ItemsEnum.GreenKeycard:
                        player.Inventory.Add(new HeldItem() { color = HeldItem.Colors.Green, itemType = HeldItem.ItemTypes.Keycard });
                        alive = false;
                        break;
                    case ItemsEnum.BlueKeycard:
                        player.Inventory.Add(new HeldItem() { color = HeldItem.Colors.Blue, itemType = HeldItem.ItemTypes.Keycard });
                        alive = false;
                        break;
                    case ItemsEnum.YellowKeycard:
                        player.Inventory.Add(new HeldItem() { color = HeldItem.Colors.Yellow, itemType = HeldItem.ItemTypes.Keycard });
                        alive = false;
                        break;
                    case ItemsEnum.SmallMedkit:
                        if (player.Health < 100f)
                        {
                            player.Health = Math.Clamp(player.Health + 10, 0, 100);
                            alive = false;
                        }
                        break;
                    case ItemsEnum.LargeMedkit:
                        if (player.Health < 100f)
                        {
                            player.Health = Math.Clamp(player.Health + 30, 0, 100);
                            alive = false;
                        }
                        break;
                }
            }
        }
    }

}