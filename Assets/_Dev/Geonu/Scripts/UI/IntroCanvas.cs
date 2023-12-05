using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCanvas : MonoBehaviour
{
    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void EnableIntroCanvas()
    {
        gameObject.SetActive(true);
    }

    public void DisableIntroCanvas()
    {
        gameObject.SetActive(false);
    }
}
