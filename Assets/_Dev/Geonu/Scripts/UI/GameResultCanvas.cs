using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResultCanvas : MonoBehaviour
{
    [Header("GameManager and PlayerManager")]
    public Text gameResultText;
    public Text level;
    public Text wave;
    public Text time;
    public Text coin;
    public Text kill;

    [Header("Weapons")]
    public GridLayoutGroup[] slots;
    public Text[] weaponLevels;
    public Image activeWeaponImage;


    private List<int> _weaponsId;
    private int _weaponCount;

    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        _weaponsId = new List<int>();

        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    private void UpdateCanvas()
    {
        UndoTransparentCanvas();

        gameResultText.text = (GameManager.GetInstance().isClear) ? "Clear" : "Fail";
        level.text = string.Format("Level: {0} / {1}", PlayerManager.instance.level, PlayerManager.instance.levelLimit);
        wave.text = string.Format("Wave: {0}", GameManager.GetInstance().wave);
        time.text = string.Format("Time: {0:D2}:{1:D2}", Mathf.FloorToInt(GameManager.GetInstance().timer / 60), Mathf.FloorToInt(GameManager.GetInstance().timer % 60));
        coin.text = string.Format("Coin: {0}", PlayerManager.instance.coin);
        kill.text = string.Format("Kill: {0}", GameManager.GetInstance().killCount);

        // Weapons init not yet implemented
        for (int i = 0; i < _weaponCount; i++)
        {
            weaponLevels[i].text = WeaponManager.instance.GetWeapon(_weaponsId[i]).level.ToString();
        }
        string activeWeaponName = WeaponManager.instance.GetActiveWeapon(WeaponManager.instance.selectedActiveWeaponId).name;
        activeWeaponImage.sprite = Resources.Load<GameObject>("Weapon/" + activeWeaponName).GetComponent<SpriteRenderer>().sprite;
    }

    public void AddSlot(int type, int id)
    {
        string resourceName = "";
        switch(type)
        {
            case 0: // weapon
                string weaponName = WeaponManager.instance.GetWeapon(id).name;
                resourceName = "Weapon/" + weaponName + "Slot";
                _weaponsId.Add(id);
                _weaponCount++;
                break;

            case 1: // accessory
                string accessoryName = ""; // AccessoryManager.instance.GetAccessory(id).name;
                resourceName = "Accessory/" + accessoryName + "Slot";
                break;

            default:
                break;
        }

        GameObject slot = Instantiate(Resources.Load<GameObject>(resourceName));
        slot.transform.SetParent(slots[type].transform);
    }

    public void EnableCanvas()
    {
        gameObject.SetActive(true);
        TransparentCanvas();
    }

    public void TransparentCanvas()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvas.sortingOrder = -1;
    }

    public void DrawFront()
    {
        canvas.sortingOrder = 0;
    }

    public void SendBack()
    {
        canvas.sortingOrder = -1;
    }

    public void UndoTransparentCanvas()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvas.sortingOrder = 0;
    }
}
