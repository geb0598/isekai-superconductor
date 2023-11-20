using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingBullet : Bullet
{
    [SerializeField] float _radius;
    [SerializeField] bool _isClockwise;
    [SerializeField] bool _isHeadingUpwards;

    public override void Initialize(Transform launcher, Vector2 target, bool isPlayerBullet, float damage)
    {
        _launcher = launcher;
        _rigidbody = GetComponent<Rigidbody2D>();
        _direction = (target - (Vector2)launcher.position).normalized;
        _isPlayerBullet = isPlayerBullet;
        _damage = damage;

        transform.position = _launcher.position + _radius * (Vector3)_direction;
    }

    protected override void UpdateBulletTransform()
    {
        Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, _speed * Time.fixedDeltaTime);
        _direction = (rotation * _direction).normalized;
        transform.position = _launcher.position + _radius * (Vector3)_direction;
        if (!_isHeadingUpwards)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg);
        }
    }
}
