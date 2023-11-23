using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    private float timer; // test

    override protected void FixedUpdate()
    {
        base.FixedUpdate();

        timer += Time.fixedDeltaTime; // test

        if (!_isLive)
            return;

        SpriterFlipX();
        Move();
    }

    private void OnEnable()
    {
        _isLive = true;
    }
}
