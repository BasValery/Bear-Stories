using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Slider sound;
    public Slider music;

    private LoadControl m_loadControl;

    private void Awake()
    {
        m_loadControl = Global.Load;
    }

    public void Options()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
    public void SetVolume()
    {
        Global.SoundManager.musicVolume = music.value;
        Global.SoundManager.soundVolume = sound.value;
    }
    public void Exit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit ();
    #endif
    }

    public void NewGame()
    {
        //m_loadControl.FadeScene("Game");
        //m_loadControl.LoadScene("Game");
        m_loadControl.LoadScene("MoviePlay");
        //SceneManager.LoadScene("Game");
    }
}
