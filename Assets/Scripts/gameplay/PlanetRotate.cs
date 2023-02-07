using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotate : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform planet;
    public float rotateSpeed = 0.3f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        planet.Rotate(new Vector3(0,0,1),rotateSpeed);
    }
}
