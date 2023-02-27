using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Timer : MonoBehaviour
    {
        public GameObject upLoadData1;
        float playTime;//�ӳ��ѳ���ʱ��,��ƫ����ӳ�ʼֵ
        float endTime;//�ӳ�����ʱ��
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
            if (Input.GetKeyDown(KeyCode.Space))  //���¿ո����ʼ
            {
                start = true;
            }

            if (start && playTime < ((endTime)+StaticMusicInfo.GetPrepearTime()))//��Ŀʱ���ڣ���ʱ������
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
