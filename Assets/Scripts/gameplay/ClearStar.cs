using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearStar : MonoBehaviour
{
    // Start is called before the first frame update
    //public Collider2D collider;
    public GameObject star;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if( collision.tag=="clearStar")
       {
            //Debug.Log("!!111");
            Destroy(star);
        }
    }
}
