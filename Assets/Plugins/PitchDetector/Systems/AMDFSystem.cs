using UnityEngine;
using Unity.Entities;
using FinerGames.PitchDetector;

public class AMDFSystem : ComponentSystem
{
    EntityQuery pitchDetectors;
    //ComponentGroup pitchDetectors;
    /*
     * 它定义了一个叫做FastYinSystem的组件系统类。这个类继承自UnityEngine.ComponentSystem基类，并且重写了OnCreate和OnUpdate方法。
     * 在OnCreate方法中,通过定义EntitiesQueryDesc来查询组件PitchDetector，并将它赋值给pitchDetectors变量。它还实例化了一个FastYin对象，并将采样率和采样窗口长度作为参数传入。
     * 在OnUpdate方法中，使用Entities.ForEach循环遍历所有具有PitchDetector组件的实体。
     * 在循环体中，首先检查播放器是否为空，如果是，直接返回。然后从播放器中获取一个长度为1024的音频缓冲区，并将其传递给FastYin对象的getPitch()方法。
     * 这个方法返回一个PitchDetectionResult对象，然后获取pitch和midiNote、midiCents值并赋值给PitchDetector组件。
     */

    AMDF amdf;

    protected override void OnCreate()
    {
        base.OnCreate();

        var query = new EntityQueryDesc()
        {
            All = new ComponentType[] { typeof(PitchDetector), },
        };
        pitchDetectors = GetEntityQuery(query);
        //pitchDetectors = GetComponentGroup(query);

        amdf = new AMDF(44100, 1024);
    }
    protected override void OnUpdate()
    {
        Entities.ForEach((PitchDetector detector) =>
        {
            if (detector.Source == null)
                return;

            if (detector.Source.clip == null)
                return;

            var buffer = new float[1024];
            detector.Source.GetOutputData(buffer, 0);

            //TODO -> jobify + burst
            var result = amdf.getPitch(buffer);

            var pitch = result.getPitch();
            var midiNote = 0;
            var midiCents = 0;

            Pitch.PitchDsp.PitchToMidiNote(pitch, out midiNote, out midiCents);

            detector.Pitch = pitch;
            detector.MidiNote = midiNote;
            detector.MidiCents = midiCents;
            //}, pitchDetectors);
        });
    }

}
