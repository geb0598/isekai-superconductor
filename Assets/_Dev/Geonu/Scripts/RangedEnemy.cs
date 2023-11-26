using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    private BulletLauncher _bulletLauncher;

    private float _defaultRange = 5f;
    private float _maxRange;

    private bool _isInRange;

    private float _timer;

    override protected void FixedUpdate()
    {
        base.FixedUpdate();

        if (!_isLive)
            return;

        SpriterFlipX();

        float dist = (_target.position - _rigidbody2d.position).magnitude;

        if (_isInRange)
        {
            _rigidbody2d.velocity = Vector2.zero;

            if (dist >= _maxRange)
            {
                _isInRange = false;
                _timer = 0f;
                return;
            }

            _timer += Time.fixedDeltaTime;
            if (_timer >= _stat.attackSpeed)
            {
                // Shoot();
                _bulletLauncher.Launch(_target.position);
                _timer = 0f;
            }

            return;
        }

        if (dist <= _defaultRange)
        {
            _isInRange = true;
            return;
        }

        if (!_isLive)
            return;

        Move();
    }


    private void Shoot()
    {
        GameObject enemyBullet = GameManager.GetInstance().poolManager.Get(2, 1);
        enemyBullet.GetComponent<EnemyBullet>().Init(_rigidbody2d.position, _stat.bulletSpeed);
    }

    private void OnEnable()
    {
        _timer = 0f;
        _isLive = true;
        _isInRange = false;
        _bulletLauncher = GetComponent<BulletLauncher>();
    }

    public override void Init()
    {
        base.Init();
        this._maxRange = _defaultRange + Random.Range(2f, 4f);
    }
}
