using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;

    [SerializeField] private GameObject _player;

    [SerializeField] private int _maxHealthPointsLimit;
    [SerializeField] private int _defaultHealthPoints;
    [SerializeField] private int _healthPointsScaleFactor;

    [SerializeField] private float _defaultPower;
    [SerializeField] private float _powerScaleFactor;

    [SerializeField] private int _levelLimit;

    [SerializeField] private int _defaultExperiencePoints;
    [SerializeField] private int _experiencePointsScaleFactor;

    [SerializeField] private int _invincibleTimeSeconds;

    private int _healthPoints;
    private int _extraHealthPoints;

    private int _coin;

    private int _level;

    private int _experiencePoints;

    private bool _isInvincible;

    private bool _isDead;

    public static PlayerManager instance { get => _instance; }

    public GameObject player { get => _player; }

    public int healthPoints { get => _healthPoints; }

    public int maxHealthPoints { get => Mathf.Min(_defaultHealthPoints + level / _healthPointsScaleFactor + _extraHealthPoints, _maxHealthPointsLimit); }

    public float power { get => _defaultPower + _level * _powerScaleFactor; }

    public int coin {  get => _coin; }

    public int level { get => _level; }

    public int levelLimit { get => _levelLimit; }

    public int experiencePoints { get => _experiencePoints; }

    public int experiencePointsRequired { get => _defaultExperiencePoints + level * _experiencePointsScaleFactor; }

    public bool isInvincible { get => _isInvincible; }

    public bool isDead { get => _isDead; }    

    public void TakeDamage()
    {
        if (isDead) return;

        if (isInvincible) return;

        GameManager.GetInstance().eventManager.playerTakeDamageEvent.Invoke();

        if (--_healthPoints > 0)
        {
            StartCoroutine(OnPlayerInvincible());
        }
        else
        {
            OnPlayerDead();
        }
    }

    public void RestoreHealthPoints()
    {
        if (_isDead) return;

        if (_healthPoints == maxHealthPoints) return;

        ++_healthPoints;
    }

    public void AddExtraHealthPoints()
    {
        if (_isDead) return;

        ++_extraHealthPoints;
    }

    public void AddExperiencePoints()
    {
        if (_isDead) return;

        if (++_experiencePoints >= experiencePointsRequired)
        {
            LevelUp();
        }
    }

    public void AddCoin(int amount)
    {
        _coin += amount;
        GameManager.GetInstance().eventManager.playerTakeCoinEvent.Invoke(amount);
    }

    public void LevelUp()
    {
        if (_isDead) return;

        if (_level == _levelLimit) return;

        ++_level;
        _experiencePoints = 0;
        GameManager.GetInstance().eventManager.playerLevelUpEvent.Invoke(); // for HUD levelUp
    }

    private void OnPlayerDead()
    {
        _isDead = true;
    }

    private IEnumerator OnPlayerInvincible()
    {
        _isInvincible = true;
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;
        yield return new WaitForSeconds(_invincibleTimeSeconds);
        color.a = 1.0f;
        spriteRenderer.color = color;
        _isInvincible = false;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        _healthPoints = _defaultHealthPoints;
        _extraHealthPoints = 0;
        _coin = 500; // start coin
        _level = 1;
        _experiencePoints = 0;
        _isInvincible = false;
        _isDead = false;
    }
}
