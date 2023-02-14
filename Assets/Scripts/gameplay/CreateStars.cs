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
        public MusicInfo musicinfo1;
        int pivot = 0;

        public float center = 12f;
        public float offset = 0.3f;
        public float interval=0.3f;
        int midiID=-1;
        public Timer time;

        void Start()
        {
            musicinfo1 = MusicInfo.GetInstance();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            midiID = musicinfo1.GetMidiID();
            if(midiID!=-1)
            {
                if(time.GetGameTime()<0.03f)
                {
                   // Invoke("PlayMetronome", 0.03f);
                    //Debug.Log("playfirsttime");
                }
                if(time.GetGameTime()*10%3<0.3f)
                {
                    BornStar();
                }
            }
        }

        void BornStar()
        {
            GameObject star1 = Instantiate(star, new Vector3(center + offset * midiID, -16, 0), new Quaternion(0, 0, 0, 0), planet.transform);
            Debug.Log("bornstar"+midiID);
        }
    }
}

