using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningChain : MonoBehaviour
{
    private Bullet _bullet;
    private LightningBoltScript _lightningBolt;

    private void Awake()
    {
        _bullet = GetComponent<Bullet>();
        _lightningBolt = GetComponentInChildren<LightningBoltScript>();
        _lightningBolt.StartPosition = _bullet.launcher.position;
        _lightningBolt.EndPosition = _bullet.target;
    }
}
