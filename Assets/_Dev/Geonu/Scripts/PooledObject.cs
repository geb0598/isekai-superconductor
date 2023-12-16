using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public int type;
    public int id;

    public GameObject GetPooledObject()
    {
        return GameManager.GetInstance().poolManager.Get(type, id);
    }

    public void ReturnPooledObject()
    {
        gameObject.SetActive(false);
    }
}
