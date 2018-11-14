using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler {

    // Use this for initialization
    public StrewingItem item;
    public Hide inventory;
    public ClickMonitor clickMonitor;
    public FaskMove flaskMove;
    public void OnDrop(PointerEventData eventData)
    {
        if (BoilngInventoryDrag.dragging == null)
            return;
        item.strewingItem = BoilngInventoryDrag.draggingItem;
        inventory.HideIt();
        clickMonitor.StartStrewing();
        flaskMove.starter = true;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
