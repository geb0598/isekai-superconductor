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

    private List<Vector2> _directions;

    private int _currentBulletIndex;

    private bool _isLaunchReady;

    public bool isLaunchReady { get => _isLaunchReady; }

    public void Launch(Vector2 direction)
    {
        if (!_isLaunchReady)
        {
            return;
        }

        _isLaunchReady = false;
        _launchPattern.GeneratePattern(_directions, direction, _bulletCount);
        StartCoroutine(LaunchBulletsWithDelay());
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
                bulletComponent.Initialize(transform.position, direction, true, weaponComponent.GetDamage(PlayerManager.instance.power));
            }
            else
            {
                bulletComponent.Initialize(transform.position, direction, false, 0);
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
        CircularLaunchPattern
    }
    
    public LaunchPatternType Type = LaunchPatternType.DirectLaunchPattern;

    public DirectLaunchPattern DirectLaunchPattern = new DirectLaunchPattern();
    public CircularLaunchPattern CircularLaunchPattern = new CircularLaunchPattern();

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
            default:
                return DirectLaunchPattern;
        }
    }
}
