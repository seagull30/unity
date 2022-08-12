using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float MoveSpeed = 8f;
    private Transform _target;

    public void MoveTo(Transform target)
    {
        if (_target != null)
        {
            return;
        }
        _target = target;

        StartCoroutine(moveToHelper());
    }

    private IEnumerator moveToHelper()
    {
        Debug.Assert(_target != null);
        while (true)
        {
                transform.LookAt(_target);
            transform.Translate(0f, 0f, MoveSpeed * Time.deltaTime, Space.Self);
            // target하고 가까워 질때까지

            if (Vector3.Distance(transform.position, _target.position) <= 1f)
            {
                break;
            }
            yield return null;

        }
    }

    private bool isOnFront()
    {
        Vector3 distanceVector = _target.position - transform.position;
        if (Vector3.Angle(distanceVector, transform.forward) <= 5f)
        {
            return true;
        }
        else
            return false;
    }

}
