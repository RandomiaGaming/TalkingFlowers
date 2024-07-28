namespace BetterAL
{
    public static class AudioVisualizer
    {
        public static System.Drawing.Bitmap Visualize(float[] samples, int height, System.Drawing.Color backgroundColor, System.Drawing.Color waveformColor) //Leader
        {
            System.Drawing.Bitmap output = new System.Drawing.Bitmap(samples.Length, height);
            System.Drawing.Graphics outputGraphics = System.Drawing.Graphics.FromImage(output);
            outputGraphics.Clear(backgroundColor);
            float maxValue = samples[0];
            float minValue = samples[0];
            for (int i = 1; i < samples.Length; i++)
            {
                if (samples[i] > maxValue)
                {
                    maxValue = samples[i];
                }
                if (samples[i] < minValue)
                {
                    minValue = samples[i];
                }
            }
            float range = maxValue - minValue;
            System.Drawing.SolidBrush waveformBursh = new System.Drawing.SolidBrush(waveformColor);
            for (int i = 0; i < samples.Length; i++)
            {
                outputGraphics.FillRectangle(waveformBursh, new System.Drawing.Rectangle(i, 0, 1, (int)((samples[i] - minValue) / range)));
            }
            outputGraphics.Dispose();
            return output;
        }
        public static System.Drawing.Bitmap Visualize(float[] samples, int height) //Follower
        {
            System.Drawing.Bitmap output = new System.Drawing.Bitmap(samples.Length, height);
            System.Drawing.Graphics outputGraphics = System.Drawing.Graphics.FromImage(output);
            outputGraphics.Clear(System.Drawing.Color.Black);
            float maxValue = samples[0];
            float minValue = samples[0];
            for (int i = 1; i < samples.Length; i++)
            {
                if (samples[i] > maxValue)
                {
                    maxValue = samples[i];
                }
                if (samples[i] < minValue)
                {
                    minValue = samples[i];
                }
            }
            float range = maxValue - minValue;
            System.Drawing.SolidBrush waveformBursh = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            for (int i = 0; i < samples.Length; i++)
            {
                outputGraphics.FillRectangle(waveformBursh, new System.Drawing.Rectangle(i, 0, 1, (int)((samples[i] - minValue) / range)));
            }
            outputGraphics.Dispose();
            return output;
        }
        public static System.Drawing.Bitmap Visualize(MonoAudioClip monoAudioClip, int height, System.Drawing.Color backgroundColor, System.Drawing.Color waveformColor) //Follower
        {
            float[] samples = monoAudioClip.ToArray();
            System.Drawing.Bitmap output = new System.Drawing.Bitmap(samples.Length, height);
            System.Drawing.Graphics outputGraphics = System.Drawing.Graphics.FromImage(output);
            outputGraphics.Clear(backgroundColor);
            float maxValue = samples[0];
            float minValue = samples[0];
            for (int i = 1; i < samples.Length; i++)
            {
                if (samples[i] > maxValue)
                {
                    maxValue = samples[i];
                }
                if (samples[i] < minValue)
                {
                    minValue = samples[i];
                }
            }
            float range = maxValue - minValue;
            System.Drawing.SolidBrush waveformBursh = new System.Drawing.SolidBrush(waveformColor);
            for (int i = 0; i < samples.Length; i++)
            {
                outputGraphics.FillRectangle(waveformBursh, new System.Drawing.Rectangle(i, 0, 1, (int)((samples[i] - minValue) / range)));
            }
            outputGraphics.Dispose();
            return output;
        }
        public static System.Drawing.Bitmap Visualize(MonoAudioClip monoAudioClip, int height) //Follower
        {
            float[] samples = monoAudioClip.ToArray();
            System.Drawing.Bitmap output = new System.Drawing.Bitmap(samples.Length, height);
            System.Drawing.Graphics outputGraphics = System.Drawing.Graphics.FromImage(output);
            outputGraphics.Clear(System.Drawing.Color.Black);
            float maxValue = samples[0];
            float minValue = samples[0];
            for (int i = 1; i < samples.Length; i++)
            {
                if (samples[i] > maxValue)
                {
                    maxValue = samples[i];
                }
                if (samples[i] < minValue)
                {
                    minValue = samples[i];
                }
            }
            float range = maxValue - minValue;
            System.Drawing.SolidBrush waveformBursh = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            for (int i = 0; i < samples.Length; i++)
            {
                int bruh = (int)(((samples[i] - minValue) / range) * height);
                outputGraphics.FillRectangle(waveformBursh, new System.Drawing.Rectangle(i, height - bruh, 1, bruh));
            }
            outputGraphics.Dispose();
            return output;
        }
    }
}