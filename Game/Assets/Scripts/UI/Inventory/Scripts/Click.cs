using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Click : MonoBehaviour, IPointerClickHandler
{
    private GameObject lookPanel;
    public Text text;
    // Use this for initialization
    void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData) // 3
    {

        var inventoryObj = GameObject.Find("Inventory");
        lookPanel = inventoryObj.GetComponent<LookSingleton>().getLook();

        var sourceItem = transform.GetChild(0);
        var lookingItem = lookPanel.transform.Find("Slot").Find("Item");

        lookingItem.GetComponent<Image>().sprite = sourceItem.GetComponent<Image>().sprite;
        lookingItem.GetChild(0).GetComponent<Text>().text = sourceItem.GetChild(0).GetComponent<Text>().text;
    }
}
