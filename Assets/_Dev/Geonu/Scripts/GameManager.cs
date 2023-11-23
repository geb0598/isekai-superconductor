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
        timer += Time.deltaTime;
        eventManager.gameTimerEvent.Invoke((int)timer / 60, (int)timer % 60);
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void SetIsShowDamage(bool isShowDamage)
    {
        this.isShowDamage = isShowDamage;
    }
}
