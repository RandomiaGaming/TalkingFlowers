//Approved 0
namespace BetterAL
{
    public static class Player
    {
        #region Public Static Methods
        public static void PlaySync(AudioClip audioClip, float volume = 1.0f, int outputDevice = 0) //Leader
        {
            if (audioClip is null)
            {
                throw new System.Exception($"{nameof(audioClip)} cannot be null.");
            }
            if (volume < 0.0f)
            {
                throw new System.Exception($"{nameof(volume)} must be greater than or equal to 0.");
            }
            if (volume > 1.0f)
            {
                throw new System.Exception($"{nameof(volume)} must be less than or equal to 1");
            }
            if (outputDevice < 0)
            {
                throw new System.Exception($"{nameof(outputDevice)} must be greater than or equal to 0.");
            }
            NAudio.Wave.WaveOutEvent waveOutEvent = new NAudio.Wave.WaveOutEvent();
            waveOutEvent.Volume = volume;
            waveOutEvent.DeviceNumber = outputDevice;
            Binding binding = new Binding(audioClip);
            NAudio.Wave.WaveExtensionMethods.Init(waveOutEvent, binding);
            System.Threading.Tasks.TaskCompletionSource<bool> taskCompletionSource = new System.Threading.Tasks.TaskCompletionSource<bool>();
            waveOutEvent.PlaybackStopped += (object sender, NAudio.Wave.StoppedEventArgs e) => { taskCompletionSource.SetResult(true); };
            waveOutEvent.Play();
            taskCompletionSource.Task.Wait();
            waveOutEvent.Dispose();
        }
        public static void PlaySync(MonoAudioClip monoAudioClip, float volume = 1.0f, int outputDevice = 0) //Follower
        {
            if (monoAudioClip is null)
            {
                throw new System.Exception($"{nameof(monoAudioClip)} cannot be null.");
            }
            if (volume < 0.0f)
            {
                throw new System.Exception($"{nameof(volume)} must be greater than or equal to 0.");
            }
            if (volume > 1)
            {
                throw new System.Exception($"{nameof(volume)} must be less than or equal to 1");
            }
            if (outputDevice < 0)
            {
                throw new System.Exception($"{nameof(outputDevice)} must be greater than or equal to 0.");
            }
            NAudio.Wave.WaveOutEvent waveOutEvent = new NAudio.Wave.WaveOutEvent();
            waveOutEvent.Volume = volume;
            waveOutEvent.DeviceNumber = outputDevice;
            MonoBinding monoBinding = new MonoBinding(monoAudioClip);
            NAudio.Wave.WaveExtensionMethods.Init(waveOutEvent, monoBinding);
            System.Threading.Tasks.TaskCompletionSource<bool> taskCompletionSource = new System.Threading.Tasks.TaskCompletionSource<bool>();
            waveOutEvent.PlaybackStopped += (object sender, NAudio.Wave.StoppedEventArgs e) => { taskCompletionSource.SetResult(true); };
            waveOutEvent.Play();
            taskCompletionSource.Task.Wait();
            waveOutEvent.Dispose();
        }
        public static void PlaySync(NAudio.Wave.ISampleProvider sampleProvider, float volume = 1.0f, int outputDevice = 0) //Follower
        {
            if (sampleProvider is null)
            {
                throw new System.Exception($"{nameof(sampleProvider)} cannot be null.");
            }
            if (volume < 0.0f)
            {
                throw new System.Exception($"{nameof(volume)} must be greater than or equal to 0.");
            }
            if (volume > 1)
            {
                throw new System.Exception($"{nameof(volume)} must be less than or equal to 1");
            }
            if (outputDevice < 0)
            {
                throw new System.Exception($"{nameof(outputDevice)} must be greater than or equal to 0.");
            }
            NAudio.Wave.WaveOutEvent waveOutEvent = new NAudio.Wave.WaveOutEvent();
            waveOutEvent.Volume = volume;
            waveOutEvent.DeviceNumber = outputDevice;
            NAudio.Wave.WaveExtensionMethods.Init(waveOutEvent, sampleProvider);
            System.Threading.Tasks.TaskCompletionSource<bool> taskCompletionSource = new System.Threading.Tasks.TaskCompletionSource<bool>();
            waveOutEvent.PlaybackStopped += (object sender, NAudio.Wave.StoppedEventArgs e) => { taskCompletionSource.SetResult(true); };
            waveOutEvent.Play();
            taskCompletionSource.Task.Wait();
            waveOutEvent.Dispose();
        }
        #endregion
    }
}