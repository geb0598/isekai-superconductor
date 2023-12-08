using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LKW99 : Weapon
{
    private void Start()
    {
        StartCoroutine(Attack());
    }

    public override IEnumerator Attack()
    {
        _bulletLauncher.Launch(new Vector2(0, 1));
        yield return null;
    }
}
