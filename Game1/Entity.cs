using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics.CodeAnalysis;
using System.Data.Common;
using System.ComponentModel;

namespace Game1
{
public class Entity : Object
    {
        public enum EntityType
        {
            None, Enemy, Item
        }
        public Vector3 Position;
        public readonly Vector3 origin;
        public readonly Vector2 scale;
        public readonly int textureIndex, healthChange;
        public readonly bool pathfinding;
        public readonly EntityType entityType;
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
        public virtual bool GetAliveState()
        {
            return alive;
        }

        public static void LoadFromFile(out Entity entity, ref BinaryReader levelFile)
        {
            // load parameters from file
            entity = new Entity(CustomVector3.ReadVector3FromFile(ref levelFile),
                                (levelFile.ReadSingle(), levelFile.ReadSingle()), levelFile.ReadInt32(), 
                                levelFile.ReadInt32(), levelFile.ReadBoolean(), Entity.EntityType.None);
        }

        public override float[] GenerateFloatData(Vector3 playerRotation)
        {

            if (!alive)
            {
                return [];
            }

            Vector3 front = Matrix3.CreateFromQuaternion(Quaternion.FromEulerAngles(playerRotation)) * new Vector3(0.0f, 0.0f, 1.0f);

            Plane entitySprite = new(Position, front, scale.X, scale.Y, textureIndex, 1.0f);

            return entitySprite.GenerateFloatData(playerRotation);
        }

        public override void ExportToFile(ref BinaryWriter levelWriter)
        {
            // save parameters to file
            levelWriter.Write((byte)entityType);
        }
    }

    public class Enemy(Vector3 position, Vector2 scaleIn, int textureIndexIn, int healthChangeIn, float maxHealth, int projectileTextureIndex, bool pathfindingIn, float sightRangeIn) : Entity(position, scaleIn, textureIndexIn, healthChangeIn, pathfindingIn, EntityType.Enemy)
    {
        protected float health = maxHealth;
        private float sightRange = sightRangeIn;
        public float maxHealth = maxHealth;
        private readonly float startingActionTimer = new Random().Next(2, 4);
        protected private float actionTimer = 5.0f;
        protected private float animationTimer = 0.0f;
        protected private int animationFrame = 0;
        public override void Tick(float deltaTime, ref Level level, ref Player player, ref HUD HUD_Object)
        {
            if (!alive) // do not continue if the entity is no longer alive
            {
                return;
            }
            Vector3 directionVector = (player.Position.X - Position.X, 0, player.Position.Z - Position.Z);

            bool canSeePlayer = RaycastToPlayer(player.Position, ref level);
            // check if the enemy has line of sight with the player
            if (!canSeePlayer || !pathfinding)// || (directionVector.LengthSquared > 25))
            {
                directionVector = (0, 0, 0);
            }
            
            // make unit length so speed of enemies is constant relative to delta time
            if (directionVector.LengthSquared > 0)
            {
                directionVector.Normalize();
            }

            // move relative to delta time
            Position += directionVector * Game.speed * deltaTime / 4;

            // make sure a movement doesn't collide with an object
            bool collided = false;
            foreach (Object o in level.levelObjects)
            {
                collided |= CheckCollision(Position, (scale.X, scale.Y * 0.9f, scale.X));
            }
            if (collided)
            {
                Position -= directionVector * Game.speed * deltaTime / 4;
            }

            // add randomness to actions and time them using delta time
            if (actionTimer <= 0 && directionVector != (0,0,0))
            {
                Random randomNum = new Random();
                if (randomNum.Next(0, 300) > 250)
                {
                    actionTimer = startingActionTimer;
                    level.temporaryObjects.Add(new Projectile(Position + (Vector3.UnitY * (scale.Y / 5)), (0.1f, 0.1f), projectileTextureIndex, healthChange, player.Position - (Position + (Vector3.UnitY * (scale.Y / 5))), 30f / MathF.Pow(healthChange, 1f/2f)));
                }
            } else
            {
                actionTimer -= deltaTime;
            }

            // only animate entities when they are moving and so can see the player
            if (canSeePlayer) 
            {
                animationTimer += deltaTime;
                if (animationTimer > 0.25f)
                {
                    animationTimer = 0f;
                    animationFrame += 1;
                    if (animationFrame > 1)
                    {
                        animationFrame = 0;
                    }
                }
            }
        }

