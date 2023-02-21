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

        int midiID;
        double frequent = 0;
        string ScoreName = CommonParams.GetInstance().GetScoreName();
        XmlParser parser;
        private List<Symbol> SymbolMeasure = new List<Symbol>();
        private List<Note> NoteList = new List<Note>();
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
            instance.allduration = 0;
            instance.midiID = -1;
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
            instance.prepeartime = (instance.magnitudeofBPM * instance.beatint * 1.0f) / (instance.perminute * 1.0f / 60.0f) + 0.15f;//0.15f为位置偏移补偿;
            StaticMusicInfo.SetPrepearTime(instance.prepeartime);
        }
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            instance.frequent = 440.0f * Mathf.Pow(2.0f, (instance.midiID*1.0f - 69.0f) / 12.0f);
            //Debug.Log(instance.frequent);
            if (0<time.GetGameTime() && time.GetGameTime()< instance.endTime)//曲目时间内，获取曲目当前音高
            {
                instance.midiID = GetMIDIID(time.GetGameTime());
                //PitchRecord.SetScoreDictionary(time.GetGameTime()+instance.prepeartime, instance.frequent);
                RecordData data = new RecordData();
                data.time = time.GetGameTime() + instance.prepeartime;
                data.Frequent = instance.midiID;
                PitchRecord.SetScoreList(data);//后台记录曲谱的音高
                StaticMusicInfo.SetScoreName(instance.midiID);
            }

            if (time.GetGameTime() >= instance.endTime)
            {
                instance.midiID = -1;
            }
            
            if(time.GetGameTime()>=(instance.endTime+instance.prepeartime))
            {
            }
        }

        void CountAllDuration()//计算duration总和
        {

            for (int i = 0; i < instance.SymbolMeasure.Count; i++)
            {
                instance.allduration = instance.allduration + instance.SymbolMeasure[i].GetDuration();
            }
            //Debug.Log(instance.SymbolMeasure.Count);
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
                    //Debug.Log("durationPercent" + instance.allduration);
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
                                  //Debug.Log("in GetDigitizedPitch:" + instance.midiID);
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

        private int GetDigitizedPitch(string step, string octave)//换算高度
        {
            int digitizedPitch = 1;

            switch (step)
            {
                case "C": digitizedPitch = 0; break;
                case "D": digitizedPitch = 2; break;
                case "E": digitizedPitch = 4; break;
                case "F": digitizedPitch = 5; break;
                case "G": digitizedPitch = 7; break;
                case "A": digitizedPitch = 9; break;
                case "B": digitizedPitch = 11; break;
            }
            //return digitizedPitch + (int.Parse(octave) - 1) * 7;
            return digitizedPitch + (int.Parse(octave) + 1) * 12;
        }

        void TotalTime()//计算曲目总时长
        {
            float BeatsPercent = instance.allBeats * 1.0f / (1.0f * instance.perminute);
            instance.endTime = BeatsPercent * 60.0f;
            StaticMusicInfo.SetEndTime(instance.endTime);
            Debug.Log("endTime" + instance.endTime);
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
