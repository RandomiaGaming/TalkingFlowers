namespace BetterAL
{
    public static class ChannelOpperations
    {
        #region Public Static Methods
        public static MonoAudioClip[] Split(AudioClip audioClip) //Leader
        {
            if (audioClip is null)
            {
                throw new System.Exception($"{nameof(audioClip)} cannot be null.");
            }
            MonoAudioClip[] monoAudioClips = new MonoAudioClip[audioClip._channelCount];
            for (int i = 0; i < audioClip._channelCount; i++)
            {
                monoAudioClips[i] = new MonoAudioClip();
                monoAudioClips[i]._encoding = audioClip._encoding;
                monoAudioClips[i]._sampleRate = audioClip._sampleRate;
                monoAudioClips[i]._bitsPerSample = audioClip._bitsPerSample;
                monoAudioClips[i]._blockAlign = audioClip._bitsPerSample / 8;
                monoAudioClips[i]._averageBytesPerSecond = (audioClip._bitsPerSample * audioClip.SampleRate) / 8;
                monoAudioClips[i]._extraSize = audioClip._extraSize;
                monoAudioClips[i]._sampleCount = audioClip._sampleCount / audioClip._channelCount;
                monoAudioClips[i]._samples = new float[monoAudioClips[i]._sampleCount];
            }
            int channelID = 0;
            long index = 0;
            for (long i = 0; i < audioClip._sampleCount; i++)
            {
                monoAudioClips[channelID]._samples[index] = audioClip._samples[i];
                channelID++;
                if (channelID >= audioClip._channelCount)
                {
                    channelID = 0;
                    index++;
                }
            }
            return monoAudioClips;
        }
        public static MonoAudioClip Split(AudioClip audioClip, int channelID) //Follower
        {
            if (audioClip is null)
            {
                throw new System.Exception($"{nameof(audioClip)} cannot be null.");
            }
            if (channelID < 0)
            {
                throw new System.Exception($"{nameof(channelID)} must be greater than or equal to 0.");
            }
            if (channelID >= audioClip._channelCount)
            {
                throw new System.Exception($"{nameof(channelID)} must be less than {nameof(audioClip.ChannelCount)}.");
            }
            MonoAudioClip monoAudioClip = new MonoAudioClip();
            monoAudioClip._encoding = audioClip._encoding;
            monoAudioClip._sampleRate = audioClip._sampleRate;
            monoAudioClip._bitsPerSample = audioClip._bitsPerSample;
            monoAudioClip._blockAlign = audioClip._bitsPerSample / 8;
            monoAudioClip._averageBytesPerSecond = (audioClip._bitsPerSample * audioClip.SampleRate) / 8;
            monoAudioClip._extraSize = audioClip._extraSize;
            monoAudioClip._sampleCount = audioClip._sampleCount / audioClip._channelCount;
            monoAudioClip._samples = new float[monoAudioClip._sampleCount];
            int index = channelID;
            for (long i = 0; i < monoAudioClip._sampleCount; i++)
            {
                monoAudioClip._samples[i] = audioClip._samples[i];
                index += audioClip._channelCount;
            }
            return monoAudioClip;
        }
        public static AudioClip Merge(params MonoAudioClip[] monoAudioClips) //Leader
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
            AudioClip audioClip = new AudioClip();
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
            audioClip._sampleCount = monoAudioClips[0]._sampleCount * monoAudioClips.Length;
            audioClip._samples = new float[audioClip._sampleCount];
            int channelID = 0;
            long index = 0;
            for (long i = 0; i < audioClip._sampleCount; i++)
            {
                audioClip._samples[index] = monoAudioClips[channelID]._samples[i];
                channelID++;
                if (channelID >= audioClip._channelCount)
                {
                    channelID = 0;
                    index++;
                }
            }
            return audioClip;
        }
        #endregion
    }
}