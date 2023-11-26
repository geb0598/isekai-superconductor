using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBullet : Bullet
{
    public float amplitude = 1;
    public float frequency = 1;

    Vector2 _startPosition;
    float _thetaRadian;


    public override void Initialize(Transform launcher, Vector2 target, bool isPlayerBullet, float damage)
    {
        base.Initialize(launcher, target, isPlayerBullet, damage);
        _startPosition = launcher.position;
        _thetaRadian = Mathf.Acos(Vector2.Dot(_direction, Vector2.right));
        if (_direction.y < 0)
            _thetaRadian = 2f * Mathf.PI - _thetaRadian;
    }

    protected override void UpdateBulletTransform()
    {
        Vector2 movementVector;
        movementVector.x = _elapsedTimeSeconds * _speed * Mathf.Cos(_thetaRadian) - Sin(_elapsedTimeSeconds * _speed) * Mathf.Sin(_thetaRadian);
        movementVector.y = _elapsedTimeSeconds * _speed * Mathf.Sin(_thetaRadian) + Mathf.Cos(_thetaRadian) * Sin(_elapsedTimeSeconds * _speed);
        _rigidbody.MovePosition(_startPosition + movementVector);
    }

    private float Sin(float thetaRadian)
    {
        return amplitude * Mathf.Sin(2 * Mathf.PI * frequency * thetaRadian);
    }

    private float Cos(float thetaRadian)
    {
        return amplitude * Mathf.Cos(2 * Mathf.PI * frequency * thetaRadian);
    }
}
