using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int id;

    [SerializeField] private int _levelLimit;

    [SerializeField] private float _defaultPower;
    [SerializeField] private float _powerScaleFactor;

    [SerializeField] private float _defaultAttackDelaySeconds;

    [SerializeField] private float _defaultAttackSpeed;
    [SerializeField] private float _attackSpeedScaleFactor;

    protected BulletLauncher _bulletLauncher;
    protected TargetFinder _targetFinder;

    protected LayerMask _enemyLayer;

    protected int _level;

    protected bool _isDelay;

    public int level { get => _level; }

    public int levelLimit { get => _levelLimit; }

    public float attackDelaySeconds { get => _defaultAttackDelaySeconds / attackSpeed; }

    public float power { get => _defaultPower + _level * _powerScaleFactor; }

    public float attackSpeed { get => _defaultAttackSpeed + _level * _attackSpeedScaleFactor; }

    public abstract IEnumerator Attack();

    public abstract float GetDamage(float playerPower);
    
    public void LevelUp()
    {
        if (_level == _levelLimit) { return; }
        ++_level;
    }
}
