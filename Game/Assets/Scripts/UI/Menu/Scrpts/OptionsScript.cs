using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour {

    public GameObject OptionsPanel;
    public GameObject OptionsPanelEng;
    public Slider sliderSound;
    public Slider sliderMusic;
    public Slider sliderSoundEng;
    public Slider sliderMusicEng;

    public Text soundValue;
    public Text musicValue;

    public Text soundValueEng;
    public Text musicValueEng;

    // Use this for initialization
    public void Activate()
    {
        if (OptionsPanel.activeSelf)
            OptionsPanel.SetActive(false);
        else
            OptionsPanel.SetActive(true);       
    }
    public void ActivateEng()
    {
        if (OptionsPanelEng.activeSelf)
            OptionsPanelEng.SetActive(false);
        else
            OptionsPanelEng.SetActive(true);
    }
    public void Deactivate()
    {
        OptionsPanel.SetActive(false);
    }

    public void SoundChange()
    {
        soundValue.text = sliderSound.value.ToString();
        soundValueEng.text = sliderSoundEng.value.ToString();
        Global.SoundManager.soundVolume = sliderSound.value;
    }

    public void MusicChange()
    {
        musicValue.text = sliderMusic.value.ToString();
        musicValueEng.text = sliderMusicEng.value.ToString();
        Global.SoundManager.musicVolume = sliderMusic.value;
    }

    public void SoundChangeEng()
    {
        soundValue.text = sliderSound.value.ToString();
        soundValueEng.text = sliderSoundEng.value.ToString();
        Global.SoundManager.soundVolume = sliderSoundEng.value;
    }

    public void MusicChangeEng()
    {
        musicValue.text = sliderMusic.value.ToString();
        musicValueEng.text = sliderMusicEng.value.ToString();
        Global.SoundManager.musicVolume = sliderMusicEng.value;
    }
}
