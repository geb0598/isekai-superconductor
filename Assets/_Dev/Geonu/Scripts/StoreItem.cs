using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem : DropItem
{
    public int index;
    public int id;
    public int price;

    public StoreItemDescription storeItemDescription;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(int id, Sprite sprite)
    {
        this.id = id;
        // price = WeaponManager.instance.GetWeapon(id).price[WeaponManager.instance.GetWeapon(id).level - 1]

        _spriteRenderer.sprite = sprite;
    }

    public override void Get()
    {
        if (PlayerManager.instance.coin < price)
        {
            // Not Enough Coin !!! Text instantiate -> Damage Text reuse.
            return;
        }

        gameObject.SetActive(false);

        if (WeaponManager.instance.GetWeaponPrefab(id).activeSelf)
        {
            WeaponManager.instance.LevelUp(id);
        }

        else
        {
            WeaponManager.instance.ActivateWeapon(id);
        }
        
        GameManager.GetInstance().eventManager.storeItemPurchaseEvent.Invoke(index);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        storeItemDescription.storeItem = this.gameObject;
        storeItemDescription.EnableDescription();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        storeItemDescription.DisableDescription();
    }*/
}
