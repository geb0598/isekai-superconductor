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
    public List<GameObject> weaponLevel;

    private List<Image[]> weaponLevels;
    private int weaponLevelsIndex;

    private void Start()
    {
        weaponLevels = new List<Image[]>();
        for (int i = 0; i < 6; i++)
            weaponLevels.Add(weaponLevel[i].GetComponentsInChildren<Image>());
    }

    private void Update()
    {
        healthPointText.text = string.Format("HP : {0} / {1}", PlayerManager.instance.healthPoints, PlayerManager.instance.maxHealthPoints);
        levelText.text = string.Format("Level : {0} / {1}", PlayerManager.instance.level, PlayerManager.instance.levelLimit);
        powerText.text = string.Format("Power : {0}", PlayerManager.instance.power);
    }

    public void WeaponLevelEnable()
    {
        for (int i = 0; i < 8; i++)
            weaponLevels[weaponLevelsIndex][i].gameObject.SetActive(true);
        weaponLevelsIndex++;
    }

    public void AddSlot(int type, string weaponName)
    {
        GameObject slotPrefab;
        GameObject slot;

        string resourceName = "Weapon/" + weaponName + "Slot";
        slotPrefab = Resources.Load<GameObject>(resourceName);

        slot = Instantiate(slotPrefab);
        slot.transform.SetParent(weaponSlot.transform);
    }

    public void EnablePauseUI()
    {
        gameObject.SetActive(true);
    }

    public void DisablePauseUI()
    {
        gameObject.SetActive(false);
    }
}
