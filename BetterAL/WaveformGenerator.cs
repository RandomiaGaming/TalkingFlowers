namespace BetterAL
{
    public static class WaveformGenerator
    {
        #region Public Delegates
        //Takes in a a float progress between 0 and 1.
        //Should return an amplitude between -1 and 1.
        //Progress represents the progress between the start and end of a wavelength.
        public delegate float WaveformProvider(float progress);
        #endregion
        #region Public Static Methods
        public static MonoAudioClip GenerateFromWaveform(WaveformProvider waveform, float frequency, long sampleCount, int sampleRate)
        {
            if (waveform is null)
            {
                throw new System.Exception($"{nameof(waveform)} cannot be null.");
            }
            if (frequency is float.NaN)
            {
                throw new System.Exception($"{nameof(frequency)} cannot be NaN.");
            }
            if (frequency is float.PositiveInfinity)
            {
                throw new System.Exception($"{nameof(frequency)} cannot be positive infinity.");
            }
            if (frequency is float.NegativeInfinity)
            {
                throw new System.Exception($"{nameof(frequency)} cannot be negative infinity.");
            }
            if (sampleCount < 0)
            {
                throw new System.Exception($"{nameof(sampleCount)} must be greater than or equal to 0.");
            }
            if (sampleRate <= 0)
            {
                throw new System.Exception($"{nameof(sampleRate)} must be greater than 0.");
            }
            MonoAudioClip output = new MonoAudioClip();
            output._encoding = NAudio.Wave.WaveFormatEncoding.IeeeFloat;
            output._sampleRate = sampleRate;
            output._bitsPerSample = 32;
            output._blockAlign = 4;
            output._averageBytesPerSecond = 4 * sampleRate;
            output._extraSize = 0;
            output._sampleCount = sampleCount;
            output._samples = new float[sampleCount];
            float waveLength = 1.0f / frequency;
            for (long i = 0; i < sampleCount; i++)
            {
                float progress = i / (float)sampleRate;
                float clampedProgress = progress % waveLength;
                float normalizedProgress = clampedProgress / waveLength;
                float sample = waveform.Invoke(normalizedProgress);
                output._samples[i] = sample;
            }
            return output;
        }
        #endregion
        #region Public WaveformProviders
        public static float SinWaveformProvider(float progress)
        {
            return (float)System.Math.Sin(6.283185f * progress);
        }
        public static float SquareWaveformProvider(float progress)
        {
            if (progress < 0.5f)
            {
                return -1.0f;
            }
            else
            {
                return 1.0f;
            }
        }
        public static float TriangleWaveformProvider(float progress)
        {
            progress *= 4.0f;
            if (progress < 2.0f)
            {
                return progress - 1.0f;
            }
            else
            {
                return 3.0f - progress;
            }
        }
        public static float SawToothWaveformProvider(float progress)
        {
            return (progress * 2.0f) - 1.0f;
        }
        public static float ToothSawWaveformProvider(float progress)
        {
            return 1.0f + (progress * -2.0f);
        }
        public static System.Random RNG = new System.Random();
        public static float RadioStaticGenerator(float progress)
        {
            return (float)((RNG.NextDouble() * 2.0) - 1.0);
        }
        #endregion
    }
}