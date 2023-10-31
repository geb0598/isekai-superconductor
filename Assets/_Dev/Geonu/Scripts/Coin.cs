using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : DropItem
{
    public int amount;

    public void Init(int amount)
    {
        this.amount = amount;
    }
}
