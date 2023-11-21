using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    public FinderFactory finderFactory;
    private IFinder _finder;

    public List<Vector2> FindTargets(Vector2 position, LayerMask layer, List<GameObject> filteringObjects)
    {
        return _finder.GetTargets(position, layer, filteringObjects);
    }

    private void Awake()
    {
        _finder = finderFactory.CreateFinder();
    }
}

[System.Serializable]
public class FinderFactory
{
    public enum FinderType
    {
        Finder,
        RangedFinder,
        NearestFinder,
        RandomFinder,
        RandomPositionFinder
    }

    public FinderType Type = FinderType.RandomFinder;

    public Finder Finder = new Finder();
    public RangedFinder RangedFinder = new RangedFinder();
    public NearestFinder NearestFinder = new NearestFinder();
    public RandomFinder RandomFinder = new RandomFinder();
    public RandomPositionFinder RandomPositionFinder = new RandomPositionFinder();

    public IFinder CreateFinder()
    {
        return GetFinderFromType(Type);
    }

    public System.Type GetClassType(FinderType type)
    {
        return GetFinderFromType(type).GetType();
    }

    public IFinder GetFinderFromType(FinderType type)
    {
        switch (type)
        {
            case FinderType.Finder:
                return Finder;
            case FinderType.RangedFinder:
                return RangedFinder;
            case FinderType.NearestFinder:
                return NearestFinder;
            case FinderType.RandomFinder:
                return RandomFinder;
            case FinderType.RandomPositionFinder:
                return RandomPositionFinder;
            default:
                return Finder;
        }
    }
}
