using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _acceleration;

    [SerializeField] protected float _damageCoefficient;

    [SerializeField] protected float _lifeTimeSeconds;

    [SerializeField] private bool _isPenetrative;

    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;
    protected BulletLauncher _bulletLauncher;
    protected TargetFinder _targetFinder;

    protected Transform _launcher;

    protected bool _isPlayerBullet;

    protected float _damage;

    protected float _elapsedTimeSeconds;

    public abstract void Initialize(Transform launcher, Vector2 target, bool isPlayerBullet, float damage);

    protected abstract void UpdateBulletTransform();

    public void Deactivate()
    {
        // Pooling required
        Destroy(gameObject);
    }

    protected void ApplyDamage(Collider2D collision)
    {
        if (_isPlayerBullet && collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(_damage * _damageCoefficient);
        }
        else if (!_isPlayerBullet && collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.instance.TakeDamage();
        }
    }

    private IEnumerator GenerateBullets()
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

    private IEnumerator GenerateBullets(Collider2D collision)
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
        if (_launcher == null)
            return;

        _elapsedTimeSeconds += Time.fixedDeltaTime;

        if (_lifeTimeSeconds != 0 && _elapsedTimeSeconds > _lifeTimeSeconds)
        {
            StartCoroutine(GenerateBullets());
        }

        UpdateBulletTransform();

        _speed += _acceleration * Time.fixedDeltaTime;
        _speed = Mathf.Max(_speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyDamage(collision);

        if (!_isPenetrative && collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(GenerateBullets(collision));
        }
    } 
}
