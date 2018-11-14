using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Video;

class MoviePlayControl : MonoBehaviour
{
    #region Fields
    private VideoPlayer m_videoPlayer;
    #endregion //Fields

    #region Messages
    private void Awake()
    {
        m_videoPlayer = GetComponent<VideoPlayer>();
        Global.Hud.gameObject.SetActive(false);
    }

    private void Start()
    {
        m_videoPlayer.loopPointReached += LoopPlayerReached;
    }



    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_videoPlayer.Stop();
            Global.Hud.gameObject.SetActive(true);
            Global.Load.LoadScene("Prologue");
        }
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            if (touch.tapCount == 2)
            {
                m_videoPlayer.Stop();
                Global.Hud.gameObject.SetActive(true);
                Global.Load.LoadScene("Prologue");
            }
        }
    }
    #endregion //Messages

    #region Methods
    private void LoopPlayerReached(VideoPlayer source)
    {
        m_videoPlayer.Stop();
        Global.Hud.gameObject.SetActive(true);
        Global.Load.LoadScene("Prologue");
    }

    #endregion //Methods
}
