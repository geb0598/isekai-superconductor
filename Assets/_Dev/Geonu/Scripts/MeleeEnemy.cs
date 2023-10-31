using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    private void FixedUpdate()
    {
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
