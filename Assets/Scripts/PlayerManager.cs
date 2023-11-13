using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager
{
    private static PlayerManager _instance;

    public UnityEvent onPlayerDead;
    public UnityEvent onPlayerInvincible;

    private int _healthPoints;
    private int _maxHealthPointsLimit;
    private int _defaultHealthPoints;
    private int _healthPointsScaleFactor;
    private int _extraHealthPoints;

    private float _defaultPower;
    private float _powerScaleFactor;

    private int _level;
    private int _levelLimit;

    private int _experiencePoints;
    private int _defaultExperiencePoints;
    private int _experiencePointsScaleFactor;

    private int _invincibleFrames;
    private bool _isInvincible = false;

    private bool _isDead = false;

    public static PlayerManager instance { get => _instance ?? (_instance = new PlayerManager()); }

    public int healthPoints { get => _healthPoints; }

    public int maxHealthPoints { get => Mathf.Max(_defaultHealthPoints + level / _healthPointsScaleFactor + _extraHealthPoints, _maxHealthPointsLimit); }

    public float power { get => _defaultPower + _level * _powerScaleFactor; }

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

        if (--_healthPoints > 0)
        {
            OnPlayerInvincible();
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

    public void LevelUp()
    {
        if (_isDead) return;

        if (_level == _levelLimit) return;

        ++_level;
        _experiencePoints = 0;
    }

    private void OnPlayerDead()
    {

    }
    private void OnPlayerInvincible()
    {

    }
}
