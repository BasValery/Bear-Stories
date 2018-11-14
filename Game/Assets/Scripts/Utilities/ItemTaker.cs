using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTaker :  MonoBehaviour
{
    public Predicate<Item> Check;
    public event Action AfterTake;

    private void OnMouseEnter()
    {
        ItemDrag.AddPotionTaker(this);
    }

    private void OnMouseExit()
    {
        ItemDrag.RemoveTaker(this);
    }

    public void PotionRecieved()
    {
        if(AfterTake != null)
        {
            AfterTake.Invoke();
        }
    }

}
