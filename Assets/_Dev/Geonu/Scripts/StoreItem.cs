using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem : MonoBehaviour
{
    public int id;
    private string _name;

    private void Awake()
    {
        _name = gameObject.name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.GetInstance().eventManager.storeItemApproachEvent.Invoke(id);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.GetInstance().eventManager.storeItemLeaveEvent.Invoke();
        }
    }
}
