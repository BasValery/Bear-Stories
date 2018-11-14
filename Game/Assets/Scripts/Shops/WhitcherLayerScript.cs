using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhitcherLayerScript : Target {

    public Image shopWindow;

    private void OnEnable()
    {
        Global.Time.TimeOfDayChanged += Time_TimeOfDayChanged;
    }

    private void Time_TimeOfDayChanged(object sender, DayChangedEventArgs e)
    {
        if (e.CurrentTimeOfDay == TimeOfDay.Night)
        {
            shopWindow.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        Global.Time.TimeOfDayChanged -= Time_TimeOfDayChanged;
    }

    private void LateUpdate()
    {
        if (shopWindow.gameObject.activeSelf)
        {
            if (Mathf.Abs(Global.PlayerInfo.Storage["PlayerX"] - transform.position.x) > 4.5)
            {
                shopWindow.gameObject.SetActive(false);
               // Global.Hud.m_arrowcontroller.Hide();
            }
        }
    }

    protected override void OnReached()
    {
        if (shopWindow != null)
        {
            shopWindow.gameObject.SetActive(true);
           // Global.Hud.m_arrowcontroller.PointOut(shopWindow.rectTransform, ArrowDirection.RIGHT);
        }
    }
}
