using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class PlayerCtrl : MonoBehaviour, ITargetable
{
    public void Damaged()
    {
        Debug.Log("공격을 받았다");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
