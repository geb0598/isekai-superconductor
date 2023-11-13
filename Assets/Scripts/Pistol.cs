using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] float _range;
    [SerializeField] float _lineWidth;

    private LineRenderer _lineRenderer;

    public Transform _nearestTarget;

    public override float GetDamage(float playerPower)
    {
        return power * playerPower;
    }

    public override IEnumerator Attack()
    {
        Vector2 direction = _nearestTarget.position - transform.position;
        _bulletLauncher.Launch(direction);
        yield return new WaitForSeconds(attackDelaySeconds);
        _isDelay = false;
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = _lineWidth;
        _lineRenderer.endWidth = _lineWidth;
        _bulletLauncher = GetComponent<BulletLauncher>();
        _level = 1;
        _isDelay = false;
    }

    private void Update()
    {
        _nearestTarget = GetNearestTarget();

        DrawLine(_nearestTarget);

        if (_nearestTarget != null && !_isDelay && _bulletLauncher.isLaunchReady)
        {
            _isDelay = true;
            StartCoroutine(Attack());
        }
    }

    private Transform GetNearestTarget()
    {
        RaycastHit2D[] targets = Physics2D.CircleCastAll(transform.position, _range, Vector2.zero, 0, LayerMask.GetMask("Enemy"));

        Transform nearestTarget = null;
        float minDistance = 100;

        foreach (RaycastHit2D target in targets)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTarget = target.transform;
            }
        }

        return nearestTarget;
    }
    
    private void DrawLine(Transform target)
    {
        if (target == null)
        {
            _lineRenderer.positionCount = 0;
            return;
        } 

        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, target.position);
    }

}
