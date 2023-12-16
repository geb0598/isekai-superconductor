using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemDescription : MonoBehaviour
{
    public GameObject storeItem;
    public Text nameText;
    public Text levelText;
    public Text descriptionText;
    public Text priceText;

    private RectTransform _rect;

    private Vector3 _offsetVector;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    private void UpdateDescription()
    {
        int level;
        if (storeItem.GetComponent<StoreItem>().type == 0)
            level = WeaponManager.instance.GetWeapon(storeItem.GetComponent<StoreItem>().id).level;
        else 
            level = WeaponManager.instance.GetActiveWeapon(storeItem.GetComponent<StoreItem>().id).level;

        nameText.text = string.Format("Name : {0}", storeItem.name.Replace("StoreItem", "").Replace("(Clone)", "")).Replace("_A", "");
        levelText.text = string.Format("Level : {0} -> {1}", level, level + 1);
        descriptionText.text = storeItem.GetComponent<StoreItem>().descriptions[level];
        priceText.text = string.Format("Price : {0} Coin",storeItem.GetComponent<StoreItem>().prices[level]);
    }


    private void Update()
    {
        if (PlayerManager.instance.player.transform.position.x - storeItem.transform.position.x > 0)
            _offsetVector = new Vector3(-2f, 2f, 0);
        else
            _offsetVector = new Vector3(2f, 2f, 0);

        _rect.position = Camera.main.WorldToScreenPoint(storeItem.transform.position + _offsetVector);

        UpdateDescription();
    }

    public void EnableDescription()
    {
        gameObject.SetActive(true);
    }

    public void DisableDescription()
    {
        gameObject.SetActive(false);
    }
}
