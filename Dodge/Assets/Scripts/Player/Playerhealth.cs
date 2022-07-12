using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerhealth : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
            Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
