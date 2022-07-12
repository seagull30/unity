using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerinput : MonoBehaviour
{
    public float X { get; private set; }
    public float Y { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        X = Input.GetAxis("Horizontal");
        Y = Input.GetAxis("Vertical");
    }
}
