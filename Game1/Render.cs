using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Game1
{

    public class Renderer
    {
        private int VertexBufferObject, VertexArrayObject;
        private double tintDuration = -1;
        private Texture levelTextureAtlas, hudAtlas, entityAtlas, enemyAtlas, mainMenuImage, font;
        public Shader levelShader;



#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Renderer(out bool success)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            success = Init();
        }
        public bool Init()
        {
            GL.ClearColor(0.094f, 0.608f, 0.8f, 1.0f);

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            // check the presence of all texture files and load all texture atlases into the VRAM
            LoadTextures(out bool success);
            if (!success)
            {
                return false;
            }
            
            levelShader = new("Shaders/level.vert", "Shaders/level.frag", out success);
            if (!success)
            {
                return false;
            }
            levelShader.Use(Matrix4.Identity, Matrix4.Identity, Matrix4.Identity);


            // when the shader handles a new coordinate, it will have 3 properties, totalling 6 floats

            int vertexLocation = GL.GetAttribLocation(levelShader.Handle, "aPosition"); 
            GL.EnableVertexAttribArray(vertexLocation); 
            // this is a vector3, starting at the index 0 
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0); 

            int textureLocation = GL.GetAttribLocation(levelShader.Handle, "aTexCoord");
            GL.EnableVertexAttribArray(textureLocation);
            // this is a vector2, starting at index 3
            GL.VertexAttribPointer(textureLocation, 2, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

            int shaderDirection = GL.GetAttribLocation(levelShader.Handle, "aShaderDir");
            GL.EnableVertexAttribArray(shaderDirection);
            // this is a float starting at index 5
            GL.VertexAttribPointer(shaderDirection, 1, VertexAttribPointerType.Float, false, 6 * sizeof(float), 5 * sizeof(float));

            // set the uniforms up for the shader
            levelShader.SetInt("tintEnable", 0);

            // enable the depth buffer for triangle sorting
            GL.Enable(EnableCap.DepthTest);

            return true;
        }

        private void LoadTextures(out bool success)
        {
            string currentFileName = "";
            success = true;
            try
            {
                currentFileName = "Textures/atlas.png";
                levelTextureAtlas = new("Textures/atlas.png", TextureUnit.Texture0, true);

                currentFileName = "Textures/hudatlas.png";
                hudAtlas = new("Textures/hudatlas.png", TextureUnit.Texture1, false);

                currentFileName = "Textures/entityAtlas.png";
                entityAtlas = new("Textures/entityatlas.png", TextureUnit.Texture2, false);

                currentFileName = "Textures/mainmenu.png";
                mainMenuImage = new("Textures/mainmenu.png", TextureUnit.Texture3, true);

                currentFileName = "Textures/font.png";
                font = new("Textures/font.png", TextureUnit.Texture4, true);

                currentFileName = "Textures/enemyAtlas.png";
                enemyAtlas = new("Textures/enemyAtlas.png", TextureUnit.Texture5, false);
            }
            catch (FileNotFoundException)
            {
                PopupWindow popup = new(500, 20, $"Missing texture files: {currentFileName}");
                popup.CenterWindow();
                popup.Run();
                Console.WriteLine("Missing texture file(s):");
                Console.WriteLine("\t" + currentFileName);
                success = false;
            }
        }

        public void RenderMainMenu()
        {
            /*
            
                UPGRADE THE MENU TO BE FANCIER

            */
            GL.Disable(EnableCap.DepthTest);

            mainMenuImage.Use(TextureUnit.Texture3);

            float[] MainMenu = [-1, -1, 0, 0, 0, 1.0f,
                                -1, 1, 0, 0, 1, 1.0f,
                                1, -1, 0, 1, 0, 1.0f,


                                -1, 1, 0, 0, 1, 1.0f,
                                1, 1, 0, 1, 1, 1.0f,
                                1, -1, 0, 1, 0, 1.0f,
                                ];
            levelShader.SetInt("textureAtlas", 3);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            RenderDataWithTransforms(MainMenu, Matrix4.Identity,Matrix4.Identity,Matrix4.Identity);

            /*
            
                UPGRADE THE MENU TO BE FANCIER

            */
        }

        public void RenderLevelFrame(Player player, ref Level levelStore, HUD HUD_Object, float WINDOW_WIDTH, float WINDOW_HEIGHT)
        {
            GL.BindVertexArray(VertexArrayObject);

            // ENABLE DEPTH CAP
            GL.Enable(EnableCap.DepthTest);

            // CLEAR SCREEN
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // CALCULATE MATRICES
            Matrix4 model = Matrix4.CreateRotationX(player.moveRotation.X);

            // by treating the rotations as euler angles and keeping them separate in the code, I can move the player by rotating only around the Y axis, while being able to look up and down as well 
            // as around the Y axis. Treating them as euler angles also means I can look around the 'pitch' axis without having to calculate this in the player movement code
            //Vector3 front = Matrix3.CreateFromQuaternion(Quaternion.FromEulerAngles(player.upRotation + player.moveRotation)) * new Vector3(0.0f, 0.0f, -1.0f);

            Vector3 front = Matrix3.CreateRotationY(player.moveRotation.Y) * Matrix3.CreateRotationX(player.upRotation.X) * new Vector3(0.0f, 0.0f, -1.0f);

            //Matrix4 view = Matrix4.LookAt(player.Position + (Vector3.UnitY * player.Scale.Y / 2), player.Position + (Vector3.UnitY * player.Scale.Y / 2) + (front.X, front.Y, front.Z), (0, 1, 0));
            Matrix4 view = CustomMatrix4.GenerateViewMatrix(player.Position + (Vector3.UnitY * player.Scale.Y / 2), front, new(0,1,0));

            //Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)(Math.PI / 3), WINDOW_WIDTH / WINDOW_HEIGHT, 0.01f, 100.0f);

            Matrix4 projection = CustomMatrix4.MakeFrustum((float)Math.PI / 2f, WINDOW_WIDTH / WINDOW_HEIGHT, 0.01f, 100f);

            // RENDER STATIC GEOMETRY
            levelTextureAtlas.Use(TextureUnit.Texture0);
            levelShader.SetInt("textureAtlas", 0);
            RenderDataWithTransforms(levelStore.GL_Level, model, view, projection);

            // RENDER TILE ENTITIES
            levelTextureAtlas.Use(TextureUnit.Texture0);
            levelShader.SetInt("textureAtlas", 0);
            RenderDataWithTransforms(levelStore.GenerateTileEntityFloatData(), model, view, projection);

            // RENDER ENTITIES
            entityAtlas.Use(TextureUnit.Texture2);
            levelShader.SetInt("textureAtlas", 2);
            RenderDataWithTransforms(levelStore.GenerateEntityFloatData(player.upRotation + player.moveRotation, false), model, view, projection);

            // RENDER ENTITIES
            enemyAtlas.Use(TextureUnit.Texture5);
            levelShader.SetInt("textureAtlas", 5);
            RenderDataWithTransforms(levelStore.GenerateEntityFloatData(player.upRotation + player.moveRotation, true), model, view, projection);

            //RENDER SKYBOX
            levelTextureAtlas.Use(TextureUnit.Texture0);
            levelShader.SetInt("textureAtlas", 0);
            RenderDataWithTransforms(new Cube(player.Position, 50, 50, 50, 15).GenerateFloatData((0,0,0)), model, view, projection);

            // DISABLE DEPTH CAP
            GL.Disable(EnableCap.DepthTest);

            // RENDER UI
            HUD_Object.SyncHUD_GL(WINDOW_WIDTH / WINDOW_HEIGHT);
            hudAtlas.Use(TextureUnit.Texture1);
            levelShader.SetInt("textureAtlas", 1);
            RenderData(HUD_Object.HUD_GL);

            // RENDER TEXT
            font.Use(TextureUnit.Texture4);
            levelShader.SetInt("textureAtlas", 4);
            RenderData(HUD_Object.TextElementGL);
        }

        private void RenderDataWithTransforms(float[] data, Matrix4 model, Matrix4 view, Matrix4 projection)
        {
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StreamDraw);
            levelShader.Use(model, view, projection);
            GL.DrawArrays(PrimitiveType.Triangles, 0, data.Length / 6);
        }

        private void RenderData(float[] data)
        {
            // UI and Text need to be rendered without moving or rotating
            RenderDataWithTransforms(data, Matrix4.Identity, Matrix4.Identity, Matrix4.Identity);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(VertexBufferObject);
            GL.DeleteBuffer(VertexArrayObject);
            if (levelShader != null)
            {
                levelShader.Dispose();   
            }
        }

        public void FlashColorTint(Vector4 color)
        {
            levelShader.SetVec4("tintColor", color);
            levelShader.SetFloat("tintEnable", 1);
            tintDuration = 0f;
        }

        public void TintTick(double timeSinceLastTick)
        {

            if (tintDuration != -1)
            {
                tintDuration += timeSinceLastTick;

                if (tintDuration > 0.12f)
                {
                    tintDuration = -1;
                    levelShader.SetFloat("tintEnable", 0);
                }
            }
        }
    }
}