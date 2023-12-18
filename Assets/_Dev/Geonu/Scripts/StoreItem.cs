using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem : DropItem
{
    private int index;

    public int[] prices;
    public string[] descriptions;

    public GameObject storeItemDescriptionPrefab;
    private GameObject _storeItemDescription;

    public void Init(int index)
    {
        this.index = index;
    }

    public void OnDestroy()
    {
        Destroy(_storeItemDescription);
    }

    public override void Get()
    {
        int level = (type == 0) ? WeaponManager.instance.GetWeapon(id).level : WeaponManager.instance.GetActiveWeapon(id).level;
        Debug.Log("Get Item : " + name.Replace("StoreItem", "").Replace("(Clone)", ""));

        if (PlayerManager.instance.coin < prices[level])
        {
            NotEnoughCoin();
            return;
        }

        if (level >= 1)
        {
            WeaponManager.instance.LevelUp(id);
        }

        else
        {
            WeaponManager.instance.ActivateWeapon(id);
        }
        
        GameManager.GetInstance().eventManager.storeItemPurchaseEvent.Invoke(index);
    }

    private void NotEnoughCoin()
    {
        Debug.Log("Not enough coin");
        if (_storeItemDescription == null)
        {
            _storeItemDescription = Instantiate(storeItemDescriptionPrefab);
            _storeItemDescription.GetComponentInChildren<StoreItemDescription>().storeItem = this.gameObject;
        }
        _storeItemDescription.GetComponentInChildren<StoreItemDescription>().priceText.color = Color.red;
    }

    // for description ui
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("StoreItemDescriptionTrigger"))
            return;

        if (_storeItemDescription != null)
            return;

        _storeItemDescription = Instantiate(storeItemDescriptionPrefab);
        _storeItemDescription.GetComponentInChildren<StoreItemDescription>().storeItem = this.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("StoreItemDescriptionTrigger"))
            return;

        Destroy(_storeItemDescription);
    }
}
