using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierBullet : Bullet
{
    [SerializeField] private float _height;

    private Vector2 _startPoint;
    private Vector2 _bezierPoint;
    private float _bezierParameter;

    public override void Initialize(Transform launcherTransform, Vector2 direction, bool isPlayerBullet, float damage)
    {
        _launcherTransform = launcherTransform;
        _rigidbody = GetComponent<Rigidbody2D>();
        transform.position = _launcherTransform.position;
        _direction = direction;
        _isPlayerBullet = isPlayerBullet;
        _elapsedTimeSeconds = 0.0f;
        _bulletLauncher = GetComponent<BulletLauncher>();

        _startPoint = _launcherTransform.position;
        _bezierPoint = (_startPoint + _direction) / 2 + new Vector2(0, _height);
        _bezierParameter = 0.0f;
    }
    protected override void UpdateBulletTransform()
    {
        _bezierParameter += Time.fixedDeltaTime * _speed;
        transform.position = Mathf.Pow(1 - _bezierParameter, 2) * _startPoint + (1 - _bezierParameter) * _bezierParameter * _bezierPoint + Mathf.Pow(_bezierParameter, 2) * _direction;
        if (_bezierParameter >= 1.0)
        {
            _childDirection = transform.position;
            GenerateBullet();
            Destroy(gameObject);
        }
    }
}
