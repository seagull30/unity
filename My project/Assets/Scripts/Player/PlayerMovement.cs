using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Playerinput _input;
    private Rigidbody _rigidbody;

    public float MoveSpeed = 10f;
    public float RotationSpeed = 120f;

    private void Awake()
    {
        _input = GetComponent<Playerinput>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        moveForward(_input.Y);
        rotateClockwise(_input.X);
    }

    /// <summary>
    /// ĳ���͸� �� �ڷ� �����δ�.
    /// </summary>
    /// <param name="dir">����� ĳ������ foward ������ �ǹ��ϸ�, ������ ĳ������ backward ������ �ǹ��Ѵ�.</param>
    private void moveForward(float dir)
    {
        Vector3 deltaPosition = MoveSpeed * dir * Time.fixedDeltaTime * transform.forward;
        Vector3 newPosition = _rigidbody.position + deltaPosition;
        _rigidbody.MovePosition(newPosition);
    }

    /// <summary>
    /// ĳ���͸� �ð�������� ȸ����Ų��.
    /// </summary>
    /// <param name="dir">����� �ð������, ������ �ݽð������ �ǹ��Ѵ�.</param>
    private void rotateClockwise(float dir)
    {
        Quaternion deltaRotation = Quaternion.Euler(0f, RotationSpeed*dir*Time.fixedDeltaTime, 0f);
        Quaternion newRotation = _rigidbody.rotation * deltaRotation;
        _rigidbody.MoveRotation(newRotation);
    }

}
