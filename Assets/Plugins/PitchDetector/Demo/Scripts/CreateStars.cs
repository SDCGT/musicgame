using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
//using symbol;
//using util;

namespace xmlParser
{
    public class CreateStars : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject star;

        public Transform planet;
        float midiID;
       
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))  //按下空格键后
            {
                Instantiate(star, new Vector3(15 + midiID, -16, 0), new Quaternion(0, 0, 0, 0), planet.transform);
            }
        }

        void BornStar()
        {

        }
    }
}

