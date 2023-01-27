using Unity.Entities;
using UnityEngine;
using UnityEngine.Audio;

namespace FinerGames.PitchDetector
{
    public class MicrophoneInputSystem : ComponentSystem
    {
        EntityQuery microphoneInputs;

        protected override void OnCreate()
        {
            base.OnCreate();

            var query = new EntityQueryDesc()
            {
                All = new ComponentType[] { typeof(MicrophoneInput), },
            };
            microphoneInputs = GetEntityQuery(query);
        }

        protected override void OnUpdate()
        {
            Entities.ForEach((MicrophoneInput input) =>
            {
                if (input.Source == null)
                    return;

                if (input.IsRecording && !Microphone.IsRecording(input.DeviceName))
                {
                    input.Source.Stop();
                    input.Source.loop = true;
                    input.Source.clip = Microphone.Start(input.DeviceName, true, 1, input.SampleRate);
                    input.Source.Play();

                    //int dspBufferSize, dspNumBuffers;
                    //AudioSettings.GetDSPBufferSize(out dspBufferSize, out dspNumBuffers);
                    //input.Source.timeSamples = (Microphone.GetPosition(input.DeviceName) + AudioSettings.outputSampleRate - 3 * dspBufferSize * dspNumBuffers) % AudioSettings.outputSampleRate;
                }
                else if (!input.IsRecording && Microphone.IsRecording(input.DeviceName))
                {
                    input.Source.Stop();
                    Microphone.End(input.DeviceName);
                }
            //}, microphoneInputs);
            });
        }
    }
}
