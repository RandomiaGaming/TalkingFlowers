//Approved 0
namespace BetterAL
{
    public sealed class MonoLoopBinding : NAudio.Wave.ISampleProvider
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
        internal MonoLoopBinding()
        {

        }
        public MonoLoopBinding(MonoAudioClip monoAudioClip)
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
                _position = value % _monoAudioClip._sampleCount;
            }
            else if (seekOrigin is System.IO.SeekOrigin.Current)
            {
                _position = (_position + value) % _monoAudioClip._sampleCount;
            }
            else if (seekOrigin is System.IO.SeekOrigin.End)
            {
                _position = (_monoAudioClip._sampleCount - value) % _monoAudioClip._sampleCount; ;
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
                _position = value % _monoAudioClip._sampleCount;
            }
            else if (seekOrigin is System.IO.SeekOrigin.Current)
            {
                _position = (_position + value) % _monoAudioClip._sampleCount;
            }
            else if (seekOrigin is System.IO.SeekOrigin.End)
            {
                _position = (_monoAudioClip._sampleCount - value) % _monoAudioClip._sampleCount; ;
            }
            else
            {
                throw new System.Exception($"{nameof(seekOrigin)} must be a valid {nameof(System.IO.SeekOrigin)}.");
            }
        }
        public void Seek(long value) //Follower
        {
            _position = value % _monoAudioClip._sampleCount;
        }
        public void Seek(int value) //Follower
        {
            _position = value % _monoAudioClip._sampleCount;
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
            for (long i = 0; i < count; i++)
            {
                buffer[offset + i] = _monoAudioClip._samples[_position];
                _position++;
                _position %= _monoAudioClip._sampleCount;
            }
            return count;
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
            for (long i = 0; i < count; i++)
            {
                buffer[offset + i] = _monoAudioClip._samples[_position];
                _position++;
                _position %= _monoAudioClip._sampleCount;
            }
            return count;
        }
        public float[] Read(long count) //Follower
        {
            if (count < 0)
            {
                throw new System.Exception($"{nameof(count)} must be greater than or equal to 0.");
            }
            float[] buffer = new float[count];
            for (long i = 0; i < count; i++)
            {
                buffer[i] = _monoAudioClip._samples[_position];
                _position++;
                _position %= _monoAudioClip._sampleCount;
            }
            return buffer;
        }
        public float[] Read(int count) //Follower
        {
            if (count < 0)
            {
                throw new System.Exception($"{nameof(count)} must be greater than or equal to 0.");
            }
            float[] buffer = new float[count];
            for (long i = 0; i < count; i++)
            {
                buffer[i] = _monoAudioClip._samples[_position];
                _position++;
                _position %= _monoAudioClip._sampleCount;
            }
            return buffer;
        }
        #endregion
    }
}