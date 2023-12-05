using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bulletPrefabs;
    [SerializeField] private int _bulletCount;

    [SerializeField] private float _launchDelaySeconds;

    public LaunchPatternFactory launchPatternFactory;
    private ILaunchPattern _launchPattern;

    private List<Vector2> _targets;

    private bool _isLaunchReady;

    private int _currentBulletIndex;

    public bool isLaunchReady { get => _isLaunchReady; }

    public void Launch(Vector2 target)
    {
        if (!_isLaunchReady) { return; }

        _isLaunchReady = false;
        _launchPattern.GeneratePattern(transform, _targets, target, _bulletCount);
        if (_launchDelaySeconds == 0)
        {
            LaunchBullets();
        } 
        else
        {
            StartCoroutine(LaunchBulletsWithDelay());
        }
        _currentBulletIndex++;
        _currentBulletIndex %= _bulletPrefabs.Count;
    }

    private void LaunchBullets()
    {
        foreach (Vector2 target in _targets)
        {
            GameObject bulletInstance = Instantiate(_bulletPrefabs[_currentBulletIndex]);
            Bullet bullet = bulletInstance.GetComponent<Bullet>();
            if (gameObject.CompareTag("Weapon"))
            {
                Weapon weapon = GetComponent<Weapon>();
                bullet.Initialize(transform, target, true, weapon.GetDamage(PlayerManager.instance.power));
            }
            else
            {
                bullet.Initialize(transform, target, false, 0);
            }
        }
        _isLaunchReady = true;
    }

    private IEnumerator LaunchBulletsWithDelay()
    {
        foreach (Vector2 target in _targets)
        {
            GameObject bulletInstance = Instantiate(_bulletPrefabs[_currentBulletIndex]);
            Bullet bullet = bulletInstance.GetComponent<Bullet>();
            if (gameObject.CompareTag("Weapon"))
            {
                Weapon weapon = GetComponent<Weapon>();
                bullet.Initialize(transform, target, true, weapon.GetDamage(PlayerManager.instance.power));
            }
            else
            {
                bullet.Initialize(transform, target, false, 0);
            }
            yield return new WaitForSeconds(_launchDelaySeconds);
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
