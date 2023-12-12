using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : DropItem
{
    private int _amount;

    public void Init(int amount)
    {
        _amount = amount;
    }

    public override void Get()
    {
        PlayerManager.instance.AddCoin(_amount); 
        gameObject.SetActive(false);
        GameManager.GetInstance().eventManager.playerTakeCoinEvent.Invoke(_amount); // called by playerManager.AddExperiencePoints
    }
}
