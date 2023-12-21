using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBullet : Bullet
{
    [SerializeField] private bool _isFollowingLauncher;
    public override void Initialize(Transform launcher, Vector2 target, bool isPlayerBullet, float damage)
    {
        base.Initialize(launcher, target, isPlayerBullet, damage);

        if (_isFollowingLauncher)
        {
            transform.SetParent(launcher);
            transform.position = target;
        } 
        else
        {
            transform.position = target;
        }
    }

    protected override void UpdateBulletTransform()
    {
        return;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        ApplyDamage(collision);
    }
}
