using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : Weapon
{
    [SerializeField] GameObject _dangerZonePrefab;

    private void Awake()
    {
        _bulletLauncher = GetComponent<BulletLauncher>();
        _level = 1;
        _isDelay = false;
    }

    public override float GetDamage(float playerPower)
    {
        return power * playerPower;
    }

    public override IEnumerator Attack()
    {
        _bulletLauncher.Launch(transform.position);
        foreach (Vector2 position in _bulletLauncher.directions)
        {
            StartCoroutine(GenerateDanzerZone(position));
        }
        yield return new WaitForSeconds(attackDelaySeconds);
        _isDelay = false;
    }

    private void Update()
    {
        if (!_isDelay && _bulletLauncher.isLaunchReady)
        {
            _isDelay = true;
            StartCoroutine(Attack());
        }
    }

    private IEnumerator GenerateDanzerZone(Vector2 position)
    {
        GameObject dangerZoneInstance = Instantiate(_dangerZonePrefab);
        dangerZoneInstance.gameObject.transform.position = position;
        yield return new WaitForSeconds(5.0f);
        Destroy(dangerZoneInstance);
    }
}
