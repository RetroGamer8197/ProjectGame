using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics.CodeAnalysis;
using System.Data.Common;

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

        public override float[] GenerateFloatData(Vector3 playerRotation)
        {
            List<float> openGLData = [];

            if (!alive)
            {
                return [];
            }

            Vector3 front = Matrix3.CreateFromQuaternion(Quaternion.FromEulerAngles(playerRotation)) * new Vector3(0.0f, 0.0f, 1.0f);

            Plane entitySprite = new(Position, front, scale.X, scale.Y, textureIndex, 1.0f);

            return entitySprite.GenerateFloatData(playerRotation);
        }
    }

    public class Enemy(Vector3 position, Vector2 scaleIn, int textureIndexIn, int healthChangeIn, float maxHealth, bool pathfindingIn) : Entity(position, scaleIn, textureIndexIn, healthChangeIn, pathfindingIn, EntityType.Enemy)
    {
        float health = maxHealth;
        public float maxHealth = maxHealth;
        readonly float startingActionTimer = new Random().Next(3, 7);
        float actionTimer = 5.0f;
        public override void Tick(Vector3 playerPosition, ref float health, float deltaTime, ref Level level, ref Player player)
        {
            if (!alive)
            {
                return;
            }
            Vector3 directionVector = (playerPosition.X - Position.X, 0, playerPosition.Z - Position.Z);

            // check if the enemy has line of sight with the player
            if (!RaycastToPlayer(playerPosition, ref level) || !pathfinding)// || (directionVector.LengthSquared > 25))
            {
                directionVector = (0, 0, 0); //(new Random().Next(-5, 5), 0, new Random().Next(-5, 5));
            }
            else
            {

            }

            if (directionVector.LengthSquared > 0)
            {
                directionVector.Normalize();
            }

            Position += directionVector * Game.speed * deltaTime / 4;

            bool collided = false;
            foreach (Object o in level.levelObjects)
            {
                collided |= CheckCollision(Position, (scale.X, scale.Y * 0.9f, scale.X));
            }
            if (collided)
            {
                Position -= directionVector * Game.speed * deltaTime / 4;
            }

            if (float.IsNaN(Position.X))
            {
                Console.WriteLine();
            }
            if ((Position - playerPosition).LengthSquared < 0.1f)
            {
                /*Position = origin;
                if (!player.Invincibility)
                {
                    player.Health -= 5;
                    player.Invincibility = true;
                    player.InvincibilityTimer = 0.5f;
                }*/
            }

            if (actionTimer <= 0 && directionVector != (0,0,0))
            {
                Random randomNum = new Random();
                if (randomNum.Next(0, 300) > 250)
                {
                    actionTimer = startingActionTimer;
                    level.temporaryObjects.Add(new Projectile(Position + (Vector3.UnitY * (scale.Y / 5)), (0.1f, 0.1f), 7, healthChange, playerPosition - Position, 3.0f));
                }
            } else
            {
                actionTimer -= deltaTime;
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
            Vector3 direction = Vector3.Normalize(playerPosition - Position);
            float stepX = float.Sign(direction.X) * stepScale, stepY = float.Sign(direction.Y) * stepScale, stepZ = float.Sign(direction.Z) * stepScale;
            Vector3 checkPosition = Player.FloorPosition(Position, stepScale);
            Vector3 RealPosition = Position;

            float modulus = 1;

            float maxDistance = (RealPosition - playerPosition).Length;
            if (maxDistance > 5)
            {
                return false;
            }

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

            while ((checkPosition - RealPosition).Length < maxDistance)
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

                float distance;
                foreach (Object O in level.levelObjects)
                {
                    if (O.objectType != ObjectType.Entity && O.CheckClickedCollision(checkPosition, stepScale, out distance) && distance < 0.05f)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override float[] GenerateFloatData(Vector3 playerRotation)
        {
            if (!alive)
            {
                return [];
            }

            return base.GenerateFloatData(playerRotation);
        }
    }

    public class Projectile(Vector3 position, Vector2 scaleIn, int textureIndexIn, int healthChangeIn, Vector3 directionToTravel, float speedIn) : Enemy(position, scaleIn, textureIndexIn, healthChangeIn, 1, false)
    {
        private float speed = speedIn;
        public override void HandleClickedOn(float attackDamage)
        {

        }

        public override bool CheckClickedCollision(Vector3 input, float stepScale, out float distanceFrom)
        {
            distanceFrom = float.MaxValue;
            return false;
        }

        public override void Tick(Vector3 playerPosition, ref float health, float deltaTime, ref Level level, ref Player player)
        {
            if (alive)
            {
                Position += Vector3.Normalize(directionToTravel) * deltaTime * speed;

                foreach (Object O in level.levelObjects)
                {
                    if (O.CheckCollision(Position, (scale.X, scale.Y, scale.X)))
                    {
                        alive = false;
                    }
                }

                if ((Position - playerPosition).LengthSquared < 0.1f)
                {
                    alive = false;
                    player.Damage(healthChange);
                }
            }
        }
    }

    public class Item(Vector3 position, Vector2 scaleIn, int textureIndexIn, int healthChangeIn, bool pathfindingIn, Item.ItemsEnum returnItemIn) : Entity(position, scaleIn, textureIndexIn, healthChangeIn, pathfindingIn, EntityType.Item)
    {
        public enum ItemsEnum
        {
            RedKeycard, GreenKeycard, BlueKeycard, YellowKeycard, SmallMedkit, LargeMedkit, SmallAmmo, ShellPack, MediumAmmo, LargeAmmo, ArmorPatch, ArmorMetal
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
                    case ItemsEnum.SmallAmmo:
                        if (player.CollectAmmo(healthChange, ItemsEnum.SmallAmmo))
                        {
                            alive = false;
                        }
                        break;
                    case ItemsEnum.ShellPack:
                        if (player.CollectAmmo(healthChange, ItemsEnum.ShellPack))
                        {
                            alive = false;
                        }
                        break;
                    case ItemsEnum.MediumAmmo:
                        if (player.CollectAmmo(healthChange, ItemsEnum.MediumAmmo))
                        {
                            alive = false;
                        }
                        break;
                    case ItemsEnum.LargeAmmo:
                        if (player.CollectAmmo(healthChange, ItemsEnum.LargeAmmo))
                        {
                            alive = false;
                        }
                        break;
                    case ItemsEnum.ArmorPatch:
                        if (player.Armor < 100f)
                        {
                            player.Armor = Math.Clamp(player.Armor + 10, 0, 100);
                            alive = false;
                        }
                        break;
                    case ItemsEnum.ArmorMetal:
                        if (player.Armor < 100f)
                        {
                            player.Armor = Math.Clamp(player.Armor + 25, 0, 100);
                            alive = false;
                        }
                        break;
                }
            }
        }
    }

}