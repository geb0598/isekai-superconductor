using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : DropItem
{
    public int amount;

    public void Init(int amount)
    {
        this.amount = amount;
    }

    protected override void Get()
    {
        // playerManager GetExp(amount) call
        // GameManager.GetInstance().playerManager.GetExp(amount);
    }
}
