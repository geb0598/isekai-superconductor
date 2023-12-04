using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected GameObject _collisionEffect;
    [SerializeField] protected GameObject _destroyEffect;
    [SerializeField] protected float _lifeTimeSeconds;

    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;
    protected BulletLauncher _bulletLauncher;
    protected TargetFinder _targetFinder;

    protected Transform _launcher;
    protected Vector2 _target;
    protected bool _isPlayerBullet;
    protected float _damage;

    protected float _elapsedTimeSeconds;

    public Transform launcher { get => _launcher; }
    public Vector2 target { get => _target; }
    public bool isPlayerBullet { get => _isPlayerBullet; }

    protected abstract void UpdateBulletTransform();

    public virtual void Initialize(Transform launcher, Vector2 target, bool isPlayerBullet, float damage)
    {
        _launcher = launcher;
        _target = target;
        _isPlayerBullet = isPlayerBullet;
        _damage = damage;
        _elapsedTimeSeconds = 0.0f;
    }

    public void Deactivate()
    {
        if (_destroyEffect != null)
        {
            Instantiate(_destroyEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    protected void ApplyDamage(Collider2D collision)
    {
        if (_isPlayerBullet && collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
        }
        else if (!_isPlayerBullet && collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.instance.TakeDamage();
        }
    }

    protected IEnumerator GenerateBullets()
    {
        if (_targetFinder != null)
        {
            List<GameObject> filteringObjects = new List<GameObject>();
            List<Vector2> targets = _targetFinder.FindTargets(transform.position, 0, filteringObjects);
            foreach (Vector2 target in targets)
            {
                yield return new WaitUntil(() => _bulletLauncher.isLaunchReady);
                _bulletLauncher.Launch(target);
            }
            yield return new WaitUntil(() => _bulletLauncher.isLaunchReady);
        }
        Deactivate();
    }

    protected IEnumerator GenerateBullets(Collider2D collision)
    {
        if (_targetFinder != null)
        {
            List<GameObject> filteringObjects = new List<GameObject>() { collision.gameObject };
            List<Vector2> targets = _targetFinder.FindTargets(transform.position, collision.gameObject.layer, filteringObjects);
            foreach (Vector2 target in targets)
            {
                yield return new WaitUntil(() => _bulletLauncher.isLaunchReady);
                _bulletLauncher.Launch(target);
            }
            yield return new WaitUntil(() => _bulletLauncher.isLaunchReady);
        }
        Deactivate();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _bulletLauncher = GetComponent<BulletLauncher>();
        _targetFinder = GetComponent<TargetFinder>();
    }

    private void FixedUpdate()
    {
        _elapsedTimeSeconds += Time.fixedDeltaTime;

        if (_lifeTimeSeconds != 0 && _elapsedTimeSeconds > _lifeTimeSeconds)
        {
            StartCoroutine(GenerateBullets());
        }

        UpdateBulletTransform();
    }
}
