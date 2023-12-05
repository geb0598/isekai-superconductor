using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject[] storeItems;

    Rigidbody2D _playerRigidbody2D;

    private void Awake()
    {
        _playerRigidbody2D = GameManager.GetInstance().playerController.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < storeItems.Length; i++)
        {
            Vector2 directionVector = new Vector3((storeItems.Length - 1) * (-2f) + i * 4f, 2f);
            storeItems[i].GetComponent<Rigidbody2D>().position = _playerRigidbody2D.position + directionVector;
        }
    }
}
