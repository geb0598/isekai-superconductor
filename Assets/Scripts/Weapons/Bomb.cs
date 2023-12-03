using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Weapon
{
    public override IEnumerator Attack()
    {
        if (_isDelay) yield break;
        _isDelay = true;
        _bulletLauncher.Launch(transform.position);
        yield return new WaitForSeconds(attackDelaySeconds);
        _isDelay = false;
    }

    public override float GetDamage(float playerPower)
    {
        return power * playerPower;
    }

    private void Awake()
    {
        _bulletLauncher = GetComponent<BulletLauncher>();
        _level = 1;
        _isDelay = false;
    }
}
