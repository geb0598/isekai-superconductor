using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _acceleration;

    [SerializeField] private bool _isPenetrative = false;

    [SerializeField] private float _lifeTimeSeconds;

    protected Vector2 _direction;
    protected Vector2 _position;

    private Rigidbody2D _rigidbody;

    private bool _isPlayerBullet;

    private float _damage;

    private float _elapsedTimeSeconds;

    public void Initialize(Vector2 position, Vector2 direction, bool isPlayerBullet, float damage)
    {
        _position = position;
        transform.position = _position;
        _direction = direction.normalized;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, _direction);
        _rigidbody = GetComponent<Rigidbody2D>();
        _isPlayerBullet = isPlayerBullet;
        _damage = damage;
        _elapsedTimeSeconds = 0.0f;
    }

    protected virtual void UpdateBulletTransform()
    {
        Vector2 movementVector = _direction * _speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + movementVector);
    }

    private void FixedUpdate()
    {
        _elapsedTimeSeconds += Time.deltaTime;

        if (_elapsedTimeSeconds > _lifeTimeSeconds )
        {
            Destroy(gameObject);
        }

        _speed += _acceleration * Time.deltaTime;

        UpdateBulletTransform();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyDamage(collision);
    } 

    private void ApplyDamage(Collider2D collision) {
        Debug.Log(collision.gameObject.layer);
        if (_isPlayerBullet && collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // collision.gameObject.GetComponent<Enemy>().GetDamaged(_damage);
            if (!_isPenetrative)
            {
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
}
