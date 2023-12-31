using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PooledObject
{
    public DamageText damageTextPrefab;
    public Coin coinPrefab;
    public HealthPack healthPackPrefab;

    // ???
    private static float[] healthPointCoefficients = { 1, 1.05f, 1.1f, 1.3f, 1.35f, 1.4f, 1.6f, 1.65f, 1.7f, 1.9f, 1.95f, 2, 2.2f, 2.25f, 2.3f };

    [SerializeField]
    protected EnemyStat _stat;

    protected Rigidbody2D _target;
    protected Rigidbody2D _rigidbody2d;

    protected bool _isLive;

    private Animator _animator;
    private SpriteRenderer[] _spriteRenderers;
    private WaitForFixedUpdate _waitForFixedUpdate;

    private float _curHp;

    private float _invincibleTimer;
    private float _invincibleTime;
    private bool _isInvincible;

    private void Awake()
    {
        _target = GameManager.GetInstance().playerController.GetComponent<Rigidbody2D>();
        _rigidbody2d = GetComponent<Rigidbody2D>();

        _animator = GetComponent<Animator>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _waitForFixedUpdate = new WaitForFixedUpdate();

        _animator.SetBool("isMoving", true);

        _invincibleTime = 0.1f;
    }

    virtual protected void FixedUpdate()
    {
        if (_isInvincible)
            _invincibleTimer += Time.fixedDeltaTime;

        if (_invincibleTimer >= _invincibleTime) 
        {
            _isInvincible = false;
        }
    }

    protected void Move()
    {
        Vector2 dirVec = _target.position - _rigidbody2d.position;
        Vector2 nextVec = dirVec.normalized * _stat.speed * Time.fixedDeltaTime;

        _rigidbody2d.MovePosition(_rigidbody2d.position + nextVec);
        _rigidbody2d.velocity = Vector2.zero;
    }

    protected void SpriterFlipX()
    {
        if (_rigidbody2d.position.x > _target.position.x)
        {
            for (int i = 0; i < _spriteRenderers.Length; i++)
            {
                _spriteRenderers[i].flipX = false;

                if (_spriteRenderers[i].CompareTag("RightWing"))
                {
                    _spriteRenderers[i].sortingOrder = 2;
                }

                if (_spriteRenderers[i].CompareTag("LeftWing"))
                {
                    _spriteRenderers[i].sortingOrder = -1;
                }
            }
        }

        else
        {
            for (int i = 0; i < _spriteRenderers.Length; i++)
            {
                _spriteRenderers[i].flipX = true;

                if (_spriteRenderers[i].CompareTag("RightWing"))
                {
                    _spriteRenderers[i].sortingOrder = -1;
                }

                if (_spriteRenderers[i].CompareTag("LeftWing"))
                {
                    _spriteRenderers[i].sortingOrder = 2;
                }
            }
        }
    }

    virtual public void Init()
    {
        _curHp = this._stat.defaultMaxHp * healthPointCoefficients[GameManager.GetInstance().subWave];
    }

    public void TakeDamage(float damage)
    {
        if (_isInvincible)
            return;

        _curHp -= damage;
        _isInvincible = true;
        _invincibleTimer = 0f;

        ShowDamage(damage);

        if (_curHp > 0)
        {
            StartCoroutine(KnockBack(_stat.knockBackCoefficient));
        }

        else
        {
            Died();
        }
    }

    public IEnumerator KnockBack(float knockBackCoefficient)
    {
        yield return _waitForFixedUpdate;

        Vector3 playerPos = GameManager.GetInstance().playerController.transform.position;
        Vector3 dirVec = transform.position - playerPos;

        _rigidbody2d.AddForce(dirVec.normalized * knockBackCoefficient);
    }

    private void Died() 
    {
        _isLive = false;
        DropCoin();
        DropHealthPack();
        GameManager.GetInstance().eventManager.enemyKilledEvent.Invoke();
        gameObject.SetActive(false);
    }

    private void DropCoin()
    {
        float amount = Random.Range(1f, 2f);
        int random = Random.Range(0, 100);
        int ratio;

        if (random < 1)
            ratio = 100;
        else if (random < 10)
            ratio = 10;
        else
            ratio = 1;

        Coin coin = coinPrefab.GetPooledObject().GetComponent<Coin>();
        coin.transform.position = transform.position;
        coin.Init(Mathf.FloorToInt(amount * ratio));
    }

    private void DropHealthPack()
    {
        float random = Random.Range(0f, 1f);

        if (random <= 0.01f)
        {
            HealthPack healthPack = Instantiate(healthPackPrefab);
            healthPack.transform.position = transform.position;
        }
    }

    private void ShowDamage(float damage)
    {
        if (!GameManager.GetInstance().isShowDamage)
            return;
        if (damage <= 0)
            return;
        DamageText damageText = damageTextPrefab.GetPooledObject().GetComponent<DamageText>();
        damageText.Init(damage, transform);
    }
}
