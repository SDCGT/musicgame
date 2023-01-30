using System.Collections;
using System.Collections.Generic;
using Pitch;
using UnityEngine;


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
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            pitch = detector.MidiNote;
            player.transform.position = new Vector3(0, pitch*0.35f-20f, 0);
            if(pitch<40)
            {
                playerMesh.color = new Color(0, 0, 0, 0);
            }
            else
            {
                playerMesh.color = new Color(255, 255, 255);
            }
        }
    }
}
