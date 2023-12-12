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
        _offsetVector = new Vector3(-2f, 2f, 0);
    }

    private void UpdateDescription()
    {
        // nameText.text = string.Format("Name : {0}", name);
        // levelText.text = string.Format("Level : {0} -> {1}", curlevel, curlevel+1);
        // descriptionText.text = string.Format();
        // priceText.text = string.Format("{0}", price);
    }


    private void Update()
    {
        // if (level >= maxLevel) return;
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
