using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Timer : MonoBehaviour
    {

        float playTime = 0.5f;//�ӳ��ѳ���ʱ��,��ƫ����ӳ�ʼֵ
        float endTime = 0;//�ӳ�����ʱ��
        bool start = false;

        void Start()
        {
            endTime = StaticMusicInfo.GetEndTime()+0.5f;//��ƫ����ӳ�ʼֵ;
            Debug.Log("endTime"+endTime);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))  //���¿ո����ʼ
            {
                start = true;

            }

            if (start && playTime < (endTime+2))//��Ŀʱ���ڣ���ʱ������
            {
                playTime += Time.deltaTime;
                //Debug.Log("playtime" + playTime);
            }

            if (start && playTime >= (endTime+2))
            {
                //SendMessage("EndGame");
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
