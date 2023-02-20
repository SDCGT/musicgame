using System.Collections;
using System.Collections.Generic;
using Pitch;
using UnityEngine;
using TMPro;

namespace FinerGames.PitchDetector.Demo
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] PitchDetector detector;

        //[SerializeField] float tickInterval = 0.01f;
        // Start is called before the first frame update
        public Transform player;
        public SpriteRenderer playerMesh;
        float pitch = 0;
        public TMP_Text score;
        public Timer time;

        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Debug.Log(pitch);
            pitch = detector.Pitch;
            pitch = pitch * Mathf.Pow(2.0f, (1.38f / 12.0f));//后台记录的音高
            player.transform.position = new Vector3(0, (detector.MidiNote+detector.MidiCents*0.01f)*0.31f-19f, 0);
            if(pitch<40)//可视性调整
            {
                playerMesh.color = new Color(0, 0, 0, 0);
            }
            else
            {
                playerMesh.color = new Color(255, 255, 255);
            }
            if(time.GetGameTime()> StaticMusicInfo.GetPrepearTime() && time.GetGameTime()<(StaticMusicInfo.GetEndTime()+StaticMusicInfo.GetPrepearTime()))
            {
                //Debug.Log("startRecord");
                //PitchRecord.SetSingDictionary(time.GetGameTime(), detector.Pitch);
                RecordData data1=new RecordData();
                data1.time = time.GetGameTime();
                data1.Frequent = detector.MidiNote + detector.MidiCents * 0.01f+1.38f;
                PitchRecord.SetSingList(data1);//记录唱歌的音高
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "star")
            {
                //Debug.Log("addScore");
                string scorestr;
                int scoreint;
                scorestr = score.text.ToString();
                int.TryParse(scorestr, out scoreint);
                scoreint +=1;
                score.text = scoreint.ToString();
                Destroy(collision.gameObject);
            }
        }
    }
}
