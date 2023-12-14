using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPoints", menuName = "ScriptableObject/SpawnPoints")]
public class SpawnPoints : ScriptableObject
{
    public int[] spawnPointsIndices;
    public int[] spawnCounts;
}
