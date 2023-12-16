using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyGroup", menuName = "ScriptableObject/EnemyGroup")]
public class EnemyGroup : ScriptableObject
{
    public Enemy[] enemyPrefabs;
    public float[] probabilities; // sum of probabilities should be 1.
}

