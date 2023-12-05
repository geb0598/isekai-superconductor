using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPattern", menuName = "ScriptableObject/SpawnPattern")]
public class SpawnPattern : ScriptableObject
{
    public List<KeyValuePair<int, int>> spawnPointsGroup;
    public List<int> spawnCounts;

    List<Transform> spawnPoints;
}
