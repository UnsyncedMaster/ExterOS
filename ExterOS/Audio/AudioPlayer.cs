using Cosmos.HAL.Audio;
using Cosmos.System.Audio;
using Cosmos.Debug.Kernel;
using System;
using Cosmos.HAL.Drivers.Audio;

namespace ExterOS.Audio
{
    public static class AudioPlayer
    {
        private static readonly Debugger Debugger;
        public static readonly AudioMixer Mixer;
        private static readonly AudioManager AM;

        static AudioPlayer()
        {
            //Broke for some odd reason??
            Debugger = new Debugger("AudioSystem");
            Mixer = new AudioMixer();
            AM = new AudioManager();
            IsAvailable = false;
        }

        public static bool IsAvailable { get; private set; }

        public static void Init()
        {
            try
            {
                Debugger.Send("Initializing Audio...");

                var audioDevice = AC97.Initialize(4096);
                if (audioDevice != null)
                {
                    AM.Output = audioDevice;
                    AM.Stream = Mixer;
                    AM.Enable();

                    Debugger.Send("Audio Initialized Successfully.");
                    IsAvailable = true;
                }
                else
                {
                    throw new Exception("AC97 Init Failed.");
                }
            }
            catch (Exception ex)
            {
                Debugger.Send($"Audio Initialization Failed: {ex.Message}");
                IsAvailable = false;
            }
        }

        public static void Play(AudioStream stream)
        {
            if (!IsAvailable)
            {
                Debugger.Send("Audio Is Not Available. Cannot Play Audio.");
                return;
            }

            Mixer.Streams.Add(stream);
            Debugger.Send("Playing Audio Stream.");
        }
    }

}
