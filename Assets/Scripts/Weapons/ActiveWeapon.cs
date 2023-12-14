using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Weapon
{
    public override IEnumerator Attack()
    {
        if (_isDelay) yield break;
        _isDelay = true;
        _elapsedTimeAfterAttack = 0;
        _bulletLauncher.Launch(transform.parent.position);
        yield return new WaitForSeconds(attackDelaySeconds);
        _isDelay = false;
    }
}
