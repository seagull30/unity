using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int HP = 100;

    public bool IsDead { get; private set; }

    public event Action<Transform> OnTakenDamage;

    public void TakeDamage(int damage)
    {
        HP -= damage;
        
        OnTakenDamage?.Invoke(transform);

        Debug.Log($"{gameObject} : damaged [{damage}]");

        if (HP <= 0)
        {   
            IsDead = true;
            gameObject.SetActive(false);
            Debug.Log($"{gameObject} : Á×À½");
        }
    }


}
