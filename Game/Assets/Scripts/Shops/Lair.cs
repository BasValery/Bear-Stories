using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lair : Target {

    #region Fields
    public ParticleSystem m_smoke;

    #endregion //Fields

    #region Messages

    #endregion //Messages

    #region Methods
    protected override void OnReached()
    {
      
        Global.Load.FadeScene("LairInside");
    }
    #endregion //Methods
}
