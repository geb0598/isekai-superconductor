using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public PoolManager poolManager;
    public EventManager eventManager;
    public AudioManager audioManager;
    public PlayerController playerController;

    public float timer;

    public bool isShowDamage;
    public bool isClear;

    [Header("WaveManagement")]
    public int wave;
    public int subWave;

    public bool isStoreEnd;

    private float _waveCheckTimer;
    private bool _isInProgress;

    [Header("HUD")]
    public int killCount;

    private void Awake()
    {
        _instance = this;
        StopGame();
    }

    private GameManager() { }

    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameManager();
        }

        return _instance;
    }

    private void Update()
    {
        if (!_isInProgress)
        {
            _waveCheckTimer -= Time.deltaTime;
            eventManager.gameTimerEvent.Invoke((int)_waveCheckTimer / 60, (int)_waveCheckTimer % 60);

            if (_waveCheckTimer <= 0f  || isStoreEnd )
            {
                _waveCheckTimer = 0f;
                _isInProgress = true;
                isStoreEnd = false;
                eventManager.waveStartEvent.Invoke();
            }
            return;
        }

        timer += Time.deltaTime;
        _waveCheckTimer += Time.deltaTime;
        eventManager.gameTimerEvent.Invoke((int)timer / 60, (int)timer % 60);

        if (timer >= subWave * 60f)
        {
            subWave++;
        }

        if (_waveCheckTimer >= 180f)
        {
            wave++;
            _isInProgress = false;
            _waveCheckTimer = 60f;
            eventManager.waveEndEvent.Invoke();
        }
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void ResetGame()
    {
        timer = 0f;
    }

    public void SetIsShowDamage(bool isShowDamage)
    {
        this.isShowDamage = isShowDamage;
    }

    public void AddKillCount()
    {
        killCount++;
    }
}
