using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropScript : MonoBehaviour, IDropHandler  {

    public GameObject boilingObject;
    public Hide inventory;
    public void OnDrop(PointerEventData eventData)
    {
        boilingObject.SetActive(true);
        GameObject.Find("GameController").GetComponent<GameControl>().boilingItem = BoilngInventoryDrag.draggingItem;
        inventory.HideIt();
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
