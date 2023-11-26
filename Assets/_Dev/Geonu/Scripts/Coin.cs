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

    protected override void Get()
    {
        // playerManager GetCoin(amount) call
        // PlayerManager.instance.AddCoin();
        GameManager.GetInstance().eventManager.playerGetCoinEvent.Invoke(_amount); // called by playerManager.AddExperiencePoints
    }
}
