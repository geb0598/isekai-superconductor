using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStat", menuName = "ScriptableObject/EnemyStat")]
public class EnemyStat : ScriptableObject
{
    public float maxHp;
    public float speed;
    public float knockBackCoefficient;
    public float attackSpeed;
    public float bulletSpeed;
}
