using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDuck : Target {
    #region Fields
    public string m_achivementName;
    #endregion //Fields

    #region Messages

    // Use this for initialization
    private void Start()
    {
        gameObject.SetActive(!Global.PlayerInfo.CheckAchivement(m_achivementName));
    }

	// Update is called once per frame
	void Update () {
		
	}
    #endregion //Messages

    #region Methdos
    protected override void OnReached()
    {
        Global.Hud.AddToInventoryAnim(Global.DataBase.getAchives()[0].getSprite());
        Global.DataBase.getAchives()[0].IsOpen = true;
        Global.Hud.ReloadAchive();
        gameObject.SetActive(false);
        Global.PlayerInfo.Achivement(m_achivementName);
    }
    #endregion //Methdos
}
