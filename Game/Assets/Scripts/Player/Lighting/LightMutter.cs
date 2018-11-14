using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightMutter : MonoBehaviour {
    #region Fields
    private GameTime m_gameTime;
    private Light m_light;
    private float m_lightBaseIntensity;
    //private float m_dampVelocity = 0f;
    #endregion //Fields

    #region Messages
    void Awake () {
        m_gameTime = Global.Time;
	}

    private void Start() {
        m_light = GetComponent<Light>();
        m_lightBaseIntensity = m_light.intensity;
    }

    // Update is called once per frame
    void Update () {
        if (m_gameTime == null)
        {
            return;
        }

        float halfTransitionTime = m_gameTime.m_transitionTime / 2f;
        float halfBaseIntensity = m_lightBaseIntensity / 2f;
        float dayProgress = m_gameTime.DayTimeProgress / halfTransitionTime;
        float dayEndProgress = (1 - m_gameTime.DayTimeProgress) / halfTransitionTime;
        //float inverseDayEndProgress = 1 - dayEndProgress;

        if (m_gameTime.TimeOfDay == TimeOfDay.Night)
        {
            // if night starts
            if (dayProgress < dayEndProgress)
            {
                m_light.intensity = Mathf.Lerp(halfBaseIntensity, m_lightBaseIntensity, dayProgress);
            }
            // if night ends
            else
            {
                m_light.intensity = Mathf.Lerp(halfBaseIntensity, m_lightBaseIntensity, dayEndProgress);
            }
        }
        else
        {
            // if day starts
            if (dayProgress < dayEndProgress)
            {
                m_light.intensity = Mathf.Lerp(halfBaseIntensity, 0, dayProgress);
            }
            // if day ends
            else
            {
                m_light.intensity = Mathf.Lerp(halfBaseIntensity, 0, dayEndProgress);
            }
        }

        /*
        float progress = m_gameTime.DayTimeProgress / m_gameTime.m_transitionTime;

        if (m_gameTime.TimeOfDay == TimeOfDay.Night)
        {
            m_light.intensity = Mathf.SmoothStep(0, m_lightBaseIntensity, progress);
        }
        else
        {
            m_light.intensity = Mathf.SmoothStep(m_lightBaseIntensity, 0, progress);
        }
        */

        /*
        if (m_gameTime.TimeOfDay == TimeOfDay.Night)
        {
            if (m_gameTime.DayTimeProgress > 0.3f)
            {
                m_light.intensity = m_lightBaseIntensity;
            }

            m_light.intensity = Mathf.SmoothDamp(
                m_light.intensity,
                m_lightBaseIntensity,
                ref m_dampVelocity,
                m_gameTime.m_transitionTime
                );
        }
        else
        {
            if (m_gameTime.DayTimeProgress > 0.3f)
            {
                m_light.intensity = 0;
            }

            m_light.intensity = Mathf.SmoothDamp(
                m_light.intensity,
                0,
                ref m_dampVelocity,
                m_gameTime.m_transitionTime
                );
        }
        */
    }
    #endregion //Messages
}
