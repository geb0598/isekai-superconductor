using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGatheringPoint : MonoBehaviour
{
    [SerializeField] private float _itemGatheringSpeed;
    [SerializeField] private float _itemGatheringRange;

    private void Update()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _itemGatheringRange, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Item"))
            {
                Vector2 direction = (transform.position - hit.collider.transform.position).normalized;
                hit.collider.GetComponent<Rigidbody2D>().MovePosition((Vector2)hit.collider.transform.position + direction * _itemGatheringSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.GetComponent<DropItem>().Get();
        }
    }
}
