using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Timer : MonoBehaviour
    {
        public GameObject upLoadData1;
        float playTime = 0;//�ӳ��ѳ���ʱ��,��ƫ����ӳ�ʼֵ
        float endTime = 0;//�ӳ�����ʱ��
        bool start = false;
        bool end = false;
        void Start()
        {
            endTime = StaticMusicInfo.GetEndTime();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
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


    }
