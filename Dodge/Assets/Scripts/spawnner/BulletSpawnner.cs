using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnner : MonoBehaviour
{
    float _deltatime = 0.0f;
    float spawnTime = 0.0f;
    public GameObject bulletPrefab;
    public Transform player;    
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Random.Range(1.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        _deltatime += Time.deltaTime;
        if (_deltatime >= spawnTime)
        {
            _deltatime = 0.0f;
            spawnTime = Random.Range(1.0f, 5.0f);
            //_deltatime = Random.RandomRange(1.0f,3.0f);
            Vector3 spawnPosition = transform.position + new Vector3(0f, 0f, 2f);
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.transform.LookAt(player);
        }

    }
}
