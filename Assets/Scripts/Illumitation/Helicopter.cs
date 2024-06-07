using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public Transform helice;
    public float angle;
 
    void Update()
    {
        helice.Rotate(0.0f, 0.0f, angle);
        angle += 0.1f;
    }
}
