using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class Pair<T1, T2>
{
    #region Properties
    public T1 Item1 { get; set; }
    public T2 Item2 { get; set; }
    #endregion //Properties

    #region Constructors
    private Pair()
    {
    }
    #endregion //Constructors

    #region Methods
    public static Pair<T1, T2> Create(T1 item1, T2 item2)
    {
        return new Pair<T1, T2>() { Item1 = item1, Item2 = item2 };
    }
    #endregion //Methods
}
