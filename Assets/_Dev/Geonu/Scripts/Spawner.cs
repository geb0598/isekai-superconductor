using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnPatterns[] spawnPatterns;

    public float[] _spawnTimes;

    private Transform[] _spawnPoints; // index starts from 1

    private float _timer;
    private float _spawnTime;

    private int _spawnPatternIndex; // loop

    private void Awake()
    {
        _spawnPoints = GetComponentsInChildren<Transform>();
        _spawnPatternIndex = 0;
    }


    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnTimes[GameManager.GetInstance().subWave])
        {
            Spawn();
            _timer = 0f;
        }
    }

    private void Spawn()
    {
        _spawnPatternIndex = (_spawnPatternIndex + 1) % spawnPatterns[GameManager.GetInstance().subWave].spawnPatterns.Length;
        spawnPatterns[GameManager.GetInstance().subWave].spawnPatterns[_spawnPatternIndex].PatternSpawn(_spawnPoints);
    }

    public void OnSubWaveChanged()
    {
        _spawnPatternIndex = 0;
    }
    public void EnableSpawner()
    {
        gameObject.SetActive(true);
    }

        public void DisableSpawner()
    {
        gameObject.SetActive(false);
    }
}

[System.Serializable]
public class SpawnPattern
{
    public EnemyGroup enemyGroup;
    public SpawnPoints spawnPoints;

    public void PatternSpawn(Transform[] _spawnPoints)
    {
        for (int i = 0; i < spawnPoints.spawnPointsIndices.Length; i++)
        {
            for (int j = 0; j < spawnPoints.spawnCounts[i] * GameManager.GetInstance().wave; j++)
            {
                int index = SelectIndexWithProbabilities(enemyGroup.probabilities);
                Enemy enemy = enemyGroup.enemyPrefabs[index].GetPooledObject().GetComponent<Enemy>();

                enemy.transform.position = _spawnPoints[spawnPoints.spawnPointsIndices[i]].position;
                enemy.Init();
            }
        }
    }

    public int SelectIndexWithProbabilities(float[] probabilities)
    {
        float x = Random.Range(0f, 1f);
        float sum = 0f;
        int index = 0;

        for (; index < probabilities.Length - 1; index++)
        {
            sum += probabilities[index];

            if (x <= sum)
            {
                break;
            }
        }

        return index;
    }
}

[System.Serializable]
public class SpawnPatterns
{
    public SpawnPattern[] spawnPatterns;
}
