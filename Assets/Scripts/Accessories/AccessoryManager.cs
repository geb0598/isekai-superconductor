using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AccessoryManager : MonoBehaviour
{
    private AccessoryManager _instance;

    [SerializeField] private GameObject[] _accessoryPrefabs;

    private List<Accessory> _accessories;

    public AccessoryManager instance { get => _instance; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Start()
    {
        _accessoryPrefabs.OrderBy(a => a.GetComponent<Accessory>().id);
        _accessories = _accessoryPrefabs.Select(a => a.GetComponent<Accessory>()).ToList();
    }

    public Accessory GetAccessory(int id)
    {
        return _accessories[id];
    }

    public void ActivateAccessory(int id)
    {
        _accessoryPrefabs[id].SetActive(true);
    }
}
