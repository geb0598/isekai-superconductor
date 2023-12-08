using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBelt : Accessory
{
    [SerializeField] GameObject _FX;
    [SerializeField] private float _range;
    [SerializeField] private float _knockbackPower;

    void explosion()
    {
        // subscribe take damage event
        Instantiate(_FX, transform.position, Quaternion.identity);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _range, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                StartCoroutine(hit.collider.GetComponent<Enemy>().KnockBack(_knockbackPower));
            }
        }
    }
}
