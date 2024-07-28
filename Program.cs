using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TalkingFlowers
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            FlowerRenderrer.Init();
            Stopwatch stopwatch = Stopwatch.StartNew();
            long lastTime = 0;
            while (true)
            {
                FlowerRenderrer.FlowerX = Cursor.Position.X;
                FlowerRenderrer.FlowerY = Cursor.Position.Y;
                while(stopwatch.ElapsedTicks - lastTime < 166666) { }
                lastTime = stopwatch.ElapsedTicks;
            }
        }
    }
   /* public static class TalkingFlowerConfig
    {
        public const string TalkingFlowerResourceName = "TalkingFlowerImage.png";
        public const float GravityForce = 9.8f;
        public const float TimeScale = 1.0f;
        public const float GlobalScale = 0.25f;
        public const double MinDelay = 1.0f;
        public const double MaxDelay = 5.0f;
        public const int MinDelayTicks = (int)(MinDelay * 10000000);
        public const int MaxDelayTicks = (int)(MaxDelay * 10000000);
    }
    public static class TalkingFlower
    {
        public static IntPtr WindowHandle;
        private static Bitmap TalkingFlowerImage;
        private static bool Held = false;
        private static Stopwatch DeltaTimer;
        private static long LastFrameTimeStamp = 0;
        private static Random RNG;
        private static long NextSoundTime = 0;
        private static float BoundsX = 0.0f;
        private static float BoundsY = 0.0f;
        private static float PositionX = 0.0f;
        private static float PositionY = 0.0f;
        private static float VelocityX = 0.0f;
        private static float VelocityY = 0.0f;
        private static float HoldOffsetX = 0;
        private static float HoldOffsetY = 0;
        private static Pen outlinePen = new Pen(Color.Red, 1.0f);
        public static void Init()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DeltaTimer = new Stopwatch();
            DeltaTimer.Start();

            RNG = new Random((int)DateTime.Now.Ticks);

            Type type = typeof(TalkingFlowerRenderForm);
            Assembly assembly = type.Assembly;
            string fullResourceName = $"{type.Namespace}.{TalkingFlowerConfig.TalkingFlowerResourceName}";
            Stream resourceStream = assembly.GetManifestResourceStream(fullResourceName);
            Bitmap TalkingFlowerImageUnscaled = new Bitmap(resourceStream);
            resourceStream.Dispose();
            TalkingFlowerImage = new Bitmap((int)(TalkingFlowerImageUnscaled.Width * TalkingFlowerConfig.GlobalScale), (int)(TalkingFlowerImageUnscaled.Height * TalkingFlowerConfig.GlobalScale));
            Graphics TalkingFlowerImageGraphics = Graphics.FromImage(TalkingFlowerImage);
            TalkingFlowerImageGraphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            TalkingFlowerImageGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            TalkingFlowerImageGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            TalkingFlowerImageGraphics.DrawImage(TalkingFlowerImageUnscaled, new Rectangle(0, 0, TalkingFlowerImage.Width, TalkingFlowerImage.Height), new Rectangle(0, 0, TalkingFlowerImageUnscaled.Width, TalkingFlowerImageUnscaled.Height), GraphicsUnit.Pixel);
            TalkingFlowerImageGraphics.Dispose();
            TalkingFlowerImageUnscaled.Dispose();

            BoundsX = Screen.PrimaryScreen.Bounds.Width - TalkingFlowerImage.Width;
            BoundsY = Screen.PrimaryScreen.Bounds.Height - TalkingFlowerImage.Height;
        }
        public static void Run()
        {
            TalkingFlowerRenderForm talkingFlowerRenderForm = new TalkingFlowerRenderForm();

            WindowHandle = talkingFlowerRenderForm.Handle;

            talkingFlowerRenderForm.Show();

            while (true)
            {
                Application.DoEvents();
            }
        }
        public static void Drop()
        {
            Held = false;
        }
        public static void Grab()
        {
            float mouseX = Cursor.Position.X;
            float mouseY = Cursor.Position.Y;

            if (mouseX >= PositionX && mouseX <= PositionX + TalkingFlowerImage.Width && mouseY >= PositionY && mouseY <= PositionY + TalkingFlowerImage.Height)
            {
                Pickup();
            }
        }
        public static void Pickup()
        {
            float mouseX = Cursor.Position.X;
            float mouseY = Cursor.Position.Y;

            HoldOffsetX = mouseX - PositionX;
            HoldOffsetY = mouseY - PositionY;

            Held = true;
        }
        public static void PaintMe(Graphics graphics)
        {
            long timeStamp = DeltaTimer.ElapsedTicks;
            long deltaTicks = timeStamp - LastFrameTimeStamp;
            float deltaTime = (deltaTicks) / 10000000.0f;

            Console.WriteLine(10000000.0 / (double)deltaTicks);

            if (NextSoundTime <= timeStamp)
            {
                NextSoundTime = timeStamp + RNG.Next(TalkingFlowerConfig.MinDelayTicks, TalkingFlowerConfig.MaxDelayTicks);
            }


            if (Held)
            {
                Point cursorPosition = Cursor.Position;

                PositionX = cursorPosition.X - HoldOffsetX;
                PositionY = cursorPosition.Y - HoldOffsetY;

                VelocityX = 0;
                VelocityY = 0;
            }
            else
            {
                float timeScale = TalkingFlowerConfig.TimeScale * deltaTime;
                PositionX += VelocityX * timeScale;
                PositionY += VelocityY * timeScale;

                VelocityY += TalkingFlowerConfig.GravityForce * timeScale * TalkingFlowerImage.Height;
            }

            if (PositionX < 0.0f)
            {
                PositionX = 0.0f;
            }
            if (PositionX > BoundsX)
            {
                PositionX = BoundsX;
            }
            if (PositionY < 0.0f)
            {
                PositionY = 0.0f;
            }
            if (PositionY > BoundsY)
            {
                PositionY = BoundsY;
            }

            graphics.DrawImageUnscaled(TalkingFlowerImage, (int)PositionX, (int)PositionY);

            WindowEngine.Update();

            foreach (WindowEngine.WindowInfo windowInfo in WindowEngine.WindowInfoList)
            {
                graphics.DrawRectangle(outlinePen, new Rectangle(windowInfo.PositionX, windowInfo.PositionY, windowInfo.Width, windowInfo.Height));
            }

            LastFrameTimeStamp = timeStamp;
        }
    }*/
}
