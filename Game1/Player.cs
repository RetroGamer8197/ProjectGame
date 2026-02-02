using System.Data.Common;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Game1
{
    public class Player
    {
        public Vector3 Position, Scale, hCollisionScale, vCollisionScale;
        public Vector3 upRotation, moveRotation;
        public List<HeldItem> Inventory = [];
        public float Health;
        public float Armor;
        public bool Invincibility = false;
        public float InvincibilityTimer = 0.0f;
        float yVelocity = 0f;
        public int weaponIndex = 0;
        private float floatTime = 0f;

        Weapon[] weapons = [new Weapon(10, 20, 1f, 0.1f, Weapon.WeaponTypes.Pistol),  new Weapon(50, 8, 3f, 0.3f, Weapon.WeaponTypes.Shotgun),
                            new Weapon(30, 30, 2f, 0.15f, Weapon.WeaponTypes.Rifle),   new Weapon(100, 3, 5f, 1f, Weapon.WeaponTypes.RPG)];

        public Player(Vector3 positionIn, Vector3 scaleIn, Vector3 moveRotationIn, float healthIn)
        {
            Position = positionIn;
            Scale = scaleIn;
            hCollisionScale = (Scale.X, Scale.Y * 0.9f, Scale.Z);
            vCollisionScale = (Scale.X * 0.6f, Scale.Y, Scale.Z * 0.6f);
            moveRotation = moveRotationIn;
            Health = healthIn;
            Armor = 0;
            upRotation = new(0);
        }

        public void NewLevel()
        {
            Position = (0, 0, 0);
            upRotation = new(0);
            moveRotation = (0, (float)Math.PI, 0);
            Inventory = [];
        }

        public void LevelReset()
        {
            NewLevel();
            Health = 100;
            Armor = 0;

            weapons = [ new Weapon(10, 20, 1f, 0.1f, Weapon.WeaponTypes.Pistol),  new Weapon(50, 8, 3f, 0.3f, Weapon.WeaponTypes.Shotgun),
                            new Weapon(30, 30, 2f, 0.15f, Weapon.WeaponTypes.Rifle),   new Weapon(100, 3, 5f, 1f, Weapon.WeaponTypes.RPG)];
        }

        public void Damage(float damageIn)
        {
            if (!Invincibility)
            {
                Health -= Math.Clamp(damageIn - (Armor / 5), 0, 100);
                Armor = Math.Clamp(Armor - (damageIn / 5), 0, 100);
                Invincibility = true;
                InvincibilityTimer = 0.5f;
            }
        }
        /*



            NEED TO UPDATE



        */
        public void Input_Tick(Game game, KeyboardState keyboardState, MouseState mouseState, ref CursorState cursorState, ref Level levelStore, float deltaTime, ref Renderer renderer, ref HUD HUD_Object, ref Game.GameState gameState)
        {
            Vector3 tempZ = new(0), tempX = new(0), tempY = new(0);
            bool jumping = false;
            if (Invincibility)
            {
                InvincibilityTimer -= deltaTime;
            }
            if (InvincibilityTimer < 0.0f)
            {
                Invincibility = false;
            }

            // very long delta times, such as slow frames or debugging causes objects to fly out of the level so clamping the delta time
            // means this won't happen
            deltaTime = Math.Clamp(deltaTime, 0.0f, 0.1f);
            
            if (keyboardState.IsKeyPressed(Keys.Escape))
            {
                gameState = Game.GameState.Pause;
            }

            // handles changing weapon
            if (keyboardState.IsKeyPressed(Keys.D1))
            {
                weaponIndex = 0;
            }
            if (keyboardState.IsKeyPressed(Keys.D2))
            {
                weaponIndex = 1;
            }
            if (keyboardState.IsKeyPressed(Keys.D3))
            {
                weaponIndex = 2;
            }
            if (keyboardState.IsKeyPressed(Keys.D4))
            {
                weaponIndex = 3;
            }

            // multiplying by delta time unhooks movement and turn speed from the frame rate, meaning slow frame rates will not make the game itself feel slow

            // input handler (rotation using euler angles)
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                moveRotation.Y += (float)(Math.PI / 2) * deltaTime;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                moveRotation.Y -= (float)(Math.PI / 2) * deltaTime;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                upRotation.X += (float)(Math.PI / 2) * deltaTime;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                upRotation.X -= (float)(Math.PI / 2) * deltaTime;
            }

            if (keyboardState.IsKeyPressed(Keys.Backspace))
            {
                if (cursorState == CursorState.Normal)
                {
                    cursorState = CursorState.Grabbed;
                }
                else
                {
                    cursorState = CursorState.Normal;
                }
            }

            if (keyboardState.IsKeyPressed(Keys.R))
            {
                weapons[weaponIndex].Reload();
            }

            if (mouseState.Delta != (0, 0) && cursorState == CursorState.Grabbed)
            {
                moveRotation.Y += (float)(mouseState.Delta.X * Math.PI / game.WINDOW_WIDTH / 1000);
                upRotation.X += (float)(mouseState.Delta.Y * Math.PI / game.WINDOW_WIDTH / 1000);
            }

            if ((mouseState.IsButtonPressed(MouseButton.Button1) && cursorState == CursorState.Grabbed) || keyboardState.IsKeyPressed(Keys.LeftAlt))
            {
                RaycastToObject(ref levelStore, ref renderer, false, ref HUD_Object);
            }

            if (keyboardState.IsKeyPressed(Keys.E))
            {
                RaycastToObject(ref levelStore, ref renderer, true, ref HUD_Object);
            }

            if (upRotation.X < -Math.PI * 0.499f)
            {
                upRotation.X = -(float)(Math.PI * 0.499f);
            }
            else if (upRotation.X > Math.PI * 0.499f)
            {
                upRotation.X = (float)(Math.PI * 0.499f);
            }

            Vector3 front3 = Matrix3.CreateRotationY(moveRotation.Y) * new Vector3(0f, 0f, -1f);

            // player input handler (movement)

            if (keyboardState.IsKeyDown(Keys.W))
            {
                tempZ += front3 * Game.speed * deltaTime;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                tempZ -= front3 * Game.speed * deltaTime;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                tempX += Vector3.Normalize(Vector3.Cross(front3, (0, 1, 0))) * Game.speed * deltaTime;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                tempX -= Vector3.Normalize(Vector3.Cross(front3, (0, 1, 0))) * Game.speed * deltaTime;
            }

            tempY.Y = yVelocity * deltaTime;

            NewCollision(tempX, tempY, tempZ, ref levelStore, deltaTime, ref jumping, ref keyboardState);

            foreach (Weapon weapon in weapons)
            {
                weapon.TimerTick(deltaTime);
            }
            
        }
        /*



            NEED TO UPDATE



        */
        
        private void NewCollision(Vector3 tempX, Vector3 tempY, Vector3 tempZ, ref Level levelStore, float deltaTime, ref bool jumping, ref KeyboardState keyboardState)
        {
            bool collidedZ = false;
            bool collidedX = false;
            bool collidedXZ = false;
            bool collidedUpY = false;
            bool collidedDownY = false;
            bool grounded = false;

            foreach (Object levelObject in levelStore.levelObjects)
            {
                if (levelObject.objectType != Object.ObjectType.Entity)
                {
                    // to simplify this part of the code, CheckCollision is a virtual function of Object and is overridden in inheriting classes
                    collidedZ       |= levelObject.CheckCollision(Position + (Vector3.UnitZ * (tempX.Z + tempZ.Z)), hCollisionScale);
                    collidedX       |= levelObject.CheckCollision(Position + (Vector3.UnitX * (tempX.X + tempZ.X)), hCollisionScale);
                    collidedXZ      |= levelObject.CheckCollision(Position + (tempX + tempZ), hCollisionScale);
                    collidedUpY     |= levelObject.CheckCollision(Position + tempX + tempZ + tempY + (Vector3.UnitY * vCollisionScale.Y * 0.05f), vCollisionScale);
                    collidedDownY   |= levelObject.CheckCollision(Position + tempX + tempZ + tempY - (Vector3.UnitY * vCollisionScale.Y * 0.05f), vCollisionScale);
                    
                    if (levelObject.objectType == Object.ObjectType.Cube)
                    {
                        Cube C = (Cube)levelObject;
                        if (C.centre.Y < Position.Y && collidedDownY)
                        {
                            grounded = true;
                        }
                    }
                }

            }

            if (grounded && !jumping)
            {
                yVelocity = 0;
                tempY.Y = 0f;
            } else if(collidedUpY)
            {
                if (floatTime > 0.2f)
                {
                    yVelocity = 0;
                    yVelocity -= 9f * deltaTime;
                    floatTime = 0f;
                }
                else
                {
                    yVelocity = 0;
                    floatTime += deltaTime;
                }
                
                
            }
            else if (jumping && grounded)
            {
                Position.Y += yVelocity * deltaTime;
            }
            else
            {
                yVelocity -= 9f * deltaTime;
                Position.Y += yVelocity * deltaTime;
            }

            if (!collidedXZ && !collidedX && !collidedZ)
            {

            }
            else if (!collidedZ && collidedX)
            {
                tempX.X = 0;
                tempZ.X = 0;
            }
            else if (!collidedX && collidedZ)
            {
                tempX.Z = 0;
                tempZ.Z = 0;
            }
            else
            {
                tempX = (0, 0, 0);
                tempZ = (0, 0, 0);
            }

            Position += tempZ + tempX;

            if (keyboardState.IsKeyDown(Keys.Space) && grounded)
            {
                yVelocity = 4f;
            }
        }

        public float GetCurrentWeaponMagUsage()
        {
            return weapons[weaponIndex].GetFullFraction();
        }

        public string GetCurrentWeaponUsageString()
        {
            return weapons[weaponIndex].currentMagUsage + "/" + weapons[weaponIndex].GetAvailableAmmo();
        }

        public bool CollectAmmo(int quantity, Item.ItemsEnum ammoType)
        {
            switch (ammoType)
            {
                case Item.ItemsEnum.SmallAmmo:
                    weapons[0].CollectAmmo(quantity);
                    return true;
                case Item.ItemsEnum.ShellPack:
                    weapons[1].CollectAmmo(quantity);
                    return true;
                case Item.ItemsEnum.MediumAmmo:
                    weapons[2].CollectAmmo(quantity);
                    return true;
                case Item.ItemsEnum.LargeAmmo:
                    weapons[3].CollectAmmo(quantity);
                    return true;
            }
            return false;
        }

        public void RaycastToObject(ref Level level, ref Renderer renderer, bool interactType, ref HUD HUD_Object)
        {
            // interact type is true if it is an interaction and false if it is an attack
            float tMaxX, tMaxY, tMaxZ, tDeltaX, tDeltaY, tDeltaZ;
            float stepScale = 1 / 100f;
            bool validRaycast = false;
            Vector3 direction = Matrix3.CreateFromQuaternion(Quaternion.FromEulerAngles(upRotation + moveRotation)) * -Vector3.UnitZ * stepScale;
            Vector3 CameraPosition = Position + (Vector3.UnitY * Scale.Y / 2);
            float length;
            float stepX = float.Sign(direction.X) * stepScale, stepY = float.Sign(direction.Y) * stepScale, stepZ = float.Sign(direction.Z) * stepScale;
            Vector3 checkPosition = FloorPosition(CameraPosition, stepScale);
            Vector3 RealPosition = CameraPosition;
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
                            Console.WriteLine();
                        }
                    }
                }

                if (closestObject != -1)
                {
                    validRaycast = true;
                    if (interactType)
                    {
                        Player ptemp = this;
                        level.levelObjects[closestObject].HandleInteract(ref Inventory, ref HUD_Object);
                    }
                    else
                    {
                        level.levelObjects[closestObject].HandleClickedOn(weapons[weaponIndex].Shoot(ref renderer));
                    }
                }


                length = (float.Min(float.Min(tMaxX, tMaxY), tMaxZ) * direction).Length;

            } while (!validRaycast && length <= 10);
            if (!validRaycast)
            {
                weapons[weaponIndex].Shoot(ref renderer);
            }
        }

        public static Vector3 FloorPosition(Vector3 position, float scale)
        {

            position /= scale;

            if (position.X < 0)
            {
                position.X--;
            }
            if (position.Z < 0)
            {
                position.Z--;
            }

            return position.Floor() * scale;

        }
    }

    public class Weapon
    {
        float attackDamage;
        public readonly int magSize;
        public int currentMagUsage;
        private int availableAmmo;
        public int UITextureIndex;
        public readonly float reloadTime;
        public readonly float shotTime;
        public float reloadTimer, shotTimer;
        public bool reloading;

        public enum WeaponTypes
        {
            Pistol, Shotgun, Rifle, RPG
        }

        public WeaponTypes weaponType;

        public Weapon(float _attackDamage, int _magSize, float _reloadTime, float _shotTime, WeaponTypes _weaponType)
        {
            attackDamage = _attackDamage;
            magSize = _magSize;
            weaponType = _weaponType;
            availableAmmo = _magSize;
            reloadTime = _reloadTime;
            shotTime = _shotTime;
            reloadTimer = -1f;
            shotTimer = -1f;
            reloading = false;
            Reload();
        }

        public float Shoot(ref Renderer renderer)
        {
            if (currentMagUsage != 0 && reloadTimer <= 0 && shotTimer <= 0)
            {
                currentMagUsage--;
                renderer.FlashColorTint((0.99f, 0.92f, 0.43f, 0.0f));
                shotTimer = shotTime;
                return attackDamage;
            }
            else
            {
                return 0;
            }
        }

        public float GetFullFraction()
        {
            if (reloadTimer > 0)
            {
                return Math.Clamp((reloadTime - reloadTimer) / reloadTime, 0, 1);
            }
            else if (shotTimer > 0)
            {
                return Math.Clamp(((float)currentMagUsage - 1 + ((shotTime - shotTimer) / shotTime)) / magSize, 0, 1);
            }
            else 
            {
                return Math.Clamp(((float)currentMagUsage) / magSize, 0, 1);
            }
        }

        public void Reload()
        {
            int increasedAmmo = 0;
            while (currentMagUsage != magSize && availableAmmo > 0)
            {
                currentMagUsage++;
                increasedAmmo++;
                availableAmmo--;
                shotTimer = -1f;
                
            }
            reloadTimer = reloadTime * ((float)increasedAmmo / magSize);
        }

        public void TimerTick(float deltaTime)
        {
            if (reloadTimer > 0)
            {
                reloadTimer -= deltaTime;
            }
            if (shotTimer > 0)
            {
                shotTimer -= deltaTime;
            }
        }

        public void CollectAmmo(int collected)
        {
            availableAmmo += collected;
        }

        public int GetAvailableAmmo()
        {
            return availableAmmo;
        }

    }

    public struct HeldItem
    {
        public enum Colors
        {
            Red, Green, Blue, Yellow, None
        }
        public enum ItemTypes
        {
            Keycard, 
        }
        public Colors color;
        public ItemTypes itemType;

    }
}