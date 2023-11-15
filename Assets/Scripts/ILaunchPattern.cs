using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILaunchPattern  
{
    public void GeneratePattern(List<Vector2> directions, Vector2 direction, int bulletCount);
}


[System.Serializable]
public class DirectLaunchPattern : ILaunchPattern
{
    [Range(0.0f, 180.0f)]
    [SerializeField] private float _precision;

    public void GeneratePattern(List<Vector2> directions, Vector2 direction, int bulletCount)
    {
        directions.Clear();

        for (int i = 0; i < bulletCount; i++)
        {
            float randomAngle = Random.Range(-_precision, _precision);

            directions.Add(Quaternion.Euler(0.0f, 0.0f, randomAngle) * direction);
        } 
    }
}

[System.Serializable]
public class CircularLaunchPattern : ILaunchPattern
{
    [SerializeField] bool _isClockwise;

    public void GeneratePattern(List<Vector2> directions, Vector2 direction, int bulletCount)
    {
        directions.Clear();

        float angle = 360.0f / bulletCount;
        float cumulativeAngle = 0.0f;

        for (int i = 0; i < bulletCount; i++)
        {
            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, cumulativeAngle);
            directions.Add(rotation * direction);

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
