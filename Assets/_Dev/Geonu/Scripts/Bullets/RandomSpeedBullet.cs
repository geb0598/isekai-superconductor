using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpeedBullet : Bullet
{
    public float _defaultSpeed;

    public override void Initialize(Transform launcher, Vector2 target, bool isPlayerBullet, float damage)
    {
        base.Initialize(launcher, target, isPlayerBullet, damage);
    }

    protected override void UpdateBulletTransform()
    {
        _speed = _defaultSpeed + 8 * Mathf.Sin(_elapsedTimeSeconds);
        Vector2 movementVector = _direction * _speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + movementVector);
    }
}
