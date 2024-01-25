//Approved 0
namespace BetterAL
{
    public static class WavFileCodec
    {
        #region Private Constant Variables
        private const int BlockSize = 536870912; //Block size of 512 MB.
        #endregion
        #region Public Static Methods
        public static AudioClip Load(string filePath) //Leader
        {
            if (filePath is null)
            {
                throw new System.Exception($"{nameof(filePath)} cannot not be null.");
            }
            if (filePath is "")
            {
                throw new System.Exception($"{nameof(filePath)} cannot be empty.");
            }
            if (!System.IO.File.Exists(filePath))
            {
                throw new System.Exception($"{nameof(filePath)} does not exist.");
            }
            AudioClip audioClip = new AudioClip();
            NAudio.Wave.AudioFileReader fileReader = new NAudio.Wave.AudioFileReader(filePath);
            NAudio.Wave.WaveFormat sourceFormat = fileReader.WaveFormat;
            audioClip._encoding = sourceFormat.Encoding;
            audioClip._channelCount = sourceFormat.Channels;
            audioClip._sampleRate = sourceFormat.SampleRate;
            audioClip._bitsPerSample = sourceFormat.BitsPerSample;
            audioClip._blockAlign = sourceFormat.BlockAlign;
            audioClip._averageBytesPerSecond = sourceFormat.AverageBytesPerSecond;
            audioClip._extraSize = sourceFormat.ExtraSize;
            audioClip._sampleCount = (fileReader.Length * 8) / sourceFormat.BitsPerSample;
            audioClip._samples = new float[audioClip._sampleCount];
            if (audioClip._sampleCount >= int.MaxValue)
            {
                //Block size of 512 MB.
                float[] block = new float[BlockSize];
                long totalReadSamples = 0;
                while (totalReadSamples + BlockSize < audioClip._sampleCount)
                {
                    fileReader.Read(audioClip._samples, 0, BlockSize);
                    for (long i = 0; i < BlockSize; i++)
                    {
                        audioClip._samples[totalReadSamples + i] = block[i];
                    }
                    totalReadSamples += BlockSize;
                }
                int samplesToRead = (int)(audioClip._sampleCount - totalReadSamples);
                fileReader.Read(block, 0, samplesToRead);
                for (long i = 0; i < samplesToRead; i++)
                {
                    audioClip._samples[totalReadSamples + i] = block[i];
                }
            }
            else
            {
                fileReader.Read(audioClip._samples, 0, (int)audioClip._sampleCount);
            }
            fileReader.Dispose();
            return audioClip;
        }
        public static MonoAudioClip LoadMono(string filePath) //Follower
        {
            if (filePath is null)
            {
                throw new System.Exception($"{nameof(filePath)} cannot be null.");
            }
            if (!System.IO.File.Exists(filePath))
            {
                throw new System.Exception($"{nameof(filePath)} does not exist.");
            }
            MonoAudioClip monoAudioClip = new MonoAudioClip();
            NAudio.Wave.AudioFileReader fileReader = new NAudio.Wave.AudioFileReader(filePath);
            NAudio.Wave.WaveFormat sourceFormat = fileReader.WaveFormat;
            monoAudioClip._encoding = sourceFormat.Encoding;
            if (!(sourceFormat.Channels is 1))
            {
                throw new System.Exception($"Audio file contained more than 1 channel.");
            }
            monoAudioClip._sampleRate = sourceFormat.SampleRate;
            monoAudioClip._bitsPerSample = sourceFormat.BitsPerSample;
            monoAudioClip._blockAlign = sourceFormat.BlockAlign;
            monoAudioClip._averageBytesPerSecond = sourceFormat.AverageBytesPerSecond;
            monoAudioClip._extraSize = sourceFormat.ExtraSize;
            monoAudioClip._sampleCount = (fileReader.Length * 8) / sourceFormat.BitsPerSample;
            monoAudioClip._samples = new float[monoAudioClip._sampleCount];
            if (monoAudioClip._sampleCount >= int.MaxValue)
            {
                //Block size of 512 MB.
                float[] block = new float[BlockSize];
                long totalReadSamples = 0;
                while (totalReadSamples + BlockSize < monoAudioClip._sampleCount)
                {
                    fileReader.Read(monoAudioClip._samples, 0, BlockSize);
                    for (long i = 0; i < BlockSize; i++)
                    {
                        monoAudioClip._samples[totalReadSamples + i] = block[i];
                    }
                    totalReadSamples += BlockSize;
                }
                int samplesToRead = (int)(monoAudioClip._sampleCount - totalReadSamples);
                fileReader.Read(block, 0, samplesToRead);
                for (long i = 0; i < samplesToRead; i++)
                {
                    monoAudioClip._samples[totalReadSamples + i] = block[i];
                }
            }
            else
            {
                fileReader.Read(monoAudioClip._samples, 0, (int)monoAudioClip._sampleCount);
            }
            fileReader.Dispose();
            return monoAudioClip;
        }
        public static void Save(AudioClip audioClip, string filePath) //Leader
        {
            if (audioClip is null)
            {
                throw new System.Exception($"{nameof(audioClip)} cannot be null.");
            }
            if (filePath is null)
            {
                throw new System.Exception($"{nameof(filePath)} cannot not be null.");
            }
            if (filePath is "")
            {
                throw new System.Exception($"{nameof(filePath)} cannot be empty.");
            }
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filePath)))
            {
                throw new System.Exception($"{nameof(filePath)} is within a directory which does not exist.");
            }
            NAudio.Wave.WaveFileWriter fileWriter = new NAudio.Wave.WaveFileWriter(filePath, NAudio.Wave.WaveFormat.CreateCustomFormat(audioClip._encoding, audioClip._sampleRate, audioClip._channelCount, audioClip._averageBytesPerSecond, audioClip._blockAlign, audioClip._bitsPerSample));
            if (audioClip._sampleCount > int.MaxValue)
            {
                float[] block = new float[BlockSize];
                long totalWrittenSamples = 0;
                while (totalWrittenSamples + BlockSize < audioClip._sampleCount)
                {
                    for (long i = 0; i < BlockSize; i++)
                    {
                        audioClip._samples[totalWrittenSamples + i] = block[i];
                    }
                    fileWriter.WriteSamples(block, 0, BlockSize);
                    totalWrittenSamples += BlockSize;
                }
                int samplesToWrite = (int)(audioClip._sampleCount - totalWrittenSamples);
                for (long i = 0; i < samplesToWrite; i++)
                {
                    audioClip._samples[totalWrittenSamples + i] = block[i];
                }
                fileWriter.WriteSamples(block, 0, samplesToWrite);
            }
            else
            {
                fileWriter.WriteSamples(audioClip._samples, 0, (int)audioClip._sampleCount);
            }
            fileWriter.Flush();
            fileWriter.Close();
            fileWriter.Dispose();
        }
        public static void Save(MonoAudioClip monoAudioClip, string filePath) //Follower
        {
            if (monoAudioClip is null)
            {
                throw new System.Exception($"{nameof(monoAudioClip)} cannot be null.");
            }
            if (filePath is null)
            {
                throw new System.Exception($"{nameof(filePath)} cannot not be null.");
            }
            if (filePath is "")
            {
                throw new System.Exception($"{nameof(filePath)} cannot be empty.");
            }
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filePath)))
            {
                throw new System.Exception($"{nameof(filePath)} is within a directory which does not exist.");
            }
            NAudio.Wave.WaveFileWriter fileWriter = new NAudio.Wave.WaveFileWriter(filePath, NAudio.Wave.WaveFormat.CreateCustomFormat(monoAudioClip._encoding, monoAudioClip._sampleRate, 1, monoAudioClip._averageBytesPerSecond, monoAudioClip._blockAlign, monoAudioClip._bitsPerSample));
            if (monoAudioClip._sampleCount > int.MaxValue)
            {
                float[] block = new float[BlockSize];
                long totalWrittenSamples = 0;
                while (totalWrittenSamples + BlockSize < monoAudioClip._sampleCount)
                {
                    for (long i = 0; i < BlockSize; i++)
                    {
                        monoAudioClip._samples[totalWrittenSamples + i] = block[i];
                    }
                    fileWriter.WriteSamples(block, 0, BlockSize);
                    totalWrittenSamples += BlockSize;
                }
                int samplesToWrite = (int)(monoAudioClip._sampleCount - totalWrittenSamples);
                for (long i = 0; i < samplesToWrite; i++)
                {
                    monoAudioClip._samples[totalWrittenSamples + i] = block[i];
                }
                fileWriter.WriteSamples(block, 0, samplesToWrite);
            }
            else
            {
                fileWriter.WriteSamples(monoAudioClip._samples, 0, (int)monoAudioClip._sampleCount);
            }
            fileWriter.Flush();
            fileWriter.Close();
            fileWriter.Dispose();
        }
        #endregion
    }
}