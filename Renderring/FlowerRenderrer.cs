using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace TalkingFlowers
{
    public static class FlowerRenderrer
    {
        public static int FlowerX;
        public static int FlowerY;
        private static FlowerRenderrerWindow[] _windows;
        public static void Init()
        {
            _windows = new FlowerRenderrerWindow[Screen.AllScreens.Length];
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                int windowIndex = i;
                new Thread(() =>
                {
                    _windows[windowIndex] = new FlowerRenderrerWindow(windowIndex);
                    _windows[windowIndex].ShowDialog();
                }).Start();
            }
        }
    }
}
