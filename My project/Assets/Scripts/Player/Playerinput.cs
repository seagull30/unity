using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerinput : MonoBehaviour
{
    public float X { get; private set; }
    public float Y { get; private set; }
    public bool Attack { get; private set; }

    void FixedUpdate()
    {
        X = Input.GetAxis("Horizontal");
        Y = Input.GetAxis("Vertical");
        Attack = Input.GetKeyDown(KeyCode.Space);
    }
}
