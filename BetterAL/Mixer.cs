//Approved 0
namespace BetterAL
{
    public static class Mixer
    {
        #region Public Static Methods
        public static MonoAudioClip Mix(params MonoAudioClip[] monoAudioClips) //Leader
        {
            if (monoAudioClips is null)
            {
                throw new System.Exception($"{nameof(monoAudioClips)} cannot be null.");
            }
            if (monoAudioClips.LongLength > int.MaxValue)
            {
                throw new System.Exception($"{nameof(monoAudioClips)}.{nameof(monoAudioClips.Length)} must be less than int.MaxValue.");
            }
            if (monoAudioClips.LongLength <= 0)
            {
                throw new System.Exception($"{nameof(monoAudioClips)}.{nameof(monoAudioClips.Length)} must be greater than 0.");
            }
            MonoAudioClip audioClip = new MonoAudioClip();
            audioClip._encoding = monoAudioClips[0]._encoding;
            audioClip._sampleRate = monoAudioClips[0]._sampleRate;
            audioClip._bitsPerSample = monoAudioClips[0]._bitsPerSample;
            audioClip._blockAlign = monoAudioClips[0]._blockAlign;
            audioClip._averageBytesPerSecond = monoAudioClips[0]._averageBytesPerSecond;
            audioClip._extraSize = monoAudioClips[0]._extraSize;
            long expectedSampleCount = monoAudioClips[0]._sampleCount;
            for (int i = 1; i < monoAudioClips.Length; i++)
            {
                if (monoAudioClips[i]._sampleCount != expectedSampleCount)
                {
                    throw new System.Exception($"{nameof(monoAudioClips)} must contain clips with identical {nameof(MonoAudioClip.SampleCount)}.");
                }
                if (monoAudioClips[i]._encoding != audioClip._encoding)
                {
                    throw new System.Exception($"{nameof(monoAudioClips)} must contain clips with identical {nameof(MonoAudioClip.Encoding)}.");
                }
                if (monoAudioClips[i]._sampleRate != audioClip._sampleRate)
                {
                    throw new System.Exception($"{nameof(monoAudioClips)} must contain clips with identical {nameof(MonoAudioClip.SampleRate)}.");
                }
                if (monoAudioClips[i]._bitsPerSample != audioClip._bitsPerSample)
                {
                    throw new System.Exception($"{nameof(monoAudioClips)} must contain clips with identical {nameof(MonoAudioClip.Encoding)}.");
                }
            }
            audioClip._sampleCount = monoAudioClips[0]._sampleCount;
            audioClip._samples = new float[audioClip._sampleCount];
            for (long i = 0; i < audioClip._sampleCount; i++)
            {
                float total = 0.0f;
                for (int j = 0; j < monoAudioClips.Length; j++)
                {
                    total += monoAudioClips[j]._samples[i];
                }
                if (total > 1.0f)
                {
                    total = 1.0f;
                }
                else if (total < -1.0f)
                {
                    total = -1.0f;
                }
                audioClip._samples[i] = total;
            }
            return audioClip;
        }
        public static MonoAudioClip DownMix(params MonoAudioClip[] monoAudioClips) //Follower
        {
            if (monoAudioClips is null)
            {
                throw new System.Exception($"{nameof(monoAudioClips)} cannot be null.");
            }
            if (monoAudioClips.LongLength > int.MaxValue)
            {
                throw new System.Exception($"{nameof(monoAudioClips)}.{nameof(monoAudioClips.Length)} must be less than int.MaxValue.");
            }
            if (monoAudioClips.LongLength <= 0)
            {
                throw new System.Exception($"{nameof(monoAudioClips)}.{nameof(monoAudioClips.Length)} must be greater than 0.");
            }
            MonoAudioClip audioClip = new MonoAudioClip();
            audioClip._encoding = monoAudioClips[0]._encoding;
            audioClip._sampleRate = monoAudioClips[0]._sampleRate;
            audioClip._bitsPerSample = monoAudioClips[0]._bitsPerSample;
            audioClip._blockAlign = monoAudioClips[0]._blockAlign;
            audioClip._averageBytesPerSecond = monoAudioClips[0]._averageBytesPerSecond;
            audioClip._extraSize = monoAudioClips[0]._extraSize;
            long expectedSampleCount = monoAudioClips[0]._sampleCount;
            for (int i = 1; i < monoAudioClips.Length; i++)
            {
                if (monoAudioClips[i]._sampleCount != expectedSampleCount)
                {
                    throw new System.Exception($"{nameof(monoAudioClips)} must contain clips with identical {nameof(MonoAudioClip.SampleCount)}.");
                }
                if (monoAudioClips[i]._encoding != audioClip._encoding)
                {
                    throw new System.Exception($"{nameof(monoAudioClips)} must contain clips with identical {nameof(MonoAudioClip.Encoding)}.");
                }
                if (monoAudioClips[i]._sampleRate != audioClip._sampleRate)
                {
                    throw new System.Exception($"{nameof(monoAudioClips)} must contain clips with identical {nameof(MonoAudioClip.SampleRate)}.");
                }
                if (monoAudioClips[i]._bitsPerSample != audioClip._bitsPerSample)
                {
                    throw new System.Exception($"{nameof(monoAudioClips)} must contain clips with identical {nameof(MonoAudioClip.Encoding)}.");
                }
            }
            audioClip._sampleCount = monoAudioClips[0]._sampleCount;
            audioClip._samples = new float[audioClip._sampleCount];
            for (long i = 0; i < audioClip._sampleCount; i++)
            {
                float total = 0.0f;
                for (int j = 0; j < monoAudioClips.Length; j++)
                {
                    total += monoAudioClips[j]._samples[i];
                }
                audioClip._samples[i] = total / monoAudioClips.Length;
            }
            return audioClip;
        }
        #endregion
    }
}