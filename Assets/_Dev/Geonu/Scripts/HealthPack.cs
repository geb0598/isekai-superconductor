using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : DropItem
{
    public override void Get()
    {
        PlayerManager.instance.RestoreHealthPoints();
        GameManager.GetInstance().eventManager.playerRestoreHealthPointsEvent.Invoke();
        Destroy(gameObject);
    }
}
