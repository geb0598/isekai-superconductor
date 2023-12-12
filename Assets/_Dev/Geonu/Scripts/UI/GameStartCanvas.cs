using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartCanvas : MonoBehaviour
{
    public Button[] selectButton;
    public Button gameStartButton;

    public void GameStart()
    {
        SceneManager.LoadScene("GeonuSampleScene", LoadSceneMode.Single);
    }

    public void SetSelectButtonInteractive(int buttonIndex)
    {
        for (int i = 0; i < selectButton.Length; i++)
        {
            if (i == buttonIndex)
                selectButton[i].interactable = false;
            else
                selectButton[i].interactable = true;
        }
    }

    public void GameStartButtonInteractive()
    {
        if (gameStartButton.interactable == false)
            gameStartButton.interactable = true;
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
