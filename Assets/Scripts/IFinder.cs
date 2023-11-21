using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public interface IFinder  
{
    public List<Vector2> GetTargets(Vector2 position, LayerMask layer, List<GameObject> filteringObjects);
}

[System.Serializable]
public class Finder : IFinder
{
    public List<Vector2> GetTargets(Vector2 position, LayerMask layer, List<GameObject> filteringObjects)
    {
        List<Vector2> targets = new List<Vector2>() { position };
        return targets;
    }
}

[System.Serializable]
public class RangedFinder : IFinder
{
    [SerializeField] protected float _range;

    public virtual List<Vector2> GetTargets(Vector2 position, LayerMask layer, List<GameObject> filteringObjects)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(position, _range, Vector2.zero, 0, layer);
        List<Vector2> targets = hits.Select(hit => (Vector2)hit.transform.position).ToList();
        List<Vector2> filteringTargets = filteringObjects.Select(obj => (Vector2)obj.transform.position).ToList();
        return targets.Except(filteringTargets).ToList();
    }
}

[System.Serializable]
public class NearestFinder : RangedFinder
{
    [SerializeField] private int _targetCount;

    public override List<Vector2> GetTargets(Vector2 position, LayerMask layer, List<GameObject> filteringObjects)
    {
        List<Vector2> targets = base.GetTargets(position, layer, filteringObjects);
        targets.Sort((pos1, pos2) =>
            Vector2.Distance(position, pos1).CompareTo(Vector2.Distance(position, pos2))
        );
        return targets.Take(_targetCount).ToList();
    }
}

[System.Serializable]
public class RandomFinder : RangedFinder
{
    [SerializeField] private int _targetCount;

    public override List<Vector2> GetTargets(Vector2 position, LayerMask layer, List<GameObject> filteringObjects)
    {
        List<Vector2> targets = base.GetTargets(position, layer, filteringObjects);
        return targets.OrderBy(x => Guid.NewGuid()).Take(_targetCount).ToList();
    }
}

[System.Serializable]
public class RandomPositionFinder : RangedFinder
{
    [SerializeField] private int _targetCount;

    public override List<Vector2> GetTargets(Vector2 position, LayerMask layer, List<GameObject> filteringObjects)
    {
        List<Vector2> targets = new List<Vector2>();
        for (int i = 0; i < _targetCount; i++)
        {
            targets.Add(position + _range * UnityEngine.Random.insideUnitCircle);
        }
        return targets;
    }
}

