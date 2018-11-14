using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoilngInventoryDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {

    public GameObject picPrefab;
    public static GameObject dragging;
    public static Item draggingItem;
    
    private GameObject copy;

    public Item item { get; set; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        copy = Instantiate(picPrefab);
        copy.GetComponent<SpriteRenderer>().sprite = transform.Find("Image").GetComponent<Image>().sprite;
        draggingItem = item;
        dragging = copy;
    }

    public void OnDrag(PointerEventData eventData)
    {

        Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
        pos.z = 0f;
        copy.transform.position =pos;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        Destroy(copy);
        copy = null;
    }


}
