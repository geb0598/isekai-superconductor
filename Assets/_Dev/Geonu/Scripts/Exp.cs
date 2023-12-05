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

    public override void Get()
    {
        for (int i = 0; i < _amount; i++)
            PlayerManager.instance.AddExperiencePoints();
        gameObject.SetActive(false);
        GameManager.GetInstance().eventManager.playerGetExpEvent.Invoke(_amount); // called by playerManager.AddExperiencePoints
    }
}
