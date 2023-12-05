using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DirectBullet : Bullet
{
    [SerializeField] protected float _speed;

    [SerializeField] protected bool _isPenetrative;

    protected Vector2 _direction;

    public override void Initialize(Transform launcher, Vector2 target, bool isPlayerBullet, float damage)
    {
        base.Initialize(launcher, target, isPlayerBullet, damage);

        _direction = (target - (Vector2)launcher.transform.position).normalized;

        transform.position = _launcher.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, _direction);
    }

    protected override void UpdateBulletTransform()
    {
        Vector2 movementVector = _direction * _speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + movementVector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isGenerating)
        {
            return;
        }

        if (_collisionEffect != null && collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(_collisionEffect, collision.transform.position, Quaternion.identity);
        }

        ApplyDamage(collision);

        if (!_isPenetrative && collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(GenerateBullets(collision));
        }
    } 
}
