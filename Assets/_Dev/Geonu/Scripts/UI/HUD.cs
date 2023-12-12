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
    public Text activeWeaponCooltimeText;
    public Image activeWeaponCooltimeImage;
    public GridLayoutGroup weaponSlot;
    public GridLayoutGroup accessorySlot;

    public Button escapeStoreButton;

    private Canvas canvas;


    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    // test
    private void Start()
    {
        WeaponManager.instance.ActivateWeapon(0);
    }

    private void OnEnable()
    {
        // initate
        killText.text = GameManager.GetInstance().killCount.ToString();
        coinText.text = PlayerManager.instance.coin.ToString();

        // not yet implemented
        string weaponName = WeaponManager.instance.GetActiveWeapon(WeaponManager.instance.selectedActiveWeaponId).name;
        activeWeaponImage.sprite = Resources.Load<GameObject>("Weapon/" + weaponName).GetComponent<SpriteRenderer>().sprite;
        UpdateLevel();
    }

    private void Update()
    {
        float remainingCooldownTime = WeaponManager.instance.GetActiveWeapon(WeaponManager.instance.selectedActiveWeaponId).remainingCooldownTime;
        if (remainingCooldownTime == 0)
        {
            Color transparentColor = activeWeaponCooltimeImage.color;
            transparentColor.a = 0f;
            activeWeaponCooltimeImage.color = transparentColor;

            return;
        }

        float cooldownTime = WeaponManager.instance.GetActiveWeapon(WeaponManager.instance.selectedActiveWeaponId).attackDelaySeconds;
        Color color = activeWeaponCooltimeImage.color;
        color.a = 0.5f;
        activeWeaponCooltimeImage.color = color;
        activeWeaponCooltimeImage.fillAmount = (cooldownTime - remainingCooldownTime) / cooldownTime;
    }

    // weapon¿¡¼­ event invoke
    public void AddSlot(int type, int id)
    {
        string resourceName;
        GameObject slotPrefab;
        GameObject slot;

        switch (type)
        {
            // weapon(0) or activeWeapon(2).
            case 0:
            case 2:
                string weaponName = WeaponManager.instance.GetWeapon(id).name;
                resourceName = "Weapon/" + weaponName + "Slot";
                slotPrefab = Resources.Load<GameObject>(resourceName);
                slot = Instantiate(slotPrefab);
                slot.transform.SetParent(weaponSlot.transform);
                break;

            // accessory
            case 1:
                string accessoryName = "";
                resourceName = "Accessory/" + accessoryName + "Slot";
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

    public void UpdateLevel()
    {
        expSlider.value = PlayerManager.instance.experiencePoints;
        expSlider.maxValue = PlayerManager.instance.experiencePointsRequired;
        levelText.text = string.Format("Lv. {0}", PlayerManager.instance.level);
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
                healthPoint[PlayerManager.instance.healthPoints - 1].color = Color.gray;
                break;

            case 1:
                healthPoint[PlayerManager.instance.healthPoints - 1].color = Color.white;
                break;
        }
    }

    public void UpdateMaxHealthPoint()
    {
        if (PlayerManager.instance.maxHealthPoints < 1)
            return;
        healthPoint[PlayerManager.instance.maxHealthPoints - 1].color = Color.white;

        if (!(PlayerManager.instance.healthPoints == PlayerManager.instance.maxHealthPoints))
            healthPoint[PlayerManager.instance.maxHealthPoints - 1].color = Color.gray;
    }

    public void UpdateKill()
    {
        killText.text = string.Format("{0}", GameManager.GetInstance().killCount);
    }

    public void UpdateCoin(int amount)
    {
        coinText.text = string.Format("{0}", PlayerManager.instance.coin);
    }

    public void EnableUI()
    {
        gameObject.SetActive(true);
    }

    public void DrawFront()
    {
        canvas.sortingOrder = 0;
    }

    public void SendBack()
    {
        canvas.sortingOrder = -1;
    }

    public void EnableEscapeStoreButton()
    {
        escapeStoreButton.gameObject.SetActive(true);
    }

    public void DisableEscapeStoreButton()
    {
        escapeStoreButton.gameObject.SetActive(false);
    }

    public void OnEscapeStoreButtonClicked()
    {
        GameManager.GetInstance().isStoreEnd = true;
        GameManager.GetInstance().eventManager.waveStartEvent.Invoke();
    }
}
