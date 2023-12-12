using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory : MonoBehaviour
{
    [SerializeField] private int _id;

    public int id { get => _id; }
}
