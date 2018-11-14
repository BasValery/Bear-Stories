using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemDrag : EventTrigger
{

    static public Item dragItem;
    public GameObject dragPrefab;
    public Item item;

    private GameObject copy;
    private static ItemTaker Taker = null;

    public override void  OnBeginDrag(PointerEventData eventData)
    {
        dragItem = item;
        copy = Instantiate(dragPrefab);
        copy.GetComponent<SpriteRenderer>().sprite = item.getSprite();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
        pos.z = 0f;
        copy.transform.position = pos;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if(Taker != null)
        {
            ItemRecieved();
        }

        Destroy(copy);
        copy = null;
        dragItem = null;
    }

    static public void AddPotionTaker(ItemTaker taker)
    {
        Taker = taker;
    }

    static public void RemoveTaker(ItemTaker taker)
    {
        if (taker == Taker)
        {
            Taker = null;
        }
    }

    private void ItemRecieved()
    {
        if (Taker.Check != null && Taker.Check(item))
        {
            Global.DataBase.deleteFromInventory(item.Id);
            Global.Hud.RefillInventory();
            Taker.PotionRecieved();
        }
    }
}