        public static void LoadFromFile(out Enemy entity, ref BinaryReader levelFile)
        {
            // load parameters from file
            entity = new Enemy(CustomVector3.ReadVector3FromFile(ref levelFile),
                                    (levelFile.ReadSingle(), levelFile.ReadSingle()), levelFile.ReadInt32(), levelFile.ReadInt32(), levelFile.ReadSingle(), levelFile.ReadInt32(), levelFile.ReadBoolean(), levelFile.ReadSingle());
        }

        public override void ExportToFile(ref BinaryWriter levelWriter)
        {
            // save parameters to file
            base.ExportToFile(ref levelWriter);

            CustomVector3.WriteVector3ToFile(origin, ref levelWriter);
            levelWriter.Write(scale.X);
            levelWriter.Write(scale.Y);
            levelWriter.Write(textureIndex);
            levelWriter.Write(healthChange);
            levelWriter.Write(maxHealth);
            levelWriter.Write(projectileTextureIndex);
            levelWriter.Write(pathfinding);
            levelWriter.Write(sightRange);
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
            if (alive)
            {
                // checks a cylinder around the entity's centre point
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
            } else
            {
                // ignore if no longer alive
                distanceFrom = float.MaxValue;
                return false;
            }
            
        }

        public bool RaycastToPlayer(Vector3 playerPosition, ref Level level)
        {
            // using a voxel-based algorithm to check for line of sight. effective at short distances and enclosed spaces 
            // but not so much at longer distances. this is countered for in the level design
            float tMaxX, tMaxY, tMaxZ, tDeltaX, tDeltaY, tDeltaZ;
            float stepScale = 1 / 100f;
            Vector3 checkPosition = Player.FloorPosition(Position, stepScale);
            Vector3 direction = Vector3.Normalize(playerPosition - Position);
            float stepX = float.Sign(direction.X) * stepScale, stepY = float.Sign(direction.Y) * stepScale, stepZ = float.Sign(direction.Z) * stepScale;
            Vector3 RealPosition = Position;

            float modulus = 1;

            float maxDistance = (RealPosition - playerPosition).Length;
            if (maxDistance > sightRange)
            {
                return false;
            }

            // distance to step to the next 'gridline' on each axis

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

            // check along the line until a collision occurs or the player is reached

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

                foreach (Object O in level.levelObjects)
                {
                    if (O.objectType != ObjectType.Entity && O.CheckClickedCollision(checkPosition, stepScale, out float distance) && distance < 0.05f)
                    {
                        // allow the entity to see through other entities and to prevent collisions with itself
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

            Vector3 front = Matrix3.CreateFromQuaternion(Quaternion.FromEulerAngles(playerRotation)) * new Vector3(0.0f, 0.0f, 1.0f);

            Plane entitySprite = new(Position, front, scale.X, scale.Y, textureIndex + animationFrame, 1.0f);

            return entitySprite.GenerateFloatData(playerRotation);
        }
    }

    public class Projectile : Enemy
    {
        private readonly float speed;
        Vector3 directionToTravel, initialPosition;

        public Projectile(Vector3 position, Vector2 scaleIn, int textureIndexIn, int healthChangeIn, Vector3 _directionToTravel, float speedIn):  base(position, scaleIn, textureIndexIn, healthChangeIn, 1, -1, false, -1f)
        {
            speed = speedIn;
            directionToTravel = _directionToTravel;
            initialPosition = position;

        }
        public override void HandleClickedOn(float attackDamage)
        {

        }

        public override bool CheckClickedCollision(Vector3 input, float stepScale, out float distanceFrom)
        {
            distanceFrom = float.MaxValue;
            return false;
        }

        public override void Tick(float deltaTime, ref Level level, ref Player player, ref HUD HUD_Object)
        {
            // if the projectile is no longer alive, it will be deleted by the Game in the next frame update
            if (alive)
            {
                Position += Vector3.Normalize(directionToTravel) * deltaTime * speed * Game.speed / 2f;

                foreach (Object O in level.levelObjects)
                {
                    if (O.CheckCollision(Position, (scale.X, scale.Y, scale.X)))
                    {
                        alive = false;
                    }
                }

                if ((Position - player.Position).LengthSquared < 0.1f)
                {
                    alive = false;
                    player.Damage(healthChange);
                }
                if ((Position - initialPosition).Length > 50f)
                {
                    alive = false;
                }
            }
        }
    }

    public class AntiEnemyProjectile : Projectile
    {
        private float speed;
        Vector3 directionToTravel, initialPosition;

        public AntiEnemyProjectile(Vector3 position, Vector2 scaleIn, int textureIndexIn, int healthChangeIn, Vector3 _directionToTravel, float speedIn):  base(position, scaleIn, textureIndexIn, healthChangeIn, _directionToTravel, speedIn)
        {
            speed = speedIn;
            directionToTravel = _directionToTravel;
            initialPosition = position;

        }

        public override void Tick(float deltaTime, ref Level level, ref Player player, ref HUD HUD_Object)
        {
            if (alive)
            {
                Position += Vector3.Normalize(directionToTravel) * deltaTime * speed;

                foreach (Object O in level.levelObjects)
                {
                    if (O.objectType != ObjectType.Entity)
                    {
                        if (O.CheckCollision(Position, (scale.X, scale.Y, scale.X)))
                        {
                            alive = false;
                        }
                    } else {
                        Entity EntityO = (Entity)O;
                        if (EntityO.entityType == EntityType.Enemy)
                        {
                            if (EntityO.CheckClickedCollision(Position, scale.X / 2f, out float distanceFrom))
                            {
                                O.HandleClickedOn(healthChange);
                                alive = false;
                            }
                        }
                    }
                    
                }

                if ((initialPosition - Position).Length > 40)
                {
                    alive = false;
                }
            }
        }
    }

    public class Item(Vector3 position, Vector2 scaleIn, int textureIndexIn, int changeIn, bool pathfindingIn, Item.ItemsEnum returnItemIn) : Entity(position, scaleIn, textureIndexIn, changeIn, pathfindingIn, EntityType.Item)
    {
        public enum ItemsEnum
        {
            RedKeycard, GreenKeycard, BlueKeycard, YellowKeycard, SmallMedkit, LargeMedkit, SmallAmmo, ShellPack, MediumAmmo, LargeAmmo, ArmorPatch, ArmorMetal
        }

        public ItemsEnum returnItem = returnItemIn;

        public static void LoadFromFile(out Item entity , ref BinaryReader levelFile)
        {
            // load parameters from file
            entity = new Item(CustomVector3.ReadVector3FromFile(ref levelFile),
                                (levelFile.ReadSingle(), levelFile.ReadSingle()), levelFile.ReadInt32(), levelFile.ReadInt32(), levelFile.ReadBoolean(), (Item.ItemsEnum)levelFile.ReadByte());
        }

        public override void ExportToFile(ref BinaryWriter levelWriter)
        {
            // save parameters to file
            base.ExportToFile(ref levelWriter);

            CustomVector3.WriteVector3ToFile(origin, ref levelWriter);
            levelWriter.Write(scale.X);
            levelWriter.Write(scale.Y);
            levelWriter.Write(textureIndex);
            levelWriter.Write(healthChange);
            levelWriter.Write(pathfinding);
            levelWriter.Write((byte)returnItem);
        }

        public override void Tick(float deltaTime, ref Level level, ref Player player, ref HUD HUD_Object)
        {
            if (!alive)
            {
                return;
            }

            if (float.IsNaN(Position.X))
            {
                Console.WriteLine();
            }
            if ((Position - player.Position).LengthSquared < 0.1f)
            {
                // adds the right item to the player based on the value of its returnItem
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