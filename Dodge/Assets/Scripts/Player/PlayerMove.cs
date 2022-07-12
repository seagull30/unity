using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody _rigidbody;
    public float _speed = 3.0f;
    Playerinput _input;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _input = GetComponent<Playerinput>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity = new Vector3(_input.X * _speed, 0.0f, _input.Y * _speed);
    }
}
