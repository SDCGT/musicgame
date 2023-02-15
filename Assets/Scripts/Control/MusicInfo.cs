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
        private static MusicInfo instance = new MusicInfo();

        int midiID = -1;
        double frequent = 0;
        string ScoreName = CommonParams.GetInstance().GetScoreName();
        XmlParser parser;
        private List<Symbol> SymbolMeasure = new List<Symbol>();
        private List<Note> NoteList = new List<Note>();
        private List<double> ScorePitchList = new List<double>();
        public Dictionary<float, double> ScoreDictionary = new Dictionary<float, double>();

        private Beat beat;
        float endTime;
        int beattypeint;
        int beatint;
        int allduration;
        int perminute;
        int allBeats;
        int measureCount;
        public Timer time;
        public float magnitudeofBPM = 2.0f;//预备播放几个小节的节拍
        float prepeartime;//预备节拍时间

        public static MusicInfo GetInstance() { return instance; }

        // Start is called before the first frame update

        private void Awake()
        {
            XmlParser parser = new XmlParser(instance.ScoreName);
            string perminutestr = parser.GetPerMinute();//获取BPM
            int.TryParse(perminutestr, out instance.perminute);
            instance.measureCount = parser.GetMeasureList().Count;
            instance.SymbolMeasure = parser.GetHighSymbolList();
            instance.beat = parser.GetBeat();

            CountAllDuration();

            int.TryParse(instance.beat.GetBeats(), out instance.beatint);
            int.TryParse(instance.beat.GetBeatType(), out instance.beattypeint);
            instance.allBeats = instance.beattypeint * instance.measureCount;
            TotalTime();
            SetStartTime();
            SetStopTime();
            instance.prepeartime = (instance.magnitudeofBPM * instance.beatint * 1.0f) / (instance.perminute * 1.0f / 60.0f);
            Debug.Log("prepeartime" + instance.prepeartime);
        }
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            instance.frequent = 440 * Mathf.Pow(2, (instance.midiID - 69) / 12);
            //Debug.Log(midiID + "scoreFrequent" + frequent);
            instance.ScorePitchList.Add(frequent);
            PitchRecord.SetScorePitchList(instance.ScorePitchList);

            //Debug.Log(time.GetGameTime());
            if (0<time.GetGameTime() && time.GetGameTime()< instance.endTime)//曲目时间内，获取曲目当前音高
            {
                instance.midiID = GetMIDIID(time.GetGameTime());
                Debug.Log("MIDIID" + instance.midiID);
                ScoreDictionary.Add(time.GetGameTime()+prepeartime, frequent);//后台记录曲谱的音高
            }

            if (time.GetGameTime() >= instance.endTime)
            {
                instance.midiID = -1;
            }
        }

        void CountAllDuration()//计算duration总和
        {

            for (int i = 0; i < instance.SymbolMeasure.Count; i++)
            {
                instance.allduration = instance.allduration + instance.SymbolMeasure[i].GetDuration();
            }
        }

        void SetStartTime()//设置每个Symbol的开始时间
        {
            float starttime = 0;
            instance.SymbolMeasure[0].SetStartTime(starttime);
            for (int i = 1; i < instance.SymbolMeasure.Count; i++)
            {
                if (instance.allduration != 0)
                {
                    int duration = instance.SymbolMeasure[i-1].GetDuration();
                    //Debug.Log(perminute);
                    float durationPercent = duration * 1.0f / (1.0f * instance.allduration);
                    float BeatsPercent = instance.allBeats * 1.0f / (1.0f * instance.perminute);
                    float durationTime = durationPercent * BeatsPercent * 60;
                    starttime = starttime + durationTime;
                    instance.SymbolMeasure[i].SetStartTime(starttime);
                    //Debug.Log("i" + i + "duration" + duration);
                    //Debug.Log("i" + i + "starttime" + starttime);
                    //Debug.Log(SymbolMeasure[i].GetStartTime());
                }
            }
        }

        void SetStopTime()//设置每个Symbol的结束时间
        {
            float stoptime = 0;
            for (int i = 0; i < instance.SymbolMeasure.Count; i++)
            {
                if (instance.allduration != 0)
                {
                    int duration = instance.SymbolMeasure[i].GetDuration();
                    //Debug.Log(perminute);
                    float durationPercent = duration * 1.0f / (1.0f * instance.allduration);
                    float BeatsPercent = instance.allBeats * 1.0f / (1.0f * instance.perminute);
                    float durationTime = durationPercent * BeatsPercent * 60;
                    stoptime = stoptime + durationTime;
                    instance.SymbolMeasure[i].SetStopTime(stoptime);
                    //Debug.Log("i" + i + "duration" + duration);
                    //Debug.Log("i" + i + "steptime" + stoptime);
                    //Debug.Log(SymbolMeasure[i].GetStartTime());
                }
            }
        }

        public int GetMIDIID(float time)//按时间节点获取音高
        {
                  for (int i = 0; i < (instance.SymbolMeasure.Count); i++)
                  {
                      bool isNote = instance.SymbolMeasure[i] is Note;
                      if (time < instance.endTime)
                      {

                          if (time > instance.SymbolMeasure[i].GetStartTime() && time < instance.SymbolMeasure[i].GetStopTime())
                          {
                              if (isNote)
                              {
                                  instance.midiID = GetDigitizedPitch(((Note)instance.SymbolMeasure[i]).GetStep(), ((Note)instance.SymbolMeasure[i]).GetOctave());
                                  //Debug.Log("in GetDigitizedPitch:" + MidiID);
                              }
                              else
                              {
                                  instance.midiID = 0;//表示休止
                              }
                          }
                      }

                      else
                      {
                          instance.midiID = -1;
                      }
                  }
            //int MidiID = 0; = instance.midiID;
           
           return instance.midiID;
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
            float BeatsPercent = instance.allBeats * 1.0f / (1.0f * instance.perminute);
            instance.endTime = BeatsPercent * 60.0f;
            StaticMusicInfo.SetEndTime(instance.endTime);
            //Debug.Log("BeatsPercent" + instance.perminute);
        }

        public float GetEndTime()
        {
            return instance.endTime;
        }

        public int GetMidiID()
        {
            return instance.midiID;
        }

        public int GetPerminute()
        {
            return instance.perminute;
        }

        public int GetBeat()
        {
            return instance.beatint;
        }

        public float GetPrepearTime()
        {
            return instance.prepeartime;
        }
    }
}
