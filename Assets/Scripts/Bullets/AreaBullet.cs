using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBullet : Bullet
{
    public override void Initialize(Transform launcherTransform, Vector2 target, bool isPlayerBullet, float damage)
    {
        _launcher = launcherTransform;
        _direction = target;
        _isPlayerBullet = isPlayerBullet;
        _elapsedTimeSeconds = 0.0f;

        transform.position = target;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        ApplyDamage(collision);
    }
}
