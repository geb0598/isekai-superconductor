using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartCanvas : MonoBehaviour
{
    public int activeItemType;

    public void GameStart()
    {
        SceneManager.LoadScene("GeonuSampleScene", LoadSceneMode.Single);
    }

    public void SetActiveItemType(int type)
    {
        activeItemType = type;
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
