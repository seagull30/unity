using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnner : MonoBehaviour
{
    float _deltatime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Time.timeScale = 0.1f;
        }
        else
            Time.timeScale = 1f;
        _deltatime += Time.deltaTime;
        if (_deltatime >= 0.5)
        {
            _deltatime = 0;
            Debug.Log("log");
        }

    }
}
