using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D _target;
    private Rigidbody2D _rigidbody2D;
    private Renderer _renderer;
    private Vector2 _dirVec;

    private float _speed;
    private float _timer;
    private float _expCheckTime = 0.2f;

    private bool _isInGrazeCollider;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        // inactive when out of camera
        if (!_renderer.isVisible)
        {
            gameObject.SetActive(false);
        }

        _timer += Time.fixedDeltaTime;

        if (_timer > _expCheckTime)
        {
            if (_isInGrazeCollider)
            {
                DropExp();
                _isInGrazeCollider = false;
            }

            _timer = 0f;
        }

        _rigidbody2D.MovePosition(_rigidbody2D.position + _dirVec * _speed * Time.fixedDeltaTime);
        _rigidbody2D.velocity = Vector2.zero;
    }

    // Called by RangedEnemy
    public void Init(Vector2 newPos, float bulletSpeed)
    {
        _speed = bulletSpeed;
        _rigidbody2D.position = newPos;
        _target = GameManager.GetInstance().playerController.GetComponent<Rigidbody2D>();
        _dirVec = (_target.position - _rigidbody2D.position).normalized;
    }

    private void DropExp()
    {
        int random = Random.Range(0, 100);
        int amount;

        if (random < 1)
            amount = 3;
        else if (random < 10)
            amount = 2;
        else
            amount = 1;

        Exp exp = GameManager.GetInstance().poolManager.Get(2, 2).GetComponent<Exp>();
        Vector3 dirVec = _rigidbody2D.position - _target.position;

        exp.transform.position = transform.position - dirVec.normalized * 2.5f;
        exp.Init(amount);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Graze"))
        {
            _isInGrazeCollider = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Graze"))
        {
            _isInGrazeCollider = false;
        }
    }
}
