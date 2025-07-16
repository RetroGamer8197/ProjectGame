namespace Game1
{
    public class Launcher
    {
        public static void Main()
        {
            using (Game game = new(1280, 720, "OpenGL Game"))
            {
                game.Run();
            }
        }
    }
}