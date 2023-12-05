using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Slider expSlider;
    public Image[] healthPoint;
    public Text timer;
    public Text killText;
    public Text coinText;
    public Text levelText;
    public Image activeWeaponImage;
    public GridLayoutGroup weaponSlot;
    public GridLayoutGroup accessorySlot;

    private int _curHealthPoint;
    private int _maxHealthPoint;

    private int _killCount;
    private int _coinCount;
    private int _levelCount;

    private void OnEnable()
    {
        // Read From GameManager
        _killCount = 0;
        _coinCount = 0;
        _levelCount = 0;

        // initate
        killText.text = _killCount.ToString();
        coinText.text = _coinCount.ToString();
        levelText.text = string.Format("Lv.{0}", _levelCount);

        // not yet implemented
        // activeWeaponImage.sprite = Resources.Load<Sprite>("\ActiveItem" + WeaponManager.instance.GetWeapon(id).name);

        UpdateLevel(PlayerManager.instance.experiencePoints, PlayerManager.instance.experiencePointsRequired);
    }

    // weapon���� event invoke
    public void AddSlot(int type, int id)
    {
        string resourceName;
        GameObject slotPrefab;
        GameObject slot;

        string equipName = PauseUI.GetEquipmentName(type, id);

        switch (type)
        {            
            // weapon
            case 0:
                resourceName = "Weapon/" + equipName + "Slot";
                slotPrefab = Resources.Load<GameObject>(resourceName);
                slot = Instantiate(slotPrefab);
                slot.transform.SetParent(weaponSlot.transform);
                break;

            // accessory
            case 1:
                resourceName = "Accessory/" + equipName + "Slot";
                slotPrefab = Resources.Load<GameObject>(resourceName);
                slot = Instantiate(slotPrefab);
                slot.transform.SetParent(accessorySlot.transform);
                break;
        }
    }

    public void UpdateExpSlider(int amount)
    {
        expSlider.value += amount;
    }

    public void UpdateLevel(int curExp, int maxExp)
    {
        expSlider.value = curExp;
        expSlider.maxValue = maxExp;
        _levelCount++;
        levelText.text = string.Format("Lv.{0}", _levelCount);
    }

    public void UpdateTimer(int minute, int second)
    {
        timer.text = string.Format("{0:D2}:{1:D2}", minute, second);
    }

    public void UpdateHealthPoint(int amount)
    {
        switch (amount)
        {
            case -1:
                _curHealthPoint--;
                healthPoint[_curHealthPoint].color = Color.gray;
                break;

            case 1:
                healthPoint[_curHealthPoint].color = Color.white;
                _curHealthPoint++;
                break;
        }
    }

    public void UpdateMaxHealthPoint()
    {
        healthPoint[_maxHealthPoint].color = Color.white;
        _maxHealthPoint++;
    }

    public void UpdateKill()
    {
        _killCount++;
        killText.text = string.Format("{0}", _killCount);
    }

    public void UpdateCoin(int amount)
    {
        _coinCount += amount;
        coinText.text = string.Format("{0}", _coinCount);
    }

    public void EnableUI()
    {
        gameObject.SetActive(true);
    }

    public void DisableUI()
    {
        gameObject.SetActive(false);
    }
}