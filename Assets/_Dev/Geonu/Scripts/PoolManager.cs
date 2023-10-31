using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _meleeEnemyPrefabs;
    [SerializeField]
    private GameObject[] _rangedEnemyPrefabs;
    [SerializeField]
    private GameObject[] _otherPrefabs;

    private List<GameObject>[][] _pools;

    private void Awake()
    {
        _pools = new List<GameObject>[3][];

        _pools[0] = new List<GameObject>[_meleeEnemyPrefabs.Length];
        _pools[1] = new List<GameObject>[_rangedEnemyPrefabs.Length];
        _pools[2] = new List<GameObject>[_otherPrefabs.Length];

        for (int i = 0; i < _pools[0].Length; i++)
        {
            _pools[0][i] = new List<GameObject>();
        }

        for (int i = 0; i < _pools[1].Length; i++)
        {
            _pools[1][i] = new List<GameObject>();
        }

        for (int i = 0; i < _pools[2].Length; i++)
        {
            _pools[2][i] = new List<GameObject>();
        }
    }

    public GameObject Get(int type,int index) // type: 0 = meleeEnemy, 1 = rangeEnemy, 2 = others
    {
        if (index >= _pools[type].Length)
        {
            Debug.LogWarning("pools[type]: Out of Index");
            return null;
        }

        GameObject select = null;

        // check whether there already exists inactive GameObject
        foreach (GameObject obj in _pools[type][index])
        {
            if (!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                break;
            }
        }

        // if no, instanitate new GameObject and add it to _pools
        if (!select)
        {
            switch (type)
            {
                case 0:
                    select = Instantiate(_meleeEnemyPrefabs[index], transform);
                    break;
                case 1:
                    select = Instantiate(_rangedEnemyPrefabs[index], transform);
                    break;
                case 2:
                    select = Instantiate(_otherPrefabs[index], transform);
                    break;
                default:
                    select = null;
                    Debug.LogWarning("Error");
                    break;
            }

            _pools[type][index].Add(select);
        }

        return select;
    }
}
