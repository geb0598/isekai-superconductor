using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField] private GameObject[] _launchPoints;
    [SerializeField] private float _launchDelaySeconds;

    [SerializeField] private List<GameObject> _bulletPrefabs;
    [SerializeField] private int _bulletCount;

    public LaunchPatternFactory launchPatternFactory;

    private ILaunchPattern _launchPattern;

    private Transform _launcher;

    private List<Vector2> _targets;

    private bool _isLaunchReady;

    private int _currentBulletIndex;

    public bool isLaunchReady { get => _isLaunchReady; }

    public List<GameObject> bulletPrefabs
    {
        set
        {
            _bulletPrefabs = value;
        }
    } 

    public int bulletCount
    {
        set
        {
            _bulletCount = value;
        }
    }

    public void Launch(Vector2 target)
    {
        if (!_isLaunchReady) { return; }

        _isLaunchReady = false;
        _launcher = transform;
        if (_launchPoints.Length != 0)
        {
            _launcher = _launchPoints[Random.Range(0, _launchPoints.Length)].transform;
        }
        _launchPattern.GeneratePattern(_launcher, _targets, target, _bulletCount);
        StartCoroutine(LaunchBullets());
        _currentBulletIndex++;
        _currentBulletIndex %= _bulletPrefabs.Count;
    }

    private IEnumerator LaunchBullets()
    {
        foreach (Vector2 target in _targets)
        {
            PooledObject bulletPool = _bulletPrefabs[_currentBulletIndex].GetComponent<PooledObject>();
            // GameObject bulletInstance = Instantiate(_bulletPrefabs[_currentBulletIndex]);
            GameObject bulletInstance = bulletPool.GetPooledObject();
            Bullet bullet = bulletInstance.GetComponent<Bullet>();
            if (gameObject.CompareTag("Weapon"))
            {
                Weapon weapon = GetComponent<Weapon>();
                bullet.Initialize(_launcher, target, true, weapon.GetDamage());
            }
            else if (gameObject.CompareTag("Bullet"))
            {
                Bullet bulletParent = GetComponent<Bullet>();
                bullet.Initialize(_launcher, target, true, bulletParent.damage);
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                bullet.Initialize(_launcher, target, false, 0);
            }

            if (_launchDelaySeconds != 0)
            {
                yield return new WaitForSeconds(_launchDelaySeconds);
            }
        }
        _isLaunchReady = true;
    }

    private void Awake()
    {
        _launchPattern = launchPatternFactory.CreateLaunchPattern();
        _targets = new List<Vector2>();
        _isLaunchReady = true;
        _currentBulletIndex = 0;
    }
}

[System.Serializable]
public class LaunchPatternFactory
{
    public enum LaunchPatternType
    {
        DirectLaunchPattern,
        CircularLaunchPattern,
        RandomLaunchPattern
    }
    
    public LaunchPatternType Type = LaunchPatternType.DirectLaunchPattern;

    public DirectLaunchPattern DirectLaunchPattern = new DirectLaunchPattern();
    public CircularLaunchPattern CircularLaunchPattern = new CircularLaunchPattern();
    public RandomLaunchPattern RandomLaunchPattern = new RandomLaunchPattern();

    public ILaunchPattern CreateLaunchPattern()
    {
        return GetLaunchPatternFromType(Type);
    }

    public System.Type GetClassType(LaunchPatternType type)
    {
        return GetLaunchPatternFromType(type).GetType();
    }

    private ILaunchPattern GetLaunchPatternFromType(LaunchPatternType type)
    {
        switch (type)
        {
            case LaunchPatternType.DirectLaunchPattern:
                return DirectLaunchPattern;
            case LaunchPatternType.CircularLaunchPattern:
                return CircularLaunchPattern;
            case LaunchPatternType.RandomLaunchPattern:
                return RandomLaunchPattern;
            default:
                return DirectLaunchPattern;
        }
    }
}
