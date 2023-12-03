using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private static WeaponManager _instance;

    [SerializeField] private GameObject[] _weaponPrefabs;
    [SerializeField] private GameObject _activeWeaponPrefab;

    private List<Weapon> _weapons;

    public static WeaponManager instance { get => _instance; }

    public Weapon activeWeapon { get => _activeWeaponPrefab.GetComponent<Weapon>(); }

    private void Start()
    {
        _instance = this;
        _weaponPrefabs.OrderBy(w => w.GetComponent<Weapon>().id);
        _weapons = _weaponPrefabs.Select(w => w.GetComponent<Weapon>()).ToList();
    }

    private void ActivateWeapon(int id)
    {
        _weaponPrefabs[id].SetActive(true);
    }
}
