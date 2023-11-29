using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGatheringPoint : MonoBehaviour
{
    [SerializeField] private float _gatheringRange;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {

        }
    }

    private void GatherItem(GameObject item)
    {
        
    }
}
