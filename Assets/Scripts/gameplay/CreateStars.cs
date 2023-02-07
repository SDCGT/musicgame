using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using symbol;
using util;

namespace xmlParser
{
    public class CreateStars : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject star;

        public Transform planet;
        int midiID=0;
        XmlParser parser = new XmlParser("Assets/Materials/MusicXml/2.xml");
        private List<Symbol> SymbolMeasure = new List<Symbol>();
        private List<Note> NoteList = new List<Note>();

        private Beat beat;
        int beattypeint;
        int beatint;
        int allduration;
        int perminute;
        int allBeats;
        int measureCount;
        bool start = false;
        float time;
        int pivot = 0;
        void Start()
        {
            NoteList = parser.GetNoteList();
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
            Debug.Log(perminute);
            SetStartTime();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))  //按下空格键后
            {
                start = true;
                // Instantiate(star, new Vector3(15 + midiID, -16, 0), new Quaternion(0, 0, 0, 0), planet.transform);
            }

            if (start)
            {
                time += Time.deltaTime;
                if ((time * 10 % 5 < 0.5))
                {
                    midiID = GetMIDIID(time);
                    Debug.Log("in start:" + midiID);
                    if(midiID != 0)
                    {
                        Debug.Log("in midiID:" + midiID);
                        GameObject star1 = Instantiate(star, new Vector3(12 + midiID*0.3f, -16, 0), new Quaternion(0, 0, 0, 0), planet.transform);
                        //star1.SetActive(true);
                       
                        Debug.Log("out midiID:" + midiID);
                    }
 
                }
            }
        }

        void BornStar()
        {

        }

        void CountAllDuration()
        {
            
            for(int i=0;i< SymbolMeasure.Count;i++)
            {
                allduration = allduration + SymbolMeasure[i].GetDuration();
            }
        }

        void SetStartTime()
        {
            float starttime = 0;
            for (int i = 0; i < SymbolMeasure.Count; i++)
            {
                if(allduration!=0)
                {
                    int duration = SymbolMeasure[i].GetDuration();
                    Debug.Log(perminute);
                    float durationPercent = duration*1.0f/(1.0f* allduration);
                    float BeatsPercent = allBeats * 1.0f / (1.0f * perminute);
                    float durationTime = durationPercent * BeatsPercent * 60;
                    starttime = starttime + durationTime;
                    SymbolMeasure[i].SetStartTime(starttime);
                    //Debug.Log(SymbolMeasure[i].GetStartTime());
                }
            }
        }
        int GetMIDIID(float time)
        {
            int MidiID=0;
            for (int i = 0; i < SymbolMeasure.Count; i++)
            {
                if((time-SymbolMeasure[i].GetStartTime())<0.1f&& (time - SymbolMeasure[i].GetStartTime())>0)
                {
                    MidiID = GetDigitizedPitch(NoteList[i].GetStep(), NoteList[i].GetOctave());
                    Debug.Log("in GetDigitizedPitch:" + MidiID);
                }
            }
            return MidiID;
        }

        private int GetDigitizedPitch(string step, string octave)
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

    }
}

