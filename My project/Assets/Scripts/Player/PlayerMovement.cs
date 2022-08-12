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
    /// 캐릭터를 앞 뒤로 움직인다.
    /// </summary>
    /// <param name="dir">양수면 캐릭터의 foward 방향을 의미하며, 음수면 캐릭터의 backward 방향을 의미한다.</param>
    private void moveForward(float dir)
    {
        Vector3 deltaPosition = MoveSpeed * dir * Time.fixedDeltaTime * transform.forward;
        Vector3 newPosition = _rigidbody.position + deltaPosition;
        _rigidbody.MovePosition(newPosition);
    }

    /// <summary>
    /// 캐릭터를 시계방향으로 회전시킨다.
    /// </summary>
    /// <param name="dir">양수면 시계방향을, 음수면 반시계방향을 의미한다.</param>
    private void rotateClockwise(float dir)
    {
        Quaternion deltaRotation = Quaternion.Euler(0f, RotationSpeed*dir*Time.fixedDeltaTime, 0f);
        Quaternion newRotation = _rigidbody.rotation * deltaRotation;
        _rigidbody.MoveRotation(newRotation);
    }

}
