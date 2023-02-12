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

        // Start is called before the first frame update
        void Start()
        {
            instance = MusicInfo.GetInstance();
            perminute = instance.GetPerminute();
            interval = 60 / perminute;
        }

        // Update is called once per frame
        void Update()
        {
            if (timer1.GetStartBool())
            {

            }
        }
    }
}