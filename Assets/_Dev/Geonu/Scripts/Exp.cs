using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : DropItem
{
    private int _amount;

    public void Init(int amount)
    {
        _amount = amount;
    }

    protected override void Get()
    {
        // playerManager GetExp(amount) call
        for (int i = 0; i < _amount; i++)
            PlayerManager.instance.AddExperiencePoints();
        GameManager.GetInstance().eventManager.playerGetExpEvent.Invoke(_amount); // called by playerManager.AddExperiencePoints
    }
}
