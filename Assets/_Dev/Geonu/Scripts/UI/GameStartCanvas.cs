using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartCanvas : MonoBehaviour
{
    public int activeItemID; // WeaponManager에서 관리

    public void GameStart()
    {
        // gameStartEvent<int> Invoke -> gameStartEvent: ActiveItem Activate, ...
        SceneManager.LoadScene("GeonuSampleScene", LoadSceneMode.Single);
    }

    // WeaponManager에서
    public void SetActiveItemID(int id)
    {
        activeItemID = id;
    }

    public void EnableGameStartCanvas()
    {
        gameObject.SetActive(true);
    }

    public void DisableGameStartCanvas()
    {
        gameObject.SetActive(false);
    }
}
