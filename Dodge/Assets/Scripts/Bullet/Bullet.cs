using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0.03f;
    private void Start()
    {
        Destroy(gameObject, 3.0f);
    }
    private void Update()
    {
        transform.Translate(0f, 0f, speed);
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    //?. ������
    //    // (expression)?.~ : expression�� null�� �ƴϸ� ����� ������.
    //    other.GetComponent<Playerhealth>()?.Die();
    //}
}
