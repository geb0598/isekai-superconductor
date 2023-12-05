using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGatheringPoint : MonoBehaviour
{
    [SerializeField] private float _itemGatheringSpeed;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            collision.GetComponent<Rigidbody2D>().MovePosition((Vector2)collision.transform.position + direction * _itemGatheringSpeed * Time.deltaTime);
        }
    }
}
