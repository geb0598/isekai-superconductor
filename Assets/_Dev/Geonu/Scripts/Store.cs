using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public StoreItem[] storeItems;
    public Sprite[] sprites; // index = weapon id

    private int _disabledIndex;

    Rigidbody2D _playerRigidbody2D;

    private void Awake()
    {
        _playerRigidbody2D = GameManager.GetInstance().playerController.GetComponent<Rigidbody2D>();
        _disabledIndex = storeItems.Length - 1;
    }

    // position initialize
    private void OnEnable()
    {
        for (int i = 0; i < storeItems.Length; i++)
        {
            Vector2 directionVector = new Vector2(5 * Mathf.Sin(2 * Mathf.PI * i / storeItems.Length), 5 * Mathf.Cos(2 * Mathf.PI * i / storeItems.Length));
            storeItems[i].GetComponent<Rigidbody2D>().position = _playerRigidbody2D.position + directionVector;
        }
    }

    public void CreateNewStoreItem(int index)
    {
        // max level check
        int newWeaponId = Random.Range(0, 4);

        storeItems[_disabledIndex].Init(newWeaponId, sprites[newWeaponId]);
        storeItems[_disabledIndex].gameObject.SetActive(true);
        _disabledIndex = index;
    }

    public void EnableStore()
    {
        gameObject.SetActive(true);
    }

    public void DisableStore()
    {
        gameObject.SetActive(false);
    }
}
