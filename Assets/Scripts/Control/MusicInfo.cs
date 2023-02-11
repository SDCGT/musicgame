using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using symbol;
using util;

namespace xmlParser
{
    public class MusicInfo : MonoBehaviour
    {
        int midiID = -1;
        double frequent = 0;
        XmlParser parser = new XmlParser("Assets/Materials/MusicXml/2.xml");
        private List<Symbol> SymbolMeasure = new List<Symbol>();
        private List<Note> NoteList = new List<Note>();
        private List<double> ScorePitchList = new List<double>();

        private Beat beat;
        float endTime;
        int beattypeint;
        int beatint;
        int allduration;
        int perminute;
        int allBeats;
        int measureCount;
        public Timer time;

        // Start is called before the first frame update

        private void Awake()
        {
            NoteList = parser.GetNoteList();
            //Debug.Log("NoteCount" + NoteList.Count);
            measureCount = parser.GetMeasureList().Count;
            SymbolMeasure = parser.GetHighSymbolList();
            beat = parser.GetBeat();
            string perminutestr = parser.GetPerMinute();
            CountAllDuration();
            int.TryParse(perminutestr, out perminute);
            int.TryParse(beat.GetBeats(), out beatint);
            int.TryParse(beat.GetBeatType(), out beattypeint);
            //Debug.Log(SymbolMeasure.Count);
            //Debug.Log(measureCount);
            //Debug.Log(perminute);
            allBeats = beattypeint * measureCount;
            //Debug.Log(allBeats);
            //Debug.Log(perminute);
            TotalTime();
        }
        void Start()
        {
            SetStartTime();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            frequent = 440 * Mathf.Pow(2, (midiID - 69) / 12);
            Debug.Log(midiID + "scoreFrequent" + frequent);
            ScorePitchList.Add(frequent);
            PitchRecord.SetScorePitchList(ScorePitchList);

            Debug.Log(time.GetGameTime());
            if (0<time.GetGameTime() && time.GetGameTime()< endTime)//曲目时间内，获取曲目当前音高
            {
              midiID = GetMIDIID(time.GetGameTime());
            }

            if (time.GetGameTime() >= endTime)
            {
                midiID = 0;
            }
        }

        void CountAllDuration()//计算duration总和
        {

            for (int i = 0; i < SymbolMeasure.Count; i++)
            {
                allduration = allduration + SymbolMeasure[i].GetDuration();
            }
        }

        void SetStartTime()//设置每个Symbol的起始时间
        {
            float starttime = 0;
            for (int i = 0; i < SymbolMeasure.Count; i++)
            {
                if (allduration != 0)
                {
                    int duration = SymbolMeasure[i].GetDuration();
                    //Debug.Log(perminute);
                    float durationPercent = duration * 1.0f / (1.0f * allduration);
                    float BeatsPercent = allBeats * 1.0f / (1.0f * perminute);
                    float durationTime = durationPercent * BeatsPercent * 60;
                    starttime = starttime + durationTime;
                    SymbolMeasure[i].SetStartTime(starttime);
                    //Debug.Log(SymbolMeasure[i].GetStartTime());
                }
            }
        }
        int GetMIDIID(float time)//按时间节点获取音高
        {
            int MidiID = midiID;
            for (int i = 0; i < SymbolMeasure.Count; i++)
            {
                if ((time - SymbolMeasure[i].GetStartTime()) < 0.1f && (time - SymbolMeasure[i].GetStartTime()) > 0)
                {
                    if (i < NoteList.Count)
                    {
                        MidiID = GetDigitizedPitch(NoteList[i].GetStep(), NoteList[i].GetOctave());
                        //Debug.Log("in GetDigitizedPitch:" + MidiID);
                    }
                    if (i >= NoteList.Count)//此处代码有很大问题需要修改
                    {
                        midiID = 0;//表示休止
                        Debug.Log("等于0时" + midiID);
                    }
                }
            }
            return MidiID;
        }

        private int GetDigitizedPitch(string step, string octave)//换算MIDIID
        {
            int digitizedPitch = 1;

            switch (step)
            {
                case "C": digitizedPitch = 1; break;
                case "D": digitizedPitch = 2; break;
                case "E": digitizedPitch = 3; break;
                case "F": digitizedPitch = 4; break;
                case "G": digitizedPitch = 5; break;
                case "A": digitizedPitch = 6; break;
                case "B": digitizedPitch = 7; break;
            }
            return digitizedPitch + (int.Parse(octave) - 1) * 7;
        }

        void TotalTime()//计算曲目总时长
        {
            float BeatsPercent = allBeats * 1.0f / (1.0f * perminute);
            endTime = BeatsPercent * 60.0f;
            StaticMusicInfo.SetEndTime(endTime);
            Debug.Log("1234"+endTime);
        }

        public float GetEndTime()
        {
            return endTime;
        }

        public int GetMidiID()
        {
            return midiID;
        }
    }
}
