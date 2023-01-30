using UnityEngine;
using Unity.Entities;
using FinerGames.PitchDetector;

public class AMDFSystem : ComponentSystem
{
    EntityQuery pitchDetectors;
    //ComponentGroup pitchDetectors;
    /*
     * ��������һ������FastYinSystem�����ϵͳ�ࡣ�����̳���UnityEngine.ComponentSystem���࣬������д��OnCreate��OnUpdate������
     * ��OnCreate������,ͨ������EntitiesQueryDesc����ѯ���PitchDetector����������ֵ��pitchDetectors����������ʵ������һ��FastYin���󣬲��������ʺͲ������ڳ�����Ϊ�������롣
     * ��OnUpdate�����У�ʹ��Entities.ForEachѭ���������о���PitchDetector�����ʵ�塣
     * ��ѭ�����У����ȼ�鲥�����Ƿ�Ϊ�գ�����ǣ�ֱ�ӷ��ء�Ȼ��Ӳ������л�ȡһ������Ϊ1024����Ƶ�������������䴫�ݸ�FastYin�����getPitch()������
     * �����������һ��PitchDetectionResult����Ȼ���ȡpitch��midiNote��midiCentsֵ����ֵ��PitchDetector�����
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
