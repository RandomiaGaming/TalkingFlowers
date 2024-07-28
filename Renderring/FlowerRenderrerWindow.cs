using System.Drawing;
using System;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace TalkingFlowers
{
    public sealed class FlowerRenderrerWindow : Form
    {
        private Screen _screen;
        private Bitmap _talkingFlowerImage;
        public FlowerRenderrerWindow(int screenIndex)
        {
            Assembly assembly = typeof(Program).Assembly;
            Stream resourceStream = assembly.GetManifestResourceStream("TalkingFlowers.TalkingFlowerImage.png");
            _talkingFlowerImage = new Bitmap(resourceStream);
            resourceStream.Dispose();

            _screen = Screen.AllScreens[screenIndex];
            ShowInTaskbar = false;
            BackColor = Color.Magenta;
            TransparencyKey = Color.Magenta;
            StartPosition = FormStartPosition.Manual;
            Location = _screen.Bounds.Location;
            Size = _screen.Bounds.Size;
            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            DoubleBuffered = true;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int scaledX = FlowerRenderrer.FlowerX - _screen.Bounds.Location.X;
            int scaledY = FlowerRenderrer.FlowerY - _screen.Bounds.Location.Y;

            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            e.Graphics.DrawImage(_talkingFlowerImage, new Rectangle(scaledX, scaledY, 100, 100), new Rectangle(0, 0, _talkingFlowerImage.Width, _talkingFlowerImage.Height), GraphicsUnit.Pixel);

            Invalidate();
        }
    }
}