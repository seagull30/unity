using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCtrl : MonoBehaviour
{
    public event UnityAction<Vector3,Vector3> PlayerAttack;

    public void asd(Vector3 a, Vector3 b)
    {
        PlayerAttack.Invoke(a, b);
    }
}
