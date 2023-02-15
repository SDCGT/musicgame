using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xmlParser
{
    public class PlanetRotate : MonoBehaviour
    {
        // Start is called before the first frame update
        public Transform planet;
        float rotateSpeed;
        float perminute;
        public float magnitudeofBPM=2.0f;//预备播放几个小节的节拍
        float beat;//一小节几拍
        MusicInfo instance;
        void Start()
        {
            instance = MusicInfo.GetInstance();
            perminute = instance.GetPerminute();
            //perminute = 60;
            beat = instance.GetBeat();
            float prepeartime = instance.GetPrepearTime();
            //prepeartime = Mathf.InverseLerp(0, perminute, magnitudeofBPM * beat);
            //Debug.Log(" magnitudeofBPM" + magnitudeofBPM);
            //Debug.Log("BPM" + perminute);
            //Debug.Log("beat" + beat);
            //Debug.Log("prepeartime" + prepeartime);
            rotateSpeed =  180 /(2*prepeartime);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //planet.Rotate(new Vector3(0, 0, 1), rotateSpeed);
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        }
    }
}
