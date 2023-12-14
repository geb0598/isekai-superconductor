using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField] private GameObject[] _launchPoints;
    [SerializeField] private float _launchDelaySeconds;

    public List<GameObject> bulletPrefabs;
    public int bulletCount;

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
        _launchPattern.GeneratePattern(transform, _targets, target, bulletCount);
        StartCoroutine(LaunchBullets());
        _currentBulletIndex++;
        _currentBulletIndex %= bulletPrefabs.Count;
    }

    private IEnumerator LaunchBullets()
    {
        // select one of the launch points if not null
        // not yet implemented
        foreach (Vector2 target in _targets)
        {
            GameObject bulletInstance = GameManager.GetInstance().poolManager.Get(3, _bulletPrefabs[_currentBulletIndex].GetComponent<Bullet>().id);
            // GameObject bulletInstance = Instantiate(_bulletPrefabs[_currentBulletIndex]);
            Bullet bullet = bulletInstance.GetComponent<Bullet>();
            if (gameObject.CompareTag("Weapon"))
            {
                Weapon weapon = GetComponent<Weapon>();
                bullet.Initialize(transform, target, true, weapon.GetDamage());
            }
            else if (gameObject.CompareTag("Bullet"))
            {
                Bullet bulletParent = GetComponent<Bullet>();
                bullet.Initialize(transform, target, true, bulletParent.damage);
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                bullet.Initialize(transform, target, false, 0);
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
