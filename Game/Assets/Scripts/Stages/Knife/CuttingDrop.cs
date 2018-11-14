using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class dropEvent : UnityEvent<bool>
{
}
public class CuttingDrop : MonoBehaviour, IDropHandler
{

    public SpriteRenderer Cuting;
    public GameObject gameControll;
    public GameObject slider;
    public Hide inventoryPanel;

    public void OnDrop(PointerEventData eventData)
    {
        Cuting.sprite = BoilngInventoryDrag.dragging.GetComponent<SpriteRenderer>().sprite;
        Cuting.transform.localScale = new Vector3(1.5f, 1.5f, 0);
        gameControll.GetComponent<CuttingItem>().cutting = BoilngInventoryDrag.draggingItem;
        slider.SetActive(true);
        inventoryPanel.HideIt();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
