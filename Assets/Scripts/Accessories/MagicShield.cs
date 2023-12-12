using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShield : Accessory
{
    [SerializeField] private GameObject _shieldFXprefab;
    [SerializeField] private float _delaySeconds;

    private GameObject _player;
    private GameObject _hitPoint;
    private GameObject _shieldFX;

    private float _elapsedTimeAfterUse;

    private bool _isDelay;

    public float remainingCooldownTime { get => _delaySeconds - _elapsedTimeAfterUse; }

    private void Start()
    {
        _player = PlayerManager.instance.player;
        _hitPoint = _player.transform.Find("HitPoint").gameObject;
        _shieldFX = Instantiate(_shieldFXprefab, transform);
        _shieldFX.transform.rotation = Quaternion.Euler(90, 0, 0);

        _elapsedTimeAfterUse = _delaySeconds;
        _isDelay = false;
    }

    private void Update()
    {
        if (_isDelay)
        {
            _elapsedTimeAfterUse += Time.deltaTime;
            _elapsedTimeAfterUse = Mathf.Min(_elapsedTimeAfterUse, _delaySeconds);
        }
    }

    private IEnumerator cooldown()
    {
        if (_isDelay) yield break;
        _isDelay = true;
        _hitPoint.GetComponent<Collider2D>().enabled = true;
        _shieldFX.SetActive(false);
        _elapsedTimeAfterUse = 0;
        yield return new WaitForSeconds(_delaySeconds);
        _isDelay = false;
        _hitPoint.GetComponent<Collider2D>().enabled = false;
        _shieldFX.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet"))
        {
            StartCoroutine(cooldown());
        }
    }

}
