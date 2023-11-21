using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsCanvas : MonoBehaviour
{
    [Header("Screen Options")]
    FullScreenMode _screenMode;
    public Toggle fullScreenToggle;
    public Dropdown resolutionDropdown;
    List<Resolution> resolutions = new List<Resolution>();

    [Header("Sound Options")]
    public float volume; // ?

    [Header("Game Play Options")]
    public bool isShowingDamage;

    private int _resolutionType;

    private void Awake()
    {
        resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();

        foreach (Resolution resolution in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = resolution.width + " x " + resolution.height + " " + resolution.refreshRate + "hz";
            resolutionDropdown.options.Add(option);
        }
        resolutionDropdown.RefreshShownValue();

        fullScreenToggle.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropdownOptionChange(int selectedResolutionType)
    {
        _resolutionType = selectedResolutionType;
    }

    public void FullScreenToggle(bool isFull)
    {
        _screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void ApplyChanges()
    {
        Screen.SetResolution(resolutions[_resolutionType].width, resolutions[_resolutionType].width, _screenMode);
    }

    public void EnableSettingsCanvas()
    {
        gameObject.SetActive(true);
    }

    public void DisableSettingsCanvas()
    {
        gameObject.SetActive(false);
    }
}
