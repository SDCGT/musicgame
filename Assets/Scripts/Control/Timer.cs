using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace xmlParser
{
    public class Timer : MonoBehaviour
    {

        float playTime = 0;//视唱已持续时间
        float endTime = 0;//视唱结束时间
        bool start = false;

        void Start()
        {
            endTime = StaticMusicInfo.GetEndTime();
            Debug.Log("endTime"+endTime);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))  //按下空格键后开始
            {
                start = true;

            }

            if (start && playTime < endTime)//曲目时间内，计时器运作
            {
                playTime += Time.deltaTime;
                //Debug.Log("playtime" + playTime);
            }

            if (start && playTime >= endTime)
            {
                //SendMessage("EndGame");
            }
        }
        public float GetGameTime()
        {
            return playTime;
        }
    }
}
