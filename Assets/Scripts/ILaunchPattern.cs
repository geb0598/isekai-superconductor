using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILaunchPattern  
{
    public void GeneratePattern(Transform launcher, List<Vector2> targets, Vector2 target, int bulletCount);
}

[System.Serializable]
public class DirectLaunchPattern : ILaunchPattern
{
    [Range(0.0f, 180.0f)]
    [SerializeField] private float _precision;

    public void GeneratePattern(Transform launcher, List<Vector2> targets, Vector2 target, int bulletCount)
    {
        targets.Clear();

        for (int i = 0; i < bulletCount; i++)
        {
            float randomAngle = Random.Range(-_precision, _precision);

            Vector2 direction = target - (Vector2)launcher.position;
            direction = Quaternion.Euler(0, 0, randomAngle) * direction;

            targets.Add((Vector2)launcher.position + direction);
        } 
    }
}

[System.Serializable]
public class CircularLaunchPattern : ILaunchPattern
{
    [SerializeField] bool _isClockwise;

    public void GeneratePattern(Transform launcher, List<Vector2> targets, Vector2 target, int bulletCount)
    {
        targets.Clear();

        float angle = 360.0f / bulletCount;
        float cumulativeAngle = 0.0f;

        for (int i = 0; i < bulletCount; i++)
        {
            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, cumulativeAngle);

            Vector2 direction = target - (Vector2)launcher.position;
            direction = rotation * direction;

            targets.Add((Vector2)launcher.position + direction);

            if (_isClockwise)
            {
                cumulativeAngle += angle;
            }
            else
            {
                cumulativeAngle -= angle;
            }
        }
    }
}

[System.Serializable]
public class RandomLaunchPattern : ILaunchPattern
{
    [SerializeField] float _range;

    public void GeneratePattern(Transform launcher, List<Vector2> directions, Vector2 direction, int bulletCount)
    {
        directions.Clear();

        for (int i = 0; i < bulletCount; i++)
        {
            directions.Add(direction + _range * Random.insideUnitCircle);
        }
    }
}
