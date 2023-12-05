using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemDescription : MonoBehaviour
{
    public GameObject[] items;
    public Text nameText;
    public Text levelText;
    public Text descriptionText;
    public Text priceText;

    private RectTransform _rect;
    private int _index;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    private void UpdateDescription()
    {
        // nameText.text = string.Format("Name : {0}", name);
        // levelText.text = string.Format("Level : {0} -> {1}", curlevel, curlevel+1);
        // descriptionText.text = string.Format();
        // priceText.text = string.Format("{0}", price);
    }


    private void OnEnable()
    {
        // if (level >= maxLevel) return;
        Vector3 positionVector = new Vector3(-2f, 2f, 0);
        _rect.position = Camera.main.WorldToScreenPoint(items[_index].transform.position + positionVector);

        UpdateDescription();
    }

    // Caution!!! : Event에서 함수 구독 시 항상 SetIndex 후에 Enable하도록 설정할 것!!!
    private void SetIndex(int index)
    {
        _index = index;
    }

    private void EnableDescription()
    {
        gameObject.SetActive(true);
    }

    private void DisableDescription()
    {
        gameObject.SetActive(false);
    }
}
