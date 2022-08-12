using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    private EnemyMovement _enemyMovement;
    private int _layer;

    private void Awake()
    {
        _enemyMovement = GetComponentInParent<EnemyMovement>();
        _layer = LayerMask.NameToLayer("Enemy");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer ==_layer)
        {
            EnemyHealth enemyHealth=other.GetComponent<EnemyHealth>();
            Debug.Assert(enemyHealth!=null);
            enemyHealth.OnTakenDamage -= _enemyMovement.MoveTo;
            enemyHealth.OnTakenDamage += _enemyMovement.MoveTo;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _layer)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            Debug.Assert(enemyHealth != null);
            enemyHealth.OnTakenDamage -= _enemyMovement.MoveTo;
        }
    }
}
