using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public void EnablePauseUI()
    {
        gameObject.SetActive(true);
    }

    public void DisablePauseUI()
    {
        gameObject.SetActive(false);
    }
}
