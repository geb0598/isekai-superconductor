using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _acceleration;

    [SerializeField] private bool _isPenetrative = false;

    [SerializeField] private bool _hasLifeTime;
    [SerializeField] private float _lifeTimeSeconds;

    protected Rigidbody2D _rigidbody;

    protected Transform _launcherTransform;

    protected BulletLauncher _bulletLauncher;

    protected Vector2 _direction;

    protected Vector2 _childDirection;

    protected bool _isPlayerBullet;

    protected float _damage;

    protected float _elapsedTimeSeconds;


    public virtual void Initialize(Transform launcherTransform, Vector2 direction, bool isPlayerBullet, float damage)
    {
        _launcherTransform = launcherTransform;
        _rigidbody = GetComponent<Rigidbody2D>();
        transform.position = _launcherTransform.position;
        _direction = direction.normalized;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, _direction);
        _isPlayerBullet = isPlayerBullet;
        _damage = damage;
        _elapsedTimeSeconds = 0.0f;
        _bulletLauncher = GetComponent<BulletLauncher>();
    }

    protected virtual void UpdateBulletTransform()
    {
        Vector2 movementVector = _direction * _speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + movementVector);
    }

    private void FixedUpdate()
    {
        _elapsedTimeSeconds += Time.deltaTime;

        if (_hasLifeTime && _elapsedTimeSeconds > _lifeTimeSeconds)
        {
            GenerateBullet();
            Destroy(gameObject);
        }

        _speed += _acceleration * Time.deltaTime;

        UpdateBulletTransform();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyDamage(collision);
    } 

    protected virtual void ApplyDamage(Collider2D collision) {
        if (_isPlayerBullet && collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // collision.gameObject.GetComponent<Enemy>().GetDamaged(_damage);
            if (!_isPenetrative)
            {
                _childDirection = Vector2.up;
                GenerateBullet();
                Destroy(gameObject);
            }
        }
        else if (!_isPlayerBullet && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerManager.instance.TakeDamage();
            if (!_isPenetrative)
            {
                Destroy(gameObject);
            }
        }
    }

    protected void GenerateBullet()
    {
        _bulletLauncher?.Launch(_childDirection);
    }
}
