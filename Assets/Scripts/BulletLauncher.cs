using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bulletPrefabs;
    [SerializeField] private int _bulletCount;

    [SerializeField] private bool _hasLaunchDelay = false;
    [SerializeField] private float _launchDelaySeconds;

    public LaunchPatternFactory launchPatternFactory;

    private ILaunchPattern _launchPattern;

    private List<Vector2> _directions;

    private int _currentBulletIndex;

    private bool _isLaunchReady;

    public List<Vector2> directions { get => _directions; }

    public bool isLaunchReady { get => _isLaunchReady; }

    public void Launch(Vector2 direction)
    {
        if (!_isLaunchReady)
        {
            return;
        }

        _isLaunchReady = false;
        _launchPattern.GeneratePattern(_directions, direction, _bulletCount);
        if (_hasLaunchDelay)
        {
            StartCoroutine(LaunchBulletsWithDelay());
        } else
        {
            LaunchBullets();
        }
    }

    private void LaunchBullets()
    {
        foreach (Vector2 direction in _directions)
        {
            GameObject bulletInstance = Instantiate(_bulletPrefabs[_currentBulletIndex]);
            Bullet bulletComponent = bulletInstance.GetComponent<Bullet>();
            if (gameObject.layer == LayerMask.NameToLayer("Weapon"))
            {
                Weapon weaponComponent = GetComponent<Weapon>();
                bulletComponent.Initialize(transform, direction, true, weaponComponent.GetDamage(PlayerManager.instance.power));
            }
            else
            {
                bulletComponent.Initialize(transform, direction, false, 0);
            }
        }
        _isLaunchReady = true;
    }

    private IEnumerator LaunchBulletsWithDelay()
    {
        foreach (Vector2 direction in _directions)
        {
            GameObject bulletInstance = Instantiate(_bulletPrefabs[_currentBulletIndex]);
            Bullet bulletComponent = bulletInstance.GetComponent<Bullet>();
            if (gameObject.layer == LayerMask.NameToLayer("Weapon"))
            {
                Weapon weaponComponent = GetComponent<Weapon>();
                bulletComponent.Initialize(transform, direction, true, weaponComponent.GetDamage(PlayerManager.instance.power));
            }
            else
            {
                bulletComponent.Initialize(transform, direction, false, 0);
            }

            yield return new WaitForSeconds(_launchDelaySeconds);
        }
        _isLaunchReady = true;
    }

    private void Awake()
    {
        _launchPattern = launchPatternFactory.CreateLaunchPattern();
        _directions = new List<Vector2>();
        _currentBulletIndex = 0;
        _isLaunchReady = true;
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
