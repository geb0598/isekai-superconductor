using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierBullet : Bullet
{
    [SerializeField] private float _height;

    private Vector2 _startPoint;
    private Vector2 _bezierPoint;
    private float _bezierParameter;

    public override void Initialize(Transform launcher, Vector2 target, bool isPlayerBullet, float damage)
    {
        _launcher = launcher;
        _direction = target;
        _isPlayerBullet = isPlayerBullet;
        _elapsedTimeSeconds = 0.0f;

        transform.position = _launcher.position;

        _startPoint = _launcher.position;
        _bezierPoint = (_startPoint + _direction) / 2 + new Vector2(0, _height);
        _bezierParameter = 0.0f;
    }

    protected override void UpdateBulletTransform()
    {
        _bezierParameter += Time.fixedDeltaTime / _lifeTimeSeconds;
        transform.position = Mathf.Pow(1 - _bezierParameter, 2) * _startPoint + (1 - _bezierParameter) * _bezierParameter * _bezierPoint + Mathf.Pow(_bezierParameter, 2) * _direction;
    }
}
