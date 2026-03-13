using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Game1
{

    public class Game : GameWindow
    {

        public enum GameState
        {
            Menu, Level, Pause, Loading, Reset, Error, None
        }


        public const float speed = 2f;
        public float WINDOW_WIDTH = 1.6f / 0.9f, WINDOW_HEIGHT = 1.0f;
        private string[] InitialLevelNames = [];
        private Queue<string> LevelFileNames = [];
        private string currentLevel = "";
        private bool gameReset = true;
        
        private readonly bool FileLoadingEnabled = true; // used for debugging and building levels
        private readonly bool FPS_counter_enabled = true; // controls whether the FPS counter element of the HUD is shown
        

        private Player player;
        private Renderer renderer;
        private Level levelStore;
        private HUD HUD_Object, PauseMenu;
        public GameState gameState = GameState.Loading;


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title })
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            // caps framerate to 480 FPS unless on macos which is limited to 120 for battery life
            UpdateFrequency = 480;
            if (OperatingSystem.IsMacOS())
            {
                UpdateFrequency = 120;
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            // CREATE RENDERER INSTANCE
            renderer = new(out bool rendererInitSuccess);
            if (!rendererInitSuccess)
            {
                ErrorReporter.Report("Failure to initialise the renderer");
                Close();
            }

            // CREATE PLAYER INSTANCE
            player = new((0,0,0), (0.25f, 0.5f, 0.25f), (0, (float)Math.PI, 0), 100f);

            // LOAD LEVEL NAMES FROM FILE
            InitialLevelNames = LoadLevelFileNames();

            // CREATE HUD OBJECTS AND ELEMENTS

            // --- HUD ---
            HUD_Object = new();

            HUD_Object.AddHUD_Element("crosshair", new Crosshair());
            HUD_Object.AddHUD_Element("healthbar", new HealthBar(100f, (-0.7f, -0.8f, 0), 2));
            HUD_Object.AddHUD_Element("ammoindicator", new HealthBar(1.0f, (0.7f, -0.8f, 0), 3));

            HUD_Object.AddHUD_Element("messagebox", new MessageBox((-0.95f, 0.75f), 24f / 720f));

            HUD_Object.AddHUD_Element("weaponicon", new WeaponIcon());

            HUD_Object.AddHUD_Element("ammotext", new AmmoText((0.7f, -0.9f), 24f / 720f));
            HUD_Object.AddHUD_Element("armorbar", new HealthBar(100f, (-0.7f, -0.65f, 0), 0));

            HUD_Object.AddHUD_Element("redkeycard", new ItemIcon((-0.9f, 0.9f), (0.1f, 0.1f), 4, new HeldItem() { color = HeldItem.Colors.Red, itemType = HeldItem.ItemTypes.Keycard }));
            HUD_Object.AddHUD_Element("bluekeycard", new ItemIcon((-0.75f, 0.9f), (0.1f, 0.1f), 5, new HeldItem() { color = HeldItem.Colors.Blue, itemType = HeldItem.ItemTypes.Keycard }));
            HUD_Object.AddHUD_Element("greenkeycard", new ItemIcon((-0.6f, 0.9f), (0.1f, 0.1f), 6, new HeldItem() { color = HeldItem.Colors.Green, itemType = HeldItem.ItemTypes.Keycard }));
            HUD_Object.AddHUD_Element("yellowkeycard", new ItemIcon((-0.45f, 0.9f), (0.1f, 0.1f), 7, new HeldItem() { color = HeldItem.Colors.Yellow, itemType = HeldItem.ItemTypes.Keycard }));

            HUD_Object.QueueMessage("WASD to move");
            HUD_Object.QueueMessage("Space to jump");
            HUD_Object.QueueMessage("E to interact with a button");
            HUD_Object.QueueMessage("Click to shoot");
            HUD_Object.QueueMessage("Backspace to capture mouse");
            
            if (FPS_counter_enabled) {
                HUD_Object.AddHUD_Element("FPS_counter", new TextElement((0.7f, 0.9f), 16f / 720f, "FPS: 0.0", false));
            }
            

            // --- Pause Menu ---
            PauseMenu = new();

            PauseMenu.AddHUD_Element("bg", new Background(15));
            PauseMenu.AddHUD_Element("title", new TextElement((-0.6f, 0.7f), 32f / 720f, "Pause Menu", true));
            PauseMenu.AddHUD_Element("message", new TextElement((-0.6f, 0.4f), 32f / 720f, "Press - to quit", true));
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            // function differently based on gamestate
            switch (gameState)
            {
                case GameState.Menu:
                    if (KeyboardState.IsKeyPressed(Keys.Enter))
                    {
                        gameState = GameState.Reset;
                        gameReset = true;
                    }
                    break;
                case GameState.Pause:
                    Pause_OnUpdateFrame();
                    break;
                case GameState.Level:
                    Level_OnUpdateFrame((float)e.Time);
                    break;
                case GameState.Loading:
                    Loading_OnUpdateFrame();
                    break;
                case GameState.Reset:
                    Reset_OnUpdateFrame();
                    break;
                case GameState.Error:
                    ErrorReporter.Report("An unknown error has occurred");
                    Close();
                    break;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            switch (gameState)
            {
                case GameState.Menu:
                    renderer.RenderMainMenu();
                    break;
                case GameState.Level:
                    renderer.RenderLevelFrame(player, ref levelStore, HUD_Object, WINDOW_WIDTH, WINDOW_HEIGHT);
                    break;
                case GameState.Pause:
                    renderer.RenderLevelFrame(player, ref levelStore, PauseMenu, WINDOW_WIDTH, WINDOW_HEIGHT);
                    break;

            }

            SwapBuffers();
        }

        private string[] LoadLevelFileNames()
        {
            const string correctHeader = "ProjectGame 1.1 | Files 2.0";

            string[] levelNamesInitial = [];
            List<string> LevelNamesQueue = [];

            // attempt to open LevelNames.txt
            try
            {
                StreamReader levelNames = new(File.Open("Levels/levelNames.txt", FileMode.Open));
                string? fileHeader = levelNames.ReadLine();
                if (fileHeader != correctHeader)
                {
                    // if the first line is not equal to the string "ProjectGame 1.1 | Files 2.0", the file is in the wrong format
                    ErrorReporter.Report("Level names is incorrect format");
                    Close();
                }
                while (!levelNames.EndOfStream)
                {
                    string? currentFileName = levelNames.ReadLine();
                    if (currentFileName != null)
                    {
                        LevelNamesQueue.Add(currentFileName);
                    }
                }
                levelNamesInitial = [.. LevelNamesQueue];
                levelNames.Close();
            } catch (FileNotFoundException)
            {
                ErrorReporter.Report("Level names file is missing! Game load cannot continue");
                Close();
            }

            return levelNamesInitial;
        }

        private void Level_OnUpdateFrame(float deltaTime)
        {
            
            // FIRST CHECK THE MESSAGEBOX TO SEE IF THE LEVEL IS COMPLETED
            if (HUD_Object.HUD_Elements["messagebox"].CheckBoolValue())
            {
                gameState = GameState.Loading;
                return;
            }

            if (FPS_counter_enabled)
            {
                HUD_Object.HUD_Elements["FPS_counter"].QueueValue("FPS: " + MathF.Round(1 /deltaTime, 2).ToString());
            }

            for (int i = 0; i < levelStore.levelObjects.Count; i++)
            {
                levelStore.levelObjects[i].CheckObjectStateIsCorrect(ref levelStore);
            }

            // RUN PLAYER INPTUT
            CursorState temp_cstate = CursorState;  // because the CursorState cannot be sent as a ref, the solution is to 
                                                    // create a copy and then reassign the original after modifying the copy
            player.Input_Tick(this, KeyboardState, MouseState, ref temp_cstate, ref levelStore, (float)Math.Clamp(deltaTime, 0.0f, 0.1f), ref renderer, ref HUD_Object, ref gameState);

            CursorState = temp_cstate;

            // UPDATE EVERY OBJECT IN THE LEVEL
            foreach (Object levelObject in levelStore.levelObjects)
            {
                if (levelObject.objectType == Object.ObjectType.Entity || levelObject.objectType == Object.ObjectType.EndCondition)
                {
                    levelObject.Tick(player.Position, ref player.Health, (float)Math.Clamp(deltaTime, 0.0f, 0.1f), ref levelStore, ref player, ref HUD_Object);
                }
            }

            // MANAGE TEMPORARY OBJECTS (PROJECTILES)
            Stack<Object> deadTempObjects = [];
            foreach (Object tempObject in levelStore.temporaryObjects)
            {
                if (tempObject.objectType == Object.ObjectType.Entity)
                {
                    tempObject.Tick(player.Position, ref player.Health, (float)Math.Clamp(deltaTime, 0.0f, 0.1f), ref levelStore, ref player, ref HUD_Object);
                }
                if (!((Entity)tempObject).getAliveState())
                {
                    deadTempObjects.Push(tempObject);
                }
            }

            while (deadTempObjects.Count > 0)
            {
                levelStore.temporaryObjects.Remove(deadTempObjects.Pop());
            }

            // TICK TINT TIMER
            renderer.TintTick(Math.Clamp(deltaTime, 0.0f, 0.1f));

            // UPDATE HUD ELEMENTS

            HUD_Object.UpdateElements(ref player, deltaTime);

            if (player.Health <= 0 || player.Position.Y < -30f)
            {
                gameState = GameState.Reset;
            }

        }

        private void Reset_OnUpdateFrame()
        {
            if (currentLevel == "" || gameReset)
            {
                Loading_OnUpdateFrame();
                gameReset = false;
            }
            
            if (gameState == GameState.Error)
            {
                Close();
            }
            else {
                if (FileLoadingEnabled)
                {
                    Level.ImportLevelFromFile(currentLevel, out levelStore);
                    gameState = GameState.Level;
                } else
                {
                    gameState = GameState.Loading;
                }
                
            
                if (!levelStore.successfullyLoaded)
                {
                    Close();
                }
                HUD_Object.LevelReset();
                player.LevelReset();
            }
        }
        private void Loading_OnUpdateFrame()
        {
            if (FileLoadingEnabled)
            {
                if (LevelFileNames.Count > 0)
                {
                    // Load the next level listed in LevelNames.txt 
                    currentLevel = LevelFileNames.Dequeue();
                    Level.ImportLevelFromFile(currentLevel, out levelStore);
                    if (!levelStore.successfullyLoaded)
                    {
                        ErrorReporter.Report($"Error loading level! File: {currentLevel}");
                        gameState = GameState.Error;
                    } else
                    {
                        HUD_Object.LevelReset();
                        player.NewLevel();
                        gameState = GameState.Level;
                    }
                } else
                {
                    // Reset the level file names queue to the start of the game and return to the menu
                    LevelFileNames = [];
                    foreach (string levelName in InitialLevelNames)
                    {
                        LevelFileNames.Enqueue(levelName);
                    }
                    gameState = GameState.Menu;
                }
            } else
            {
                // used for building levels and debugging
                levelStore = LevelTemp.Level3Return();
                levelStore.ExportToFile("Levels/boss.lvl");

                /*levelStore = LevelTemp.DemoReturn();
                levelStore.ExportToFile("Levels/demo.lvl");*/

                HUD_Object.LevelReset();
                player.NewLevel();
                gameState = GameState.Level;
            }
            
        }

        private void Pause_OnUpdateFrame()
        {
            if (CursorState == CursorState.Grabbed)
            {
                CursorState = CursorState.Normal;
                if (OperatingSystem.IsMacOS())
                {
                    MousePosition = (Size.X / 2f, Size.Y / 2f);
                }
            }
            
            if (KeyboardState.IsKeyPressed(Keys.Escape))
            {
                gameState = GameState.Level;
            }
            else if (KeyboardState.IsKeyPressed(Keys.Minus))
            {
                Close();
            }
        }

        protected override void OnUnload()
        {
            // in order to not waste GPU resources, all objects using the GPU must give up their memory.

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            renderer.Dispose();

            base.OnUnload();
        }
    }
 
}