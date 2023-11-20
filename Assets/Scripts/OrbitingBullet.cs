using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingBullet : Bullet
{
    [SerializeField] float _radius;
    [SerializeField] bool _isClockwise;


    public override void Initialize(Transform launcherTransform, Vector2 direction, bool isPlayerBullet, float damage)
    {
        _launcherTransform = launcherTransform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _direction = direction.normalized;
        _isPlayerBullet = isPlayerBullet;
        _damage = damage;
    }
    protected override void UpdateBulletTransform()
    {
        Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, _speed * Time.fixedDeltaTime);
        _direction = (rotation * _direction).normalized;
        transform.position = _launcherTransform.position + _radius * (Vector3)_direction;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg);
    }
}
