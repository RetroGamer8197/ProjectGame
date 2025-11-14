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
        public bool Invincibility = false;
        public float InvincibilityTimer = 0.0f;
        float yVelocity = 0f;
        public int weaponIndex = 0;

        Weapon[] weapons = [new Weapon(10, 20, Weapon.WeaponTypes.Pistol), new Weapon(50, 8, Weapon.WeaponTypes.Shotgun),
                            new Weapon(30, 30, Weapon.WeaponTypes.Rifle), new Weapon(100, 3, Weapon.WeaponTypes.RPG)];

        public Player(Vector3 positionIn, Vector3 scaleIn, Vector3 moveRotationIn, float healthIn)
        {
            Position = positionIn;
            Scale = scaleIn;
            hCollisionScale = (Scale.X, Scale.Y * 0.6f, Scale.Z);
            vCollisionScale = (Scale.X * 0.6f, Scale.Y, Scale.Z * 0.6f);
            moveRotation = moveRotationIn;
            Health = healthIn;
            upRotation = new(0);
        }

        public void Input_Tick(Game game, KeyboardState keyboardState, MouseState mouseState, ref CursorState cursorState, ref Level levelStore, float deltaTime, ref Renderer renderer, ref List<HUD_Element> hud_elements, ref Game.GameState gameState)
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
                RaycastToObject(ref levelStore, ref renderer, false, ref hud_elements);
            }

            if (keyboardState.IsKeyPressed(Keys.E))
            {
                RaycastToObject(ref levelStore, ref renderer, true, ref hud_elements);
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


            // collision detection
            bool collidedZ = false;
            bool collidedX = false;
            bool collidedXZ = false;
            bool collidedY = false;
            bool grounded = false;

            foreach (Object levelObject in levelStore.levelObjects)
            {
                if (levelObject.objectType != Object.ObjectType.Entity)
                {
                    // to simplify this part of the code, CheckCollision is a virtual function of Object and is overridden in inheriting classes
                    collidedZ |= levelObject.CheckCollision(Position + (Vector3.UnitZ * (tempX.Z + tempZ.Z)), hCollisionScale);
                    collidedX |= levelObject.CheckCollision(Position + (Vector3.UnitX * (tempX.X + tempZ.X)), hCollisionScale);
                    collidedXZ |= levelObject.CheckCollision(Position + (tempX + tempZ), hCollisionScale);
                    collidedY |= levelObject.CheckCollision(Position + tempX + tempZ + tempY, vCollisionScale);
                    if (levelObject.objectType == Object.ObjectType.Cube)
                    {
                        Cube C = (Cube)levelObject;
                        if (C.centre.Y < Position.Y && collidedY)
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

        public void RaycastToObject(ref Level level, ref Renderer renderer, bool interactType, ref List<HUD_Element> hud_elements)
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
                        level.levelObjects[closestObject].HandleInteract(ref Inventory, ref hud_elements);
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

        /*
        private Vector3 Position;
        private Vector3 playerScale = (0.25f, 0.5f, 0.25f);
        private Vector3 moveRotation;
        private Vector3 upRotation;
        private Vector3 front;
        private float health = 100;
        */
    }

    public class Weapon
    {
        float attackDamage;
        public readonly int magSize;
        int currentMagUsage;
        private int availableAmmo;
        public int UITextureIndex;

        public enum WeaponTypes
        {
            Pistol, Shotgun, Rifle, RPG
        }

        public WeaponTypes weaponType;

        public Weapon(float _attackDamage, int _magSize, WeaponTypes _weaponType)
        {
            attackDamage = _attackDamage;
            magSize = _magSize;
            weaponType = _weaponType;
            availableAmmo = 10000;
        }

        public float Shoot(ref Renderer renderer)
        {
            if (currentMagUsage != 0)
            {
                currentMagUsage--;
                renderer.FlashColorTint((0.99f, 0.92f, 0.43f, 0.0f));
                return attackDamage;
            }
            else
            {
                return 0;
            }
        }

        public float GetFullFraction()
        {
            return ((float)currentMagUsage) / magSize;
        }

        public void Reload()
        {
            while (currentMagUsage != magSize && availableAmmo > 0)
            {
                currentMagUsage++;
                availableAmmo--;
            }
        }

        public void CollectAmmo(int collected)
        {
            availableAmmo += collected;
        }

    }

    public class HeldItem
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