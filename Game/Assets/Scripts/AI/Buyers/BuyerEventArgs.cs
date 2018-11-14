using System;


public class BuyerEventArgs : EventArgs
{
    #region Fields
    Buyer m_npc;
    #endregion //Fields

    #region Properties
    public Buyer NPC
    {
        get { return m_npc; }
    }
    #endregion //Properties

    #region Constructors
    public BuyerEventArgs(Buyer npc)
    {
        m_npc = npc;
    }
    #endregion //Constructors
}