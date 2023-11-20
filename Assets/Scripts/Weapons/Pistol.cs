using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pistol : Weapon
{
    public override float GetDamage(float playerPower)
    {
        return power * playerPower;
    }

    public override IEnumerator Attack()
    {
        List<Vector2> targets = _targetFinder.FindTargets(transform.position, _enemyLayer, new List<GameObject>());
        foreach(Vector2 target in targets)
        {
            yield return new WaitUntil(() => _bulletLauncher.isLaunchReady);
            _bulletLauncher.Launch(target);
        }
        yield return new WaitForSeconds(attackDelaySeconds);
        _isDelay = false;
    }

    private void Awake()
    { 
        _bulletLauncher = GetComponent<BulletLauncher>();
        _targetFinder = GetComponent<TargetFinder>();
        _enemyLayer = LayerMask.GetMask("RangedEnemy", "MeleeEnemy");
        _level = 1;
        _isDelay = false;
    }

    private void Update()
    {
        if (!_isDelay)
        {
            _isDelay = true;
            StartCoroutine(Attack());
        }
    }
}
