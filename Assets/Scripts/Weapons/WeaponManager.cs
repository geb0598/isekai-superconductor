using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _weaponPrefabs;

    private List<Weapon> _weapons;

    private void Start()
    {
        _weaponPrefabs.OrderBy(w => w.GetComponent<Weapon>().id);
        _weapons = _weaponPrefabs.Select(w => w.GetComponent<Weapon>()).ToList();
    }

    private void ActivateWeapon(int id)
    {
        _weaponPrefabs[id].SetActive(true);
    }
}
