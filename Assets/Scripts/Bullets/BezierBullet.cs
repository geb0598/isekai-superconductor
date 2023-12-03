using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierBullet : Bullet
{
    [SerializeField] private float _height;

    private Vector2 _startPoint;
    private Vector2 _bezierPoint;
    private float _bezierParam;

    public override void Initialize(Transform launcher, Vector2 target, bool isPlayerBullet, float damage)
    {
        base.Initialize(launcher, target, isPlayerBullet, damage);

        transform.position = _launcher.position;

        _startPoint = _launcher.position;
        _bezierPoint = (_startPoint + _target) / 2 + new Vector2(0, _height);
        _bezierParam = 0.0f;
    }

    protected override void UpdateBulletTransform()
    {
        _bezierParam += Time.fixedDeltaTime / _lifeTimeSeconds;
        transform.position = BezierInterpolate(_startPoint, _target, _bezierParam);
    }

    private Vector2 BezierInterpolate(Vector2 startPoint, Vector2 endPoint, float param)
    {
        Vector2 p1 = Vector2.Lerp(startPoint, _bezierPoint, param);
        Vector2 p2 = Vector2.Lerp(_bezierPoint, endPoint, param);
        return Vector2.Lerp(p1, p2, param);
    }
}
