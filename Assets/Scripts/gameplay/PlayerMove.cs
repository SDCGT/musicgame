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

        [SerializeField] float tickInterval = 0.01f;
        // Start is called before the first frame update
        public Transform player;
        public SpriteRenderer playerMesh;
        float pitch = 0;
        public TMP_Text score;
        private List<double> singPitchList = new List<double>();
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            pitch = detector.MidiNote;
            Debug.Log("singFrequent" + detector.Pitch);
            singPitchList.Add(detector.Pitch);
            player.transform.position = new Vector3(0, pitch*0.3f-15f, 0);
            if(pitch<40)//可视性调整
            {
                playerMesh.color = new Color(0, 0, 0, 0);
            }
            else
            {
                playerMesh.color = new Color(255, 255, 255);
            }
            PitchRecord.SetSingPitchList(singPitchList);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "star")
            {
                Debug.Log("addScore");
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
