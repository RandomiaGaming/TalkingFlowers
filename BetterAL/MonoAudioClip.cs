﻿//Approved 0
namespace BetterAL
{
    public sealed class MonoAudioClip
    {
        #region Internal Variables
        internal float[] _samples;
        internal long _sampleCount;
        internal NAudio.Wave.WaveFormatEncoding _encoding;
        internal int _sampleRate;
        internal int _bitsPerSample;
        internal int _blockAlign;
        internal int _averageBytesPerSecond;
        internal int _extraSize;
        #endregion
        #region Public Variables
        public NAudio.Wave.WaveFormat WaveFormat => NAudio.Wave.WaveFormat.CreateCustomFormat(_encoding, _sampleRate, 1, _averageBytesPerSecond, _blockAlign, _bitsPerSample);
        public long SampleCount => _sampleCount;
        public NAudio.Wave.WaveFormatEncoding Encoding => _encoding;
        public int SampleRate => _sampleRate;
        public int BitsPerSample => _bitsPerSample;
        public int BlockAlign => _blockAlign;
        public int AverageBytesPerSecond => _averageBytesPerSecond;
        public int ExtraSize => _extraSize;
        #endregion
        #region Constructors
        internal MonoAudioClip()
        {

        }
        public MonoAudioClip(float[] samples, NAudio.Wave.WaveFormat waveFormat)
        {
            if (waveFormat is null)
            {
                throw new System.Exception($"{nameof(waveFormat)} cannot be null.");
            }
            if (samples is null)
            {
                throw new System.Exception($"{nameof(samples)} cannot be null.");
            }
            _encoding = waveFormat.Encoding;
            if (!(waveFormat.Channels is 1))
            {
                throw new System.Exception($"{nameof(waveFormat)} contained more than 1 channel.");
            }
            _sampleRate = waveFormat.SampleRate;
            _bitsPerSample = waveFormat.BitsPerSample;
            _blockAlign = waveFormat.BlockAlign;
            _averageBytesPerSecond = waveFormat.AverageBytesPerSecond;
            _extraSize = waveFormat.ExtraSize;
            _sampleCount = samples.LongLength;
            _samples = new float[_sampleCount];
            for (long i = 0; i < _sampleCount; i++)
            {
                _samples[i] = samples[i];
            }
        }
        public MonoAudioClip(float[] samples, NAudio.Wave.WaveFormatEncoding encoding, int sampleRate, int bitsPerSample, int blockAlign, int averageBytesPerSecond, int extraSize)
        {
            if (samples is null)
            {
                throw new System.Exception($"{nameof(samples)} cannot be null.");
            }
            if (sampleRate <= 0)
            {
                throw new System.Exception($"{nameof(sampleRate)} must be greater than 0.");
            }
            if (bitsPerSample <= 0)
            {
                throw new System.Exception($"{nameof(bitsPerSample)} must be greater than 0.");
            }
            _encoding = encoding;
            _sampleRate = sampleRate;
            _bitsPerSample = bitsPerSample;
            _blockAlign = blockAlign;
            _averageBytesPerSecond = averageBytesPerSecond;
            _extraSize = extraSize;
            _sampleCount = samples.LongLength;
            _samples = new float[_sampleCount];
            for (long i = 0; i < _sampleCount; i++)
            {
                _samples[i] = samples[i];
            }
        }
        #endregion
        #region Public Methods
        public void Read(float[] buffer, long offset, long startIndex, long count) //Leader
        {
            if (offset < 0)
            {
                throw new System.Exception($"{nameof(offset)} must be greater than or equal to 0.");
            }
            if (offset >= buffer.LongLength)
            {
                throw new System.Exception($"{nameof(offset)} must be less than {nameof(buffer)}.{nameof(buffer.Length)}.");
            }
            if (startIndex < 0)
            {
                throw new System.Exception($"{nameof(startIndex)} must be greater than or equal to 0.");
            }
            if (startIndex >= _sampleCount)
            {
                throw new System.Exception($"{nameof(startIndex)} must be less than {nameof(SampleCount)}.");
            }
            if (count < 0)
            {
                throw new System.Exception($"{nameof(count)} must be greater than or equal to 0.");
            }
            if (count + offset >= buffer.LongLength)
            {
                throw new System.Exception($"{nameof(count)} + {nameof(offset)} must be less than {nameof(buffer)}.{nameof(buffer.Length)}.");
            }
            if (count + startIndex >= _sampleCount)
            {
                throw new System.Exception($"{nameof(count)} + {nameof(startIndex)} must be less than {nameof(SampleCount)}.");
            }
            for (long i = 0; i < count; i++)
            {
                buffer[offset + i] = _samples[startIndex + i];
            }
        }
        public void Read(float[] buffer, int offset, int startIndex, int count) //Follower
        {
            if (offset < 0)
            {
                throw new System.Exception($"{nameof(offset)} must be greater than or equal to 0.");
            }
            if (offset >= buffer.LongLength)
            {
                throw new System.Exception($"{nameof(offset)} must be less than {nameof(buffer)}.{nameof(buffer.Length)}.");
            }
            if (startIndex < 0)
            {
                throw new System.Exception($"{nameof(startIndex)} must be greater than or equal to 0.");
            }
            if (startIndex >= _sampleCount)
            {
                throw new System.Exception($"{nameof(startIndex)} must be less than {nameof(SampleCount)}.");
            }
            if (count < 0)
            {
                throw new System.Exception($"{nameof(count)} must be greater than or equal to 0.");
            }
            if (count + offset >= buffer.LongLength)
            {
                throw new System.Exception($"{nameof(count)} + {nameof(offset)} must be less than {nameof(buffer)}.{nameof(buffer.Length)}.");
            }
            if (count + startIndex >= _sampleCount)
            {
                throw new System.Exception($"{nameof(count)} + {nameof(startIndex)} must be less than {nameof(SampleCount)}.");
            }
            for (long i = 0; i < count; i++)
            {
                buffer[offset + i] = _samples[startIndex + i];
            }
        }
        public float[] Read(long startIndex, long count) //Follower
        {
            if (startIndex < 0)
            {
                throw new System.Exception($"{nameof(startIndex)} must be greater than or equal to 0.");
            }
            if (startIndex >= _sampleCount)
            {
                throw new System.Exception($"{nameof(startIndex)} must be less than {nameof(SampleCount)}.");
            }
            if (count < 0)
            {
                throw new System.Exception($"{nameof(count)} must be greater than or equal to 0.");
            }
            if (count + startIndex >= _sampleCount)
            {
                throw new System.Exception($"{nameof(count)} + {nameof(startIndex)} must be less than {nameof(SampleCount)}.");
            }
            float[] buffer = new float[count];
            for (long i = 0; i < count; i++)
            {
                buffer[i] = _samples[startIndex + i];
            }
            return buffer;
        }
        public float[] Read(int startIndex, int count) //Follower
        {
            if (startIndex < 0)
            {
                throw new System.Exception($"{nameof(startIndex)} must be greater than or equal to 0.");
            }
            if (startIndex >= _sampleCount)
            {
                throw new System.Exception($"{nameof(startIndex)} must be less than {nameof(SampleCount)}.");
            }
            if (count < 0)
            {
                throw new System.Exception($"{nameof(count)} must be greater than or equal to 0.");
            }
            if (count + startIndex >= _sampleCount)
            {
                throw new System.Exception($"{nameof(count)} + {nameof(startIndex)} must be less than {nameof(SampleCount)}.");
            }
            float[] buffer = new float[count];
            for (long i = 0; i < count; i++)
            {
                buffer[i] = _samples[startIndex + i];
            }
            return buffer;
        }
        public void ToArray(float[] buffer, long offset) //Leader
        {
            if (offset < 0)
            {
                throw new System.Exception($"{nameof(offset)} must be greater than or equal to 0.");
            }
            if (offset >= buffer.LongLength)
            {
                throw new System.Exception($"{nameof(offset)} must be less than {nameof(buffer)}.{nameof(buffer.Length)}.");
            }
            if (_sampleCount + offset >= buffer.LongLength)
            {
                throw new System.Exception($"{nameof(SampleCount)} + {nameof(offset)} must be less than {nameof(buffer)}.{nameof(buffer.Length)}.");
            }
            for (long i = 0; i < _sampleCount; i++)
            {
                buffer[i + offset] = _samples[i];
            }
        }
        public float[] ToArray() //Follower
        {
            float[] buffer = new float[_sampleCount];
            for (long i = 0; i < _sampleCount; i++)
            {
                buffer[i] = _samples[i];
            }
            return buffer;
        }
        #endregion
    }
}