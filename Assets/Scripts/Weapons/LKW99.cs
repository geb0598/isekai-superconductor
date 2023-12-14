using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LKW99 : Weapon
{
    public override IEnumerator Attack()
    {
        yield return new WaitUntil(() => _bulletLauncher.isLaunchReady);
        _bulletLauncher.Launch((Vector2)transform.position + Random.insideUnitCircle);
        yield return new WaitForSeconds(attackDelaySeconds);
        _isDelay = false;
    }

    protected override void Update()
    {
        base.Update();

        if (!_isDelay)
        {
            _isDelay = true;
            StartCoroutine(Attack());
        }
    }
}
