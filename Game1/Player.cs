using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Game1
{
    public class Player
    {
        public Vector3 Position, Scale, hCollisionScale, vCollisionScale;
        public Vector3 upRotation, moveRotation;
        public float Health;
        bool EscapeKeyState;
        float yVelocity = 0f;
        int weaponIndex = 0;
        Weapon[] weapons = [new Weapon(10, 20, Weapon.WeaponTypes.Pistol), new Weapon(50, 8, Weapon.WeaponTypes.Shotgun),
                            new Weapon(30, 30, Weapon.WeaponTypes.Rifle), new Weapon(100, 3, Weapon.WeaponTypes.RPG)];

        public Player(Vector3 positionIn, Vector3 scaleIn, Vector3 moveRotationIn, float healthIn)
        {
            Position = positionIn;
            Scale = scaleIn;
            hCollisionScale = (Scale.X, Scale.Y * 0.9f, Scale.Z);
            vCollisionScale = (Scale.X * 0.9f, Scale.Y, Scale.Z * 0.9f);
            moveRotation = moveRotationIn;
            Health = healthIn;
            upRotation = new(0);
        }

        public void Input_Tick(Game game, KeyboardState keyboardState, MouseState mouseState, ref CursorState cursorState, ref Level levelStore, float deltaTime)
        {
            Vector3 tempZ = new(0), tempX = new(0), tempY = new(0);
            bool jumping = false;

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                game.Close();
            }

            // input handler (rotation using euler angles)
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                moveRotation.Y += (float)(Math.PI / 600.0);
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                moveRotation.Y -= (float)(Math.PI / 600.0);
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                upRotation.X += (float)(Math.PI / 600.0);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                upRotation.X -= (float)(Math.PI / 600.0);
            }



            if (keyboardState.IsKeyDown(Keys.Backspace))
            {
                if (!EscapeKeyState)
                {
                    EscapeKeyState = true;
                    if (cursorState == CursorState.Normal)
                    {
                        cursorState = CursorState.Grabbed;
                    }
                    else
                    {
                        cursorState = CursorState.Normal;
                    }

                }
            }
            else if (keyboardState.IsKeyReleased(Keys.Backspace))
            {
                if (EscapeKeyState)
                {
                    EscapeKeyState = false;
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

            if (mouseState.IsButtonPressed(MouseButton.Button1))
            {
                RaycastToObject(ref levelStore);
            }

            if (upRotation.X < -Math.PI * 0.45f)
            {
                upRotation.X = -(float)(Math.PI * 0.45f);
            }
            else if (upRotation.X > Math.PI * 0.45f)
            {
                upRotation.X = (float)(Math.PI * 0.45f);
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

            /*if (keyboardState.IsKeyDown(Keys.E))
            {
                tempY.Y += Game.speed * deltaTime;
            }
            if (keyboardState.IsKeyDown(Keys.Q))
            {
                tempY.Y -= Game.speed * deltaTime;
            }*/

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
                else
                {
                    levelObject.Tick(Position, ref Health, deltaTime);
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

        public void RaycastToObject(ref Level level)
        {
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
            float closestDistance, currentDistance;

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
                    if (level.levelObjects[i].CheckClickedCollision(checkPosition, stepScale, out currentDistance))
                    {
                        if (currentDistance < closestDistance)
                        {
                            closestObject = i;
                        }
                    }
                }

                if (closestObject != -1)
                {
                    validRaycast = true;
                    level.levelObjects[closestObject].HandleClickedOn(weapons[weaponIndex].Shoot());
                }


                length = (float.Min(float.Min(tMaxX, tMaxY), tMaxZ) * direction).Length;

            } while (!validRaycast && length <= 10);
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
        int magSize;
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

        public float Shoot()
        {
            if (currentMagUsage != 0)
            {
                currentMagUsage--;
                return attackDamage;
            }
            else
            {
                return 0;
            }
        }

        public void Reload()
        {
            while (currentMagUsage != magSize && availableAmmo > 0)
            {
                currentMagUsage++;
            }
        }

        public void CollectAmmo(int collected)
        {
            availableAmmo += collected;
        }

    }
}