using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LKW_99 : Weapon
{
    private void Awake()
    {
        _bulletLauncher = GetComponent<BulletLauncher>();
        _level = 1;
        _isDelay = false;
    }

    private void Start()
    {
        StartCoroutine(Attack());
    }

    public override IEnumerator Attack()
    {
        _bulletLauncher.Launch(new Vector2(0, 1));
        yield return null;
    }

    public override float GetDamage(float playerPower)
    {
        return 0;
    }
}
