using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    [Header("GameManager")]
    public UnityEvent<int, int> gameTimerEvent;

    [Header("PlayerManager")]
    public UnityEvent<int, int> playerLevelUpEvent;
    public UnityEvent<int> playerGetExpEvent;
    public UnityEvent<int> playerGetCoinEvent;

    [Header("Enemy")]
    public UnityEvent enemyKilledEvent;

    [Header("StoreItem")]
    public UnityEvent<int> storeItemApproachEvent;
    public UnityEvent storeItemLeaveEvent;

    [Header("WeaponManager")]
    public UnityEvent<int, int> takeNewWeapon;
}
