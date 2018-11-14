using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DragBottle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

    public GameObject picPrefab;
    public GameControl control;
    private GameObject copy;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (control.boilingItem == null)
            return;
        control.fixTemp();
        copy = Instantiate(picPrefab);
        copy.GetComponent<SpriteRenderer>().sprite = control.boilingItem.getSprite();
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
        pos.z = 0f;
        copy.transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        control.finish();
        Destroy(copy);
        copy = null;
        
        Global.Load.LoadScene("Strew");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
