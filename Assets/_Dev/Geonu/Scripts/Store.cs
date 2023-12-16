using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject[] storeItemWeaponPrefabs;
    public GameObject[] storeItemActiveWeaponPrefabs;

    private GameObject[] _storeItems;
    private GameObject[] _storeItemTransforms;
    private int _disabledIndex;

    Rigidbody2D _playerRigidbody2D;

    private void Awake()
    {
        storeItemWeaponPrefabs = storeItemWeaponPrefabs.OrderBy(w => w.GetComponent<StoreItem>().id).ToArray();
        storeItemActiveWeaponPrefabs = storeItemActiveWeaponPrefabs.OrderBy(w => w.GetComponent<StoreItem>().id).ToArray();

        _playerRigidbody2D = GameManager.GetInstance().playerController.GetComponent<Rigidbody2D>();
        _storeItems = new GameObject[6];
        _storeItemTransforms = new GameObject[6];
    }

    // position initialize
    private void OnEnable()
    {
        for (int i = 0; i < _storeItemTransforms.Length; i++)
        {
            Vector2 directionVector = new Vector2(6 * Mathf.Sin(2 * Mathf.PI * i / 6), 6 * Mathf.Cos(2 * Mathf.PI * i / 6));
            _storeItemTransforms[i] = new GameObject();
            _storeItemTransforms[i].GetComponent<Transform>().position = _playerRigidbody2D.position + directionVector;
        }

        // create and set position
        for (int i = 0; i < 5; i++)
        {
            GameObject storeItem = CreateStoreItem();
            storeItem.GetComponent<StoreItem>().Init(i);
            storeItem.GetComponent<Rigidbody2D>().position = _storeItemTransforms[i].GetComponent<Transform>().position;
            _storeItems[i] = storeItem;
        }

        _disabledIndex = 5;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _storeItems.Length; i++)
        {
            DestroyStoreItem(i);
            Destroy(_storeItemTransforms[i]);
            _storeItemTransforms[i] = null;
        }
    }

    public GameObject CreateStoreItem()
    {
        GameObject storeItem;
        int random = Random.Range(0, 2);

        if (random == 0)
        {
            storeItem = Instantiate(storeItemActiveWeaponPrefabs[WeaponManager.instance.selectedActiveWeaponId]);
        }

        else
        {
            // max level check
            int newWeaponId = Random.Range(0, storeItemWeaponPrefabs.Length);

            storeItem = Instantiate(storeItemWeaponPrefabs[newWeaponId]);
        }
        return storeItem;
    }

    public void DestroyStoreItem(int index)
    {
        Destroy(_storeItems[index]);
        _storeItems[index] = null;
        _disabledIndex = index;
    }

    public void CreateNewStoreItem()
    {
        GameObject newStoreItem = CreateStoreItem();
        _storeItems[_disabledIndex] = newStoreItem;
        _storeItems[_disabledIndex].GetComponent<Rigidbody2D>().position = _storeItemTransforms[_disabledIndex].GetComponent<Transform>().position;
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
