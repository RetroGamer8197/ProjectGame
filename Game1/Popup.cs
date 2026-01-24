using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Game1
{

    public class PopupWindow : GameWindow
    {
        public PopupWindow(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title })
        {
            
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            
            if (e.Key == Keys.Escape)
            {
                Close();
            }

        }
    }
}