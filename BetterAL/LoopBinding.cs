//Approved 0
namespace BetterAL
{
    public sealed class LoopBinding : NAudio.Wave.ISampleProvider
    {
        #region Internal Variables
        internal AudioClip _audioClip;
        internal long _position;
        #endregion
        #region Public Variables
        public NAudio.Wave.WaveFormat WaveFormat => _audioClip.WaveFormat;
        public AudioClip AudioClip => _audioClip;
        public long Position => _position;
        public long SampleCount => _audioClip._sampleCount;
        public NAudio.Wave.WaveFormatEncoding Encoding => _audioClip._encoding;
        public int SampleRate => _audioClip._sampleRate;
        public int BitsPerSample => _audioClip._bitsPerSample;
        public int BlockAlign => _audioClip._blockAlign;
        public int AverageBytesPerSecond => _audioClip._averageBytesPerSecond;
        public int ExtraSize => _audioClip._extraSize;
        #endregion
        #region Constructors
        internal LoopBinding()
        {

        }
        public LoopBinding(AudioClip audioClip)
        {
            if (audioClip is null)
            {
                throw new System.Exception($"{nameof(audioClip)} cannot be null.");
            }
            _audioClip = audioClip;
        }
        #endregion
        #region Public Methods
        public void SeekBeginning() //Leader
        {
            _position = 0;
        }
        public void SeekEnd() //Leader
        {
            _position = _audioClip._sampleCount;
        }
        public void Seek(long value, System.IO.SeekOrigin seekOrigin) //Leader
        {
            if (!(value % _audioClip._channelCount is 0))
            {
                throw new System.Exception($"{nameof(value)} must be an even multiple of {nameof(_audioClip.ChannelCount)}.");
            }
            if (seekOrigin is System.IO.SeekOrigin.Begin)
            {
                _position = value % _audioClip._sampleCount;
            }
            else if (seekOrigin is System.IO.SeekOrigin.Current)
            {
                _position = (_position + value) % _audioClip._sampleCount;
            }
            else if (seekOrigin is System.IO.SeekOrigin.End)
            {
                _position = (_audioClip._sampleCount - value) % _audioClip._sampleCount; ;
            }
            else
            {
                throw new System.Exception($"{nameof(seekOrigin)} must be a valid {nameof(System.IO.SeekOrigin)}.");
            }
        }
        public void Seek(int value, System.IO.SeekOrigin seekOrigin) //Follower
        {
            if (!(value % _audioClip._channelCount is 0))
            {
                throw new System.Exception($"{nameof(value)} must be an even multiple of {nameof(_audioClip.ChannelCount)}.");
            }
            if (seekOrigin is System.IO.SeekOrigin.Begin)
            {
                _position = value % _audioClip._sampleCount;
            }
            else if (seekOrigin is System.IO.SeekOrigin.Current)
            {
                _position = (_position + value) % _audioClip._sampleCount;
            }
            else if (seekOrigin is System.IO.SeekOrigin.End)
            {
                _position = (_audioClip._sampleCount - value) % _audioClip._sampleCount; ;
            }
            else
            {
                throw new System.Exception($"{nameof(seekOrigin)} must be a valid {nameof(System.IO.SeekOrigin)}.");
            }
        }
        public void Seek(long value) //Follower
        {
            if (!(value % _audioClip._channelCount is 0))
            {
                throw new System.Exception($"{nameof(value)} must be an even multiple of {nameof(_audioClip.ChannelCount)}.");
            }
            _position = value % _audioClip._sampleCount;
        }
        public void Seek(int value) //Follower
        {
            if (!(value % _audioClip._channelCount is 0))
            {
                throw new System.Exception($"{nameof(value)} must be an even multiple of {nameof(_audioClip.ChannelCount)}.");
            }
            _position = value % _audioClip._sampleCount;
        }
        public long Read(float[] buffer, long offset, long count) //Leader
        {
            if (!(count % _audioClip._channelCount is 0))
            {
                throw new System.Exception($"{nameof(count)} must be an even multiple of {nameof(_audioClip.ChannelCount)}.");
            }
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
                buffer[offset + i] = _audioClip._samples[_position];
                _position++;
                _position %= _audioClip._sampleCount;
            }
            return count;
        }
        public int Read(float[] buffer, int offset, int count) //Follower
        {
            if (!(count % _audioClip._channelCount is 0))
            {
                throw new System.Exception($"{nameof(count)} must be an even multiple of {nameof(_audioClip.ChannelCount)}.");
            }
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
                buffer[offset + i] = _audioClip._samples[_position];
                _position++;
                _position %= _audioClip._sampleCount;
            }
            return count;
        }
        public float[] Read(long count) //Follower
        {
            if (!(count % _audioClip._channelCount is 0))
            {
                throw new System.Exception($"{nameof(count)} must be an even multiple of {nameof(_audioClip.ChannelCount)}.");
            }
            if (count < 0)
            {
                throw new System.Exception($"{nameof(count)} must be greater than or equal to 0.");
            }
            float[] buffer = new float[count];
            for (long i = 0; i < count; i++)
            {
                buffer[i] = _audioClip._samples[_position];
                _position++;
                _position %= _audioClip._sampleCount;
            }
            return buffer;
        }
        public float[] Read(int count) //Follower
        {
            if (!(count % _audioClip._channelCount is 0))
            {
                throw new System.Exception($"{nameof(count)} must be an even multiple of {nameof(_audioClip.ChannelCount)}.");
            }
            if (count < 0)
            {
                throw new System.Exception($"{nameof(count)} must be greater than or equal to 0.");
            }
            float[] buffer = new float[count];
            for (long i = 0; i < count; i++)
            {
                buffer[i] = _audioClip._samples[_position];
                _position++;
                _position %= _audioClip._sampleCount;
            }
            return buffer;
        }
        #endregion
    }
}