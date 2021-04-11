using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorController : MonoBehaviour
{
    public bool DetectHero = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hero")
        {
            DetectHero = true;
        }
    }

}
