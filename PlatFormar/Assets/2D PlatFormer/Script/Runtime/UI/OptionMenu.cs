using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    public Slider BGMSlider;
    public Slider SFXSlider;
    public AudioMixer am;

    void Start()
    {
        // 해상도
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> ResolutionList = new List<string>();

        for( int i = 0; i < resolutions.Length; i++)
        {
            string tmp = resolutions[i].width + " x " + resolutions[i].height + "  " + resolutions[i].refreshRate + "Hz";
            ResolutionList.Add(tmp);
        }
        resolutionDropdown.AddOptions(ResolutionList);
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIDX");

        // 사운드
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    private void Update()
    {
        
    }

    public void SetResolution(int ResolutionIndex)
    {
        Resolution res = resolutions[ResolutionIndex];
        Screen.SetResolution(res.width, res.height, true);
        PlayerPrefs.SetInt("ResolutionIDX", ResolutionIndex);
    }

    public void SetBGMVolume(float Slidervalue)
    {
        am.SetFloat("BGMVolume", Mathf.Log10(Slidervalue) * 20);
        PlayerPrefs.SetFloat("BGMVolume", Slidervalue);
    }

    public void SetSFXVolume(float Slidervalue)
    {
        am.SetFloat("SFXVolume", Mathf.Log10(Slidervalue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", Slidervalue);
    }
}
