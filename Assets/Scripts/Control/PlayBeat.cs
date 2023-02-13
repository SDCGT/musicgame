using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace xmlParser
{
    public class PlayBeat : MonoBehaviour
    {
        public Timer timer1;
        int perminute;
        MusicInfo instance;
        float interval;
        float time=0;
        public AudioSource metronomeSound;
        bool playbeat;

        private void Awake()
        {
         
        }
        // Start is called before the first frame update
        void Start()
        {
            instance = MusicInfo.GetInstance();
            //perminute = instance.GetPerminute();
            perminute =60;
            Debug.Log("inter" + perminute);
            interval = 60 / perminute;
            
        }

        private void Update()
        {
            if(timer1.GetGameTime()>0.8f&&!playbeat)
            {
                InvokeRepeating("PlayMetronome", 0, interval);
                playbeat = true;
            }
        }

        // Update is called once per frame
        void PlayMetronome()
        {
            //Debug.Log("play");
            metronomeSound.Play();
        }

    }
}