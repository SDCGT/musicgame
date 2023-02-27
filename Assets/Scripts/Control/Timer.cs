using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Timer : MonoBehaviour
    {
        public GameObject upLoadData1;
        float playTime;//视唱已持续时间,有偏差，增加初始值
        float endTime;//视唱结束时间
        bool start;
        bool end;
        public delegate void EndGame();

        public static event EndGame EndUI;
        void Start()
        {
            playTime = 0;
            endTime = 0;
            start = false;
            bool end = false;
            endTime = StaticMusicInfo.GetEndTime();
            PitchRecord.ClearData();
            //Debug.Log(PitchRecord.GetScoreList().Count);
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Debug.Log("playtime"+playTime);
            if (Input.GetKeyDown(KeyCode.Space))  //按下空格键后开始
            {
                start = true;
            }

            if (start && playTime < ((endTime)+StaticMusicInfo.GetPrepearTime()))//曲目时间内，计时器运作
            {
                playTime += Time.deltaTime;
                //Debug.Log("playtime" + playTime);
            }

            if (start && playTime >= ((endTime) + StaticMusicInfo.GetPrepearTime())&&!end)
            {
                TriggerEnd();
                upLoadData1.SendMessage("UpLoad");
                end = true;
            }

    }
        public float GetGameTime()
        {
            return playTime;
        }

        public bool GetStartBool()
        {
            return start;
        }

        public static void TriggerEnd()
        {

        if (EndUI != null)
        {
            Debug.Log("Trigger");
            EndUI();
        }
        }
    }
