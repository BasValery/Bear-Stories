using Assets.Scripts.Sky;
using Imphenzia;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DayNightSycle : MonoBehaviour {

    #region Fields
    private GameTime m_gameTime;

    #region Settings
    public float m_dayNightTransition = 0.2f;
    #endregion //Settings

    #region GameObjects
    private GameObject m_skyObject;
    private GameObject m_sunObject;
    private GameObject m_moonObject;
    private GameObject m_rotationPointObject;
    #endregion //GameObjects

    #region Components
    private GradientSkyObject m_gradientSky;
    private Light m_globalLightSource;
    #endregion//Components

    #region data
    public Color[] m_dayGradient = new Color[3];
    public Color[] m_nightGradient = new Color[3];
    private Vector3 m_initialSunPosition;
    private Vector3 m_initialMoonPosition;
    #endregion //data

    #endregion //Fields

    #region Properties
    #endregion //Properties

    #region Messages
    // Use this for initialization
    void Start () {
        //m_clouds.ForEach(x => x.Launch());
        Vector3 local = m_rotationPointObject.transform.localPosition;
        m_initialMoonPosition = new Vector3(local.x + 0.36f, local.y, local.z);
        m_initialSunPosition = new Vector3(local.x - 0.36f, local.y, local.z);
        UpdateGlobe();
        UpdateGlobeLight();
        UpdateSky();
    }

    private void Awake()
    {
        m_gameTime = FindObjectOfType<GameTime>();

        //m_nightGradient = new Color[3];
        //m_dayGradient = new Color[3];

        //initing game objects
        m_skyObject = transform.Find("Sky").gameObject;
        m_sunObject = transform.Find("Sun").gameObject;
        m_moonObject = transform.Find("Moon").gameObject;
        m_rotationPointObject = transform.Find("RotationPointObject").gameObject;
        //

        //intializing components
        m_gradientSky = m_skyObject.GetComponent<GradientSkyObject>();
        m_globalLightSource = transform.Find("GlobalSunSource").GetComponent<Light>();
        //



    }


    void OnValidate()
    {
        if (m_nightGradient.Length != 3)
        {
            Array.Resize(ref m_nightGradient, 3);
        }

        if (m_dayGradient.Length != 3)
        {
            Array.Resize(ref m_dayGradient, 3);
        }

        if (m_dayNightTransition <= 0 || m_dayNightTransition >= 1)
        {
            m_dayNightTransition = 0.5f;
        }
    }

    private void OnEnable()
    {
    }
    


    private void OnDisable()
    {
        //m_clouds.ForEach(x => x.Stop());
    }

    // Update is called once per frame
    void Update () {
        if (m_gameTime && m_gameTime.IsFreezed == false)
        {
            UpdateGlobe();
            UpdateGlobeLight();
            UpdateSky();
        }
    }
    #endregion //Messages


    #region Methods

    private void UpdateGlobe()
    {
        Vector3 rotationPoint = m_rotationPointObject.transform.position;

        //rotating moon

        m_moonObject.transform.localPosition = m_initialMoonPosition;
        m_moonObject.transform.RotateAround(rotationPoint,
            Vector3.back,
            (360.0f * m_gameTime.DayProgress)
            );
        m_moonObject.transform.rotation = Quaternion.Euler(0, 0, m_moonObject.transform.rotation.z);

        //rotation sun

        m_sunObject.transform.localPosition = m_initialSunPosition;
        m_sunObject.transform.RotateAround(rotationPoint,
            Vector3.back,
            (360.0f * m_gameTime.DayProgress)
            );
        m_sunObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void UpdateGlobeLight()
    {
        m_globalLightSource.transform.eulerAngles = new Vector3(
            (360.0f * m_gameTime.DayProgress) * -1 + 90,
            0,
            0
        );
        /*
        m_globalLightSource.transform.Rotate(
            (360.0f / m_gameTime.m_dayDuration) * UnityEngine.Time.deltaTime * -1,
            0,
            0,
            Space.Self
            );
        */
    }

    private void UpdateSky()
    {
        GradientColorKey[] gradientColorKeys = m_gradientSky.gradient.colorKeys;
        //Color color;
        Color dayColor;
        Color nightColor;

        float progress = m_gameTime.DayTimeProgress / m_dayNightTransition;
        for (int i = 0; i < gradientColorKeys.Length; i++)
        {
                dayColor = m_dayGradient[m_dayGradient.Length - 1 - i]; //from last to first
                nightColor = m_nightGradient[m_dayGradient.Length - 1 - i]; // from last to first

            if (m_gameTime.TimeOfDay == TimeOfDay.Day)
            {
                GradientColorKey colorKey = gradientColorKeys[i];

                colorKey.color.r = Mathf.Lerp(nightColor.r, dayColor.r, progress);
                colorKey.color.g = Mathf.Lerp(nightColor.g, dayColor.g, progress);
                colorKey.color.b = Mathf.Lerp(nightColor.b, dayColor.b, progress);

                //color = m_dayGradient[m_nightGradient.Length - 1 - i];
                //colorKey.color.r = Mathf.Lerp(colorKey.color.r, color.r, m_citySceneControl.DayTimeProgress);
                //colorKey.color.g = Mathf.Lerp(colorKey.color.g, color.g, m_citySceneControl.DayTimeProgress);
                //colorKey.color.b = Mathf.Lerp(colorKey.color.b, color.b, m_citySceneControl.DayTimeProgress);

                //colorKey.color.r = color.r;
                //colorKey.color.g = color.g;
                //colorKey.color.b = color.b;
                gradientColorKeys[i] = colorKey;
            }
            else
            {
                GradientColorKey colorKey = gradientColorKeys[i];

                colorKey.color.r = Mathf.Lerp(dayColor.r, nightColor.r, progress);
                colorKey.color.g = Mathf.Lerp(dayColor.g, nightColor.g, progress);
                colorKey.color.b = Mathf.Lerp(dayColor.b, nightColor.b, progress);

                //color = m_nightGradient[m_nightGradient.Length - 1 - i];
                //colorKey.color.r = Mathf.Lerp(colorKey.color.r, color.r, m_citySceneControl.DayTimeProgress);
                //colorKey.color.g = Mathf.Lerp(colorKey.color.g, color.g, m_citySceneControl.DayTimeProgress);
                //colorKey.color.b = Mathf.Lerp(colorKey.color.b, color.b, m_citySceneControl.DayTimeProgress);

                //colorKey.color.r = color.r;
                //colorKey.color.g = color.g;
                //colorKey.color.b = color.b;
                gradientColorKeys[i] = colorKey;
            }
        }
            m_gradientSky.gradient.colorKeys = gradientColorKeys;

    }

    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }


    #endregion //Methods
}
