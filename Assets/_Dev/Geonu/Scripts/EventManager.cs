using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    [Header("GameManager")]
    public UnityEvent<int, int> gameTimerEvent;
    public UnityEvent waveStartEvent;
    public UnityEvent waveEndEvent;
    public UnityEvent subWaveIncreaseEvent;

    [Header("PlayerManager")]
    public UnityEvent playerLevelUpEvent;
    public UnityEvent playerTakeDamageEvent;
    public UnityEvent playerRestoreHealthPointsEvent;
    public UnityEvent playerAddExtraHealthPointEvent;
    public UnityEvent<int> playerTakeExpEvent;
    public UnityEvent<int> playerTakeCoinEvent;

    [Header("Enemy")]
    public UnityEvent enemyKilledEvent;

    [Header("StoreItem")]
    public UnityEvent<int> storeItemPurchaseEvent;

    [Header("WeaponManager")]
    public UnityEvent<int, int> addNewWeaponEvent; // <type, id>
}
