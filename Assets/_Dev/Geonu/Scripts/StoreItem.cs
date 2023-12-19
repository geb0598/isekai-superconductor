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
        Debug.Log(string.Format("Get Item : " + name.Replace("StoreItem", "").Replace("(Clone)", "") + "level: {0}", level));

        if (PlayerManager.instance.coin < prices[level])
        {
            NotEnoughCoin();
            return;
        }

        if (level >= 1)
        {
            if (type == 0)
                WeaponManager.instance.GetWeapon(id).LevelUp();
            else
                WeaponManager.instance.GetActiveWeapon(WeaponManager.instance.selectedActiveWeaponId).LevelUp();
            GameManager.GetInstance().eventManager.weaponLevelUpEvent.Invoke(type, id);
        }

        else
        {
            WeaponManager.instance.ActivateWeapon(id);
        }
        
        GameManager.GetInstance().eventManager.storeItemPurchaseEvent.Invoke(index);
        PlayerManager.instance.AddCoin(-prices[level]);
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
