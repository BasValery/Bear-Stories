using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Global
{
    #region Fields
    public static GameTime Time;
    public static Hud Hud;
    public static LoadControl Load;
    public static PlayerInfo PlayerInfo;
    public static DataBase DataBase;
    public static StoryLine StoryLine;
    public static SoundManager SoundManager;
    public static DailyGoldTaskSetter DailyGoldTaskSetter;
    public static Teleporter Teleporter;

    #endregion //Fields

    #region Constructors
    static Global()
    {
        GameObject app = SafeFind("__app");
        GameObject hud = SafeFind("__hud");

        Time = (GameTime)SafeComponent(app, "GameTime");
        DailyGoldTaskSetter = (DailyGoldTaskSetter)SafeComponent(app, "DailyGoldTaskSetter");
        Hud = (Hud)SafeComponent(hud, "Hud");
        Load = (LoadControl)SafeComponent(app, "LoadControl");
        PlayerInfo = (PlayerInfo)SafeComponent(app, "PlayerInfo");
        DataBase = (DataBase)SafeComponent(app, "DataBase");
        StoryLine = (StoryLine)SafeComponent(app, "StoryLine");
        SoundManager = (SoundManager)SafeComponent(app, "SoundManager");
        Teleporter = (Teleporter)SafeComponent(app, "Teleporter");
       
    }
    #endregion //Constructors

    #region Methods
    private static GameObject SafeFind(string s)
    {
        GameObject g = GameObject.Find(s);
        if (g == null)
        {
            Woe("GameObject " + s + "  not on _preload.");
        }
        return g;
    }

    private static Component SafeComponent(GameObject g, string s)
    {
        Component c = g.GetComponent(s);
        if (c == null)
        {
            Woe("Component " + s + " not on _preload.");
        }
        return c;
    }

    private static void Woe(string error)
    {
        Debug.Log(">>> Cannot proceed... " + error);
        Debug.Log(">>> It is very likely you just forgot to launch");
        Debug.Log(">>> from scene zero, the _preload scene.");
    }
    #endregion //Methods
}