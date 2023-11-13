using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingBullet : Bullet
{
    [SerializeField] float _moveSpeed;
    [SerializeField] bool _isClockwise;
    protected override void UpdateBulletTransform()
    {
        // transform.RotateAround(_position, Vector3.up, _moveSpeed * Time.fixedDeltaTime);
    }
}
