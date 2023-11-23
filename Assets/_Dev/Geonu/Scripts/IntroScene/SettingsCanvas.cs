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
    public Slider masterVolumeSlider;
    public Slider backgroundVolumeSlider;
    public Slider sfxVolumeSlider;

    [Header("Game Play Options")]
    public Toggle ShowDamageToggle;

    private int _resolutionType;

    private void Start()
    {
        // screen option initialize
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

        // audio option initialize
        masterVolumeSlider.value = GameManager.GetInstance().audioManager.masterVolume;
        backgroundVolumeSlider.value = GameManager.GetInstance().audioManager.bgmVolume;
        sfxVolumeSlider.value = GameManager.GetInstance().audioManager.sfxVolume;

        // Game play option initialize
        ShowDamageToggle.isOn = GameManager.GetInstance().isShowDamage;
    }

    private void OnEnable()
    {
        Start();
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
