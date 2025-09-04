using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Game1
{

    public class Renderer
    {
        private int VertexBufferObject, VertexArrayObject;
        private double tintDuration = -1;
        public Shader levelShader;
        private Texture levelTextureAtlas, hudAtlas, entityAtlas;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Renderer()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            Init();
        }

        public void Init()
        {
            GL.ClearColor(0.2f, 0.2f, 0.5f, 1.0f);

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            levelTextureAtlas = new("Textures/atlas.png", TextureUnit.Texture0, true);
            hudAtlas = new("Textures/hudatlas.png", TextureUnit.Texture1, false);
            entityAtlas = new("Textures/entityatlas.png", TextureUnit.Texture2, false);

            levelShader = new("Shaders/level.vert", "Shaders/level.frag");
            levelShader.Use(Matrix4.Identity, Matrix4.Identity, Matrix4.Identity);

            int vertexLocation = GL.GetAttribLocation(levelShader.Handle, "aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            int textureLocation = GL.GetAttribLocation(levelShader.Handle, "aTexCoord");
            GL.EnableVertexAttribArray(textureLocation);
            GL.VertexAttribPointer(textureLocation, 2, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

            int shaderDirection = GL.GetAttribLocation(levelShader.Handle, "aShaderDir");
            GL.EnableVertexAttribArray(shaderDirection);
            GL.VertexAttribPointer(shaderDirection, 1, VertexAttribPointerType.Float, false, 6 * sizeof(float), 5 * sizeof(float));

            levelShader.SetInt("texture1", 0);
            levelShader.SetInt("texture2", 1);
            levelShader.SetInt("texture3", 2);
            levelShader.SetVec4("tintColor", (1, 1, 1, 0));
            levelShader.SetInt("tintEnable", 0);

            GL.Enable(EnableCap.DepthTest);
        }

        public void RenderFrame(Player player, Level levelStore, Level HUD, float WINDOW_WIDTH, float WINDOW_HEIGHT)
        {
            GL.Enable(EnableCap.DepthTest);     // when drawing level geometry, we need the depth buffer enabled so that the triangles are drawn in the correct order 

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            /// Draw the Level Geometry

            GL.BufferData(BufferTarget.ArrayBuffer, levelStore.GL_Level.Length * sizeof(float), levelStore.GL_Level, BufferUsageHint.StreamDraw);

            GL.BindVertexArray(VertexArrayObject);

            // generate the correct transformation matrices
            Matrix4 model = Matrix4.CreateRotationX(player.moveRotation.X);

            Vector3 front = Matrix3.CreateFromQuaternion(Quaternion.FromEulerAngles(player.upRotation + player.moveRotation)) * new Vector3(0.0f, 0.0f, -1.0f); // by treating the rotations as euler angles and keeping them separate in the code, I can move the player by rotating only around the Y axis, while being able to look up and down as well as around the Y axis. Treating them as euler angles also means I can look around the 'pitch' axis without having to calculate this in the player movement code

            Matrix4 view = Matrix4.LookAt(player.Position + (Vector3.UnitY * player.Scale.Y / 2), player.Position + (Vector3.UnitY * player.Scale.Y / 2) + (front.X, front.Y, front.Z), (0, 1, 0));

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)(Math.PI / 3), WINDOW_WIDTH / WINDOW_HEIGHT, 0.1f, 100.0f);

            // enable the correct texture atlas for the level
            levelTextureAtlas.Use(TextureUnit.Texture0);
            levelShader.SetInt("textureAtlas", 0);
            levelShader.Use(model, view, projection);

            GL.DrawArrays(PrimitiveType.Triangles, 0, levelStore.GL_Level.Length / 6);

            /// Begin drawing entities
            Entity entityTemp;
            List<float> GL_Entity = [];
            foreach (Object objEntity in levelStore.levelObjects)
            {
                if (objEntity.objectType == Object.ObjectType.Entity)
                {
                    entityTemp = (Entity)objEntity;
                    if (entityTemp.alive == true)
                    {
                        GL_Entity.AddRange(entityTemp.GenerateOpenGLData(player.upRotation + player.moveRotation));
                    }
                }
            }

            float[] GL_EntityArray = [.. GL_Entity];

            levelShader.SetInt("textureAtlas", 2);

            GL.BufferData(BufferTarget.ArrayBuffer, GL_EntityArray.Length * sizeof(float), GL_EntityArray, BufferUsageHint.StreamDraw); // buffer the Entity vertex data to the GPU. Entities are drawn like 2D sprites so their vertex data needs to be updated every frame
            levelShader.Use(model, view, projection);
            GL.DrawArrays(PrimitiveType.Triangles, 0, GL_EntityArray.Length / 6);

            // skybox
            Triangle[] skybox_triangles = new Cube(player.Position, 50, 50, 50, 15).ConvertToTriangles();

            List<float> skybox = [];

            foreach (Triangle tempTriangle in skybox_triangles)
            {
                for (int i = 0; i < 3; i++)
                {
                    skybox.AddRange(tempTriangle.coordinates[i].X, tempTriangle.coordinates[i].Y, tempTriangle.coordinates[i].Z, tempTriangle.textureCoordinates[i].X, tempTriangle.textureCoordinates[i].Y, tempTriangle.directionIndex);
                }
            }

            levelTextureAtlas.Use(TextureUnit.Texture0);
            levelShader.SetInt("textureAtlas", 0);

            GL.BufferData(BufferTarget.ArrayBuffer, skybox.Count * sizeof(float), skybox.ToArray(), BufferUsageHint.StreamDraw); // buffer the skybox vertex data 
            levelShader.Use(model, view, projection);
            GL.DrawArrays(PrimitiveType.Triangles, 0, skybox.Count / 6);

            /// Begin Drawing the UI

            GL.Disable(EnableCap.DepthTest);    // disable the depth test to prevent the level geometry from obscuring the HUD

            HUD.Sync_GL_Level();

            GL.BufferData(BufferTarget.ArrayBuffer, HUD.GL_Level.Length * sizeof(float), HUD.GL_Level, BufferUsageHint.StreamDraw); // buffer the HUD vertex data to the GPU

            // Change texture atlas to the correct atlas for the HUD
            hudAtlas.Use(TextureUnit.Texture1);
            levelShader.SetInt("textureAtlas", 1);

            levelShader.Use(Matrix4.Identity, Matrix4.Identity, Matrix4.Identity);  // we can reuse the level shader by adding all relevant parameters in such as way that the vertex shader applies no transformations and the fragment shader applies no shading (shaded dir = 1.0f)

            GL.DrawArrays(PrimitiveType.Triangles, 0, HUD.GL_Level.Length / 6); // finally, we can tell the GPU to draw the HUD
        }

        public void Dispose()
        {
            GL.DeleteBuffer(VertexBufferObject);
            GL.DeleteBuffer(VertexArrayObject);

            levelShader.Dispose();
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

                if (tintDuration > 0.2f)
                {
                    tintDuration = -1;
                    levelShader.SetFloat("tintEnable", 0);
                }
            }
        }
    }
}