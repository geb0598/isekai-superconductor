using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public Text healthPointText;
    public Text levelText;
    public Text powerText;

    public GridLayoutGroup weaponSlot;
    public List<GameObject> weaponLevels;
    public MiniLevels[] weaponLevelsImage;

    private List<int> _weaponsID;
    private List<int> _weaponLevelsArray;
    private int _weaponCount;

    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        healthPointText.text = string.Format("HP : {0} / {1}", PlayerManager.instance.healthPoints, PlayerManager.instance.maxHealthPoints);
        levelText.text = string.Format("Level : {0} / {1}", PlayerManager.instance.level, PlayerManager.instance.levelLimit);
        powerText.text = string.Format("Power : {0}", PlayerManager.instance.power);
    }

    public void WeaponLevelUp(int id)
    {
        int weaponIndex = _weaponsID.IndexOf(id);

        if (weaponIndex == -1)
        {
            Debug.Log("Error");
            return;
        }

        weaponLevelsImage[weaponIndex].MiniLevel[_weaponLevelsArray[weaponIndex]].color = Color.black;

        _weaponLevelsArray[weaponIndex]++;
    }

    public void AddSlot(int type, int id)
    {
        GameObject slotPrefab;
        GameObject slot;

        string weaponName = WeaponManager.instance.GetWeapon(id).name;

        string resourceName = "Weapon/" + weaponName + "Slot";
        slotPrefab = Resources.Load<GameObject>(resourceName);

        slot = Instantiate(slotPrefab);
        slot.transform.SetParent(weaponSlot.transform);

        if (_weaponsID == null)
            _weaponsID = new List<int>();

        if (_weaponLevelsArray == null)
            _weaponLevelsArray = new List<int>();

        _weaponsID.Add(id);
        _weaponLevelsArray.Add(0);
        WeaponLevelUp(id);

        weaponLevels[_weaponCount].SetActive(true);
        _weaponCount++;
    }

    public void EnablePauseUI()
    {
        gameObject.SetActive(true);
        TransparentUI();
    }

    public void TransparentUI()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvas.sortingOrder = -1;
    }

    public void UndoTransparentUI()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvas.sortingOrder = 0;
    }
}


[System.Serializable]
public class MiniLevels
{
    public Image[] MiniLevel;
}