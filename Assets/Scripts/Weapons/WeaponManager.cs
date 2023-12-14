using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private static WeaponManager _instance;

    [SerializeField] private GameObject[] _weaponPrefabs;
    [SerializeField] private GameObject[] _activeWeaponPrefabs;

    private List<Weapon> _weapons;
    private List<ActiveWeapon> _activeWeapons;

    public int selectedActiveWeaponId = 0;

    public static WeaponManager instance { get => _instance; }

    public Weapon activeWeapon { get => _activeWeapons[selectedActiveWeaponId]; }

    private void Awake()
    {
        _instance = this;
        _weaponPrefabs = _weaponPrefabs.OrderBy(w => w.GetComponent<Weapon>().id).ToArray();
        _weapons = _weaponPrefabs.Select(w => w.GetComponent<Weapon>()).ToList();
        _activeWeaponPrefabs = _activeWeaponPrefabs.OrderBy(w => w.GetComponent<Weapon>().id).ToArray();
        _activeWeapons = _activeWeaponPrefabs.Select(w => w.GetComponent<ActiveWeapon>()).ToList();
    }

    public Weapon GetWeapon(int id)
    {
        return _weapons[id];
    }

    public GameObject GetWeaponPrefab(int id)
    {
        return _weaponPrefabs[id];
    }

    public Weapon GetActiveWeapon(int id)
    {
        return _activeWeapons[id];
    }

    public void ActivateWeapon(int id)
    {
        _weaponPrefabs[id].SetActive(true);
        GameManager.GetInstance().eventManager.addNewWeaponEvent.Invoke(0, id); // event invoke for ui
    }

    public void ActivateActiveWeapon()
    {
        _activeWeaponPrefabs[selectedActiveWeaponId].SetActive(true);
    }

    public void SelectActiveWeapon(int id)
    {
        selectedActiveWeaponId = id;
    }

    public void LevelUp(int id)
    {
        // Not yet implemented
        Debug.Log("Level up");
    }

    private void Upgrade(int id)
    {
        // Not yet implemented
        Debug.Log("Upgrade");
    }
}
