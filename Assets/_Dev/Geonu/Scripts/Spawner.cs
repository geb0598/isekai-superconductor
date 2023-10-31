using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Transform[] _spawnPoints;

    private float _timer;
    private float _spawnTime;

    private void Awake()
    {
        _spawnPoints = GetComponentsInChildren<Transform>();
        _spawnTime = 1f;
    }


    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnTime)
        {
            Spawn();
            _timer = 0f;
        }
    }

    private void Spawn()
    {
        int type = Random.Range(0, 2);
        int index = Random.Range(0, 6);

        GameObject enemy = GameManager.GetInstance().poolManager.Get(type, index);

        enemy.transform.position = _spawnPoints[Random.Range(1, _spawnPoints.Length)].position;
        enemy.GetComponent<Enemy>().Init();
    }
}
