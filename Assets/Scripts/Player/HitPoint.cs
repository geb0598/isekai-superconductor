using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            PlayerManager.instance.TakeDamage();
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.GetComponent<DropItem>().Get();
        }
    }
}
