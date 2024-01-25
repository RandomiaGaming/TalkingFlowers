using BetterAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkingFlowers
{
    public enum SoundPlayerState { WaitingToBind, WaitingToPlay, Playing, Paused, ReachedEnd }
    public sealed class SoundPlayer
    {
        private AudioClip boundAudioClip = null;
        private SoundPlayerState state = SoundPlayerState.WaitingToBind;
        public SoundPlayer()
        {

        }
        public static void Bind(AudioClip audioClip)
        {

        }
    }
}
