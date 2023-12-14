using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EnemyIndices
{
    Alien, // from here, ranged enemy
    Blien,
    Clien,
    Dlien,
    Elien,
    Flien,
    Glien = 10, // from here, melee enemy
    Hlien,
    Ilien,
    Jlien,
    Klien,
    Llien
}

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class GameObjectList
    {
        public List<GameObject> prefabs = new List<GameObject>();
    }

    [SerializeField]
    private List<GameObjectList> prefabs = new List<GameObjectList>();
    private List<GameObject>[][] _pools;

    private void Awake()
    {
        _pools = new List<GameObject>[prefabs.Count][];

        for (int i = 0; i < prefabs.Count; i++)
        {
            _pools[i] = new List<GameObject>[prefabs[i].prefabs.Count];

            for (int j = 0; j < prefabs[i].prefabs.Count; j++)
            {
                _pools[i][j] = new List<GameObject>();
            }
        }
    }

    public GameObject Get(int type,int index) // type: ragnedEnemy = 0, meleeEnemy = 1 , others = 2
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
            if (index >= prefabs[type].prefabs.Count)
            {
                Debug.LogWarning("Error");
            }

            select = Instantiate(prefabs[type].prefabs[index], transform);

            _pools[type][index].Add(select);
        }

        return select;
    }
}
