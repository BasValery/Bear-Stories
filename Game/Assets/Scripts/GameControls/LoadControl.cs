using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadControl : MonoBehaviour
{
    #region Fields
    public GameObject m_loadingScreenObject;
    public Slider m_slider;
    public Animator m_animator;
    public RawImage m_black;

    private AsyncOperation m_async;
    private bool m_loading = false;
    #endregion //Fields

    #region Events

    #endregion //Events

    #region Properties
    public string CurrentSceneName
    {
        get { return SceneManager.GetActiveScene().name; }
    }
    #endregion //Properties

    #region Messages
    #endregion //Messages

    #region Methods
    public void LoadScene(string sceneName)
    {
        if (m_loading == false)
        {
            m_loading = true;
            StartCoroutine(Loading(sceneName));
        }
    }

    public void FadeScene(string sceneName)
    {
        if (m_loading == false)
        {
            m_loading = true;
            StartCoroutine(Fading(sceneName));
        }
    }

    private IEnumerator Loading(string sceneName)
    {
        if (m_loadingScreenObject == null)
        {
            Debug.LogError("loading screen object is not set");
            yield break;
        }

        m_loadingScreenObject.SetActive(true);
        m_async = SceneManager.LoadSceneAsync(sceneName);
        m_async.allowSceneActivation = false;

        while (m_async.isDone == false)
        {
            m_slider.value = m_async.progress;

            if (m_async.progress >= 0.9f)
            {
                m_slider.value = 1.0f;
                m_async.allowSceneActivation = true;
            }

            yield return null;
        }
        m_loadingScreenObject.SetActive(false);
        m_loading = false;
    }

    private IEnumerator Fading(string sceneName)
    {
        if (m_black == null)
        {
            Debug.LogError("loading screen object is not set");
            yield break;
        }


        //m_animator.SetTrigger("Fade");
        //yield return new WaitUntil(() => m_black.color.a == 1);
        //SceneManager.LoadScene(sceneName);
        //m_animator.SetTrigger("Reveal");



        m_animator.SetTrigger("Fade");
        m_async = SceneManager.LoadSceneAsync(sceneName);
        m_async.allowSceneActivation = false;

        while (m_async.progress <= 0.9f && m_black.color.a != 1)
        {
            yield return null;
        }

        m_async.allowSceneActivation = true;
        while (m_async.isDone == false)
        {
            yield return null;
        }
        m_animator.SetTrigger("Reveal");
        m_loading = false;
    }
    #endregion //Methods
}
