using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

class Guider : MonoBehaviour
{
    #region Fields
    public static Guider instance = null;

    private Camera m_camera;
    private Vector3 m_initialScale;
    private float m_initialOrthogonalSize;
    #endregion //Fields

    #region Properties
    #endregion //Properties

    #region Messages
    public void Awake()
    {
        #region Singleton
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this.This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
            return;
        }

        //Sets this to not be destroyed when reloading scene
        #endregion //Singleton

        m_camera = Camera.main;
        SceneManager.activeSceneChanged += SceneChanged;

        m_initialScale = transform.localScale;
        m_initialOrthogonalSize = m_camera.orthographicSize;
    }

    private void LateUpdate()
    {
        Step();
    }

    private void Start()
    {
    }
    #endregion //Messages

    #region Methods
    public void Step()
    {
        Vector3 newScale = m_initialScale;
        float scale = m_camera.orthographicSize / m_initialOrthogonalSize;
        newScale.x *= scale;
        newScale.y *= scale;
        transform.localScale = newScale;

        Rect m_bounds = CameraController.CameraBounds(
            m_camera.transform.position,
            m_camera.orthographicSize
        );

        Vector3 pos = m_camera.transform.position;

        pos.x = m_bounds.xMin + m_bounds.width * 0.1f;
        pos.y = m_bounds.yMin;
        pos.z = transform.position.z;
        transform.position = pos;

    }

    private void SceneChanged(Scene arg0, Scene arg1)
    {
        m_camera = Camera.main;
    }
    #endregion //Methods
}
