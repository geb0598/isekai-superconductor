using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public PoolManager poolManager;
    public PlayerController playerController;

    public float timer;

    private void Awake()
    {
        _instance = this;
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
    }
}
