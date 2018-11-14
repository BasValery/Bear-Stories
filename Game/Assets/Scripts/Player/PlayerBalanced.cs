using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalanced : MonoBehaviour
{

    private bool balanced;
    public bool IsBalanced
    {
        get
        {
            return balanced;
        }
        private
        set
        {
            balanced = value;
        }
    }
   
    public void setBalance(int balance)
    {
        if (balance == 0)
            IsBalanced = false;
        else
            IsBalanced = true;
    }
}
