//Approved 0
namespace BetterAL
{
    public sealed class MonoBinding : NAudio.Wave.ISampleProvider
    {
        #region Internal Variables
        internal MonoAudioClip _monoAudioClip;
        internal long _position;
        #endregion
        #region Public Variables
        public NAudio.Wave.WaveFormat WaveFormat => _monoAudioClip.WaveFormat;
        public MonoAudioClip MonoAudioClip => _monoAudioClip;
        public long Position => _position;
        public long SampleCount => _monoAudioClip._sampleCount;
        public NAudio.Wave.WaveFormatEncoding Encoding => _monoAudioClip._encoding;
        public int SampleRate => _monoAudioClip._sampleRate;
        public int BitsPerSample => _monoAudioClip._bitsPerSample;
        public int BlockAlign => _monoAudioClip._blockAlign;
        public int AverageBytesPerSecond => _monoAudioClip._averageBytesPerSecond;
        public int ExtraSize => _monoAudioClip._extraSize;
        #endregion
        #region Constructors
        internal MonoBinding()
        {

        }
        public MonoBinding(MonoAudioClip monoAudioClip)
        {
            if (monoAudioClip is null)
            {
                throw new System.Exception($"{nameof(monoAudioClip)} cannot be null.");
            }
            _monoAudioClip = monoAudioClip;
        }
        #endregion
        #region Public Methods
        public void SeekBeginning() //Leader
        {
            _position = 0;
        }
        public void SeekEnd() //Leader
        {
            _position = _monoAudioClip._sampleCount;
        }
        public void Seek(long value, System.IO.SeekOrigin seekOrigin) //Leader
        {
            if (seekOrigin is System.IO.SeekOrigin.Begin)
            {
                if (value < 0)
                {
                    throw new System.Exception($"{nameof(value)} must be greater than or equal to 0.");
                }
                if (value > _monoAudioClip._sampleCount)
                {
                    throw new System.Exception($"{nameof(value)} must be less than or equal to {nameof(SampleCount)}.");
                }
                _position = value;
            }
            else if (seekOrigin is System.IO.SeekOrigin.Current)
            {
                if (_position + value < 0)
                {
                    throw new System.Exception($"{nameof(Position)} + {nameof(value)} must be greater than or equal to 0.");
                }
                if (_position + value > _monoAudioClip._sampleCount)
                {
                    throw new System.Exception($"{nameof(Position)} + {nameof(value)} must be less than or equal to {nameof(SampleCount)}.");
                }
                _position = _position + value;
            }
            else if (seekOrigin is System.IO.SeekOrigin.End)
            {
                if (_monoAudioClip._sampleCount - value < 0)
                {
                    throw new System.Exception($"{nameof(SampleCount)} - {nameof(value)} must be greater than or equal to 0.");
                }
                if (_monoAudioClip._sampleCount - value > _monoAudioClip._sampleCount)
                {
                    throw new System.Exception($"{nameof(SampleCount)} - {nameof(value)} must be less than or equal to {nameof(SampleCount)}.");
                }
                _position = _monoAudioClip._sampleCount - value;
            }
            else
            {
                throw new System.Exception($"{nameof(seekOrigin)} must be a valid {nameof(System.IO.SeekOrigin)}.");
            }
        }
        public void Seek(int value, System.IO.SeekOrigin seekOrigin) //Follower
        {
            if (seekOrigin is System.IO.SeekOrigin.Begin)
            {
                if (value < 0)
                {
                    throw new System.Exception($"{nameof(value)} must be greater than or equal to 0.");
                }
                if (value > _monoAudioClip._sampleCount)
                {
                    throw new System.Exception($"{nameof(value)} must be less than or equal to {nameof(SampleCount)}.");
                }
                _position = value;
            }
            else if (seekOrigin is System.IO.SeekOrigin.Current)
            {
                if (_position + value < 0)
                {
                    throw new System.Exception($"{nameof(Position)} + {nameof(value)} must be greater than or equal to 0.");
                }
                if (_position + value > _monoAudioClip._sampleCount)
                {
                    throw new System.Exception($"{nameof(Position)} + {nameof(value)} must be less than or equal to {nameof(SampleCount)}.");
                }
                _position = _position + value;
            }
            else if (seekOrigin is System.IO.SeekOrigin.End)
            {
                if (_monoAudioClip._sampleCount - value < 0)
                {
                    throw new System.Exception($"{nameof(SampleCount)} - {nameof(value)} must be greater than or equal to 0.");
                }
                if (_monoAudioClip._sampleCount - value > _monoAudioClip._sampleCount)
                {
                    throw new System.Exception($"{nameof(SampleCount)} - {nameof(value)} must be less than or equal to {nameof(SampleCount)}.");
                }
                _position = _monoAudioClip._sampleCount - value;
            }
            else
            {
                throw new System.Exception($"{nameof(seekOrigin)} must be a valid {nameof(System.IO.SeekOrigin)}.");
            }
        }
        public void Seek(long value) //Follower
        {
            if (value < 0)
            {
                throw new System.Exception($"{nameof(value)} must be greater than or equal to 0.");
            }
            if (value > _monoAudioClip._sampleCount)
            {
                throw new System.Exception($"{nameof(value)} must be less than or equal to {nameof(SampleCount)}.");
            }
            _position = value;
        }
        public void Seek(int value) //Follower
        {
            if (value < 0)
            {
                throw new System.Exception($"{nameof(value)} must be greater than or equal to 0.");
            }
            if (value > _monoAudioClip._sampleCount)
            {
                throw new System.Exception($"{nameof(value)} must be less than or equal to {nameof(SampleCount)}.");
            }
            _position = value;
        }
        public long Read(float[] buffer, long offset, long count) //Leader
        {
            if (offset < 0)
            {
                throw new System.Exception($"{nameof(offset)} must be greater than or equal to 0.");
            }
            if (count < 0)
            {
                throw new System.Exception($"{nameof(count)} must be greater than or equal to 0.");
            }
            if (offset >= buffer.LongLength)
            {
                throw new System.Exception($"{nameof(offset)} must be less than {nameof(buffer)}.{nameof(buffer.Length)}.");
            }
            if (count >= buffer.LongLength)
            {
                throw new System.Exception($"{nameof(count)} must be less than {nameof(buffer)}.{nameof(buffer.Length)}.");
            }
            if (offset + count >= buffer.LongLength)
            {
                throw new System.Exception($"{nameof(count)} + {nameof(offset)} must be less than {nameof(buffer)}.{nameof(buffer.Length)}.");
            }
            long readCount = count;
            if (_position + count > _monoAudioClip._sampleCount)
            {
                readCount = _monoAudioClip._sampleCount - _position;
            }
            for (long i = 0; i < readCount; i++)
            {
                buffer[offset + i] = _monoAudioClip._samples[_position + i];
            }
            for (long i = readCount; i < count; i++)
            {
                buffer[offset + i] = 0;
            }
            _position += readCount;
            return readCount;
        }
        public int Read(float[] buffer, int offset, int count) //Follower
        {
            if (offset < 0)
            {
                throw new System.Exception($"{nameof(offset)} must be greater than or equal to 0.");
            }
            if (count < 0)
            {
                throw new System.Exception($"{nameof(count)} must be greater than or equal to 0.");
            }
            if (offset >= buffer.LongLength)
            {
                throw new System.Exception($"{nameof(offset)} must be less than {nameof(buffer)}.{nameof(buffer.Length)}.");
            }
            if (count >= buffer.LongLength)
            {
                throw new System.Exception($"{nameof(count)} must be less than {nameof(buffer)}.{nameof(buffer.Length)}.");
            }
            if (offset + count >= buffer.LongLength)
            {
                throw new System.Exception($"{nameof(count)} + {nameof(offset)} must be less than {nameof(buffer)}.{nameof(buffer.Length)}.");
            }
            long readCount = count;
            if (_position + count > _monoAudioClip._sampleCount)
            {
                readCount = _monoAudioClip._sampleCount - _position;
            }
            for (long i = 0; i < readCount; i++)
            {
                buffer[offset + i] = _monoAudioClip._samples[_position + i];
            }
            for (long i = readCount; i < count; i++)
            {
                buffer[offset + i] = 0;
            }
            _position += readCount;
            return (int)readCount;
        }
        public float[] Read(long count) //Leader
        {
            if (count < 0)
            {
                throw new System.Exception($"{nameof(count)} must be greater than or equal to 0.");
            }
            long readCount = count;
            if (_position + count > _monoAudioClip._sampleCount)
            {
                readCount = _monoAudioClip._sampleCount - _position;
            }
            float[] buffer = new float[count];
            for (long i = 0; i < readCount; i++)
            {
                buffer[i] = _monoAudioClip._samples[_position + i];
            }
            _position += readCount;
            return buffer;
        }
        public float[] Read(int count) //Follower
        {
            if (count < 0)
            {
                throw new System.Exception($"{nameof(count)} must be greater than or equal to 0.");
            }
            long readCount = count;
            if (_position + count > _monoAudioClip._sampleCount)
            {
                readCount = _monoAudioClip._sampleCount - _position;
            }
            float[] buffer = new float[count];
            for (long i = 0; i < readCount; i++)
            {
                buffer[i] = _monoAudioClip._samples[_position + i];
            }
            _position += readCount;
            return buffer;
        }
        #endregion
    }
}