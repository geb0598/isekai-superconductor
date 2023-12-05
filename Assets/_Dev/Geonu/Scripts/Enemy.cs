using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
    private float _invincibleTime = 0.5f; // test init
    private bool _isInvincible;

    private void Awake()
    {
        _target = GameManager.GetInstance().playerController.GetComponent<Rigidbody2D>();
        _rigidbody2d = GetComponent<Rigidbody2D>();

        _animator = GetComponent<Animator>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _waitForFixedUpdate = new WaitForFixedUpdate();

        _animator.SetBool("isMoving", true);
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
        _curHp = this._stat.maxHp;
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
            StartCoroutine(KnockBack());
        }

        else
        {
            Died();
        }
    }

    private void Died() 
    {
        _isLive = false;
        DropCoin();
        GameManager.GetInstance().eventManager.enemyKilledEvent.Invoke();
        gameObject.SetActive(false);
    }

    private IEnumerator KnockBack()
    {
        yield return _waitForFixedUpdate;

        Vector3 playerPos = GameManager.GetInstance().playerController.transform.position;
        Vector3 dirVec = transform.position - playerPos;

        _rigidbody2d.AddForce(dirVec.normalized * _stat.knockBackCoefficient);
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

        Coin coin = GameManager.GetInstance().poolManager.Get(2, 0).GetComponent<Coin>();
        coin.transform.position = transform.position;
        coin.Init(Mathf.FloorToInt(amount * ratio));
    }

    private void ShowDamage(float damage)
    {
        if (!GameManager.GetInstance().isShowDamage)
            return;
        if (damage <= 0)
            return;
        GameObject damageText = GameManager.GetInstance().poolManager.Get(2, 3);
        damageText.GetComponent<DamageText>().Init(damage, transform);
    }
}
