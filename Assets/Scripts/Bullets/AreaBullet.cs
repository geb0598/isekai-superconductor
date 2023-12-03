using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBullet : Bullet
{
    public override void Initialize(Transform launcher, Vector2 target, bool isPlayerBullet, float damage)
    {
        base.Initialize(launcher, target, isPlayerBullet, damage);

        transform.position = target;
    }

    protected override void UpdateBulletTransform()
    {
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(GenerateBullets(collision));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        ApplyDamage(collision);
    }
}
