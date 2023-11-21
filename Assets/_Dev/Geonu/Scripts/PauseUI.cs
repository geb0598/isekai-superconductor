using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public void OnPauseButtonClicked()
    {
        gameObject.SetActive(true);
    }

    public void OnBackButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
