using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateTolTip : MonoBehaviour, IPointerClickHandler {

    public PlayerTask task;
    public GameObject descriptionPrefab;

    public void OnPointerClick(PointerEventData eventData)
    {
        var copy = Instantiate(descriptionPrefab);
      
        copy.transform.SetParent(transform,false);
        //copy.transform.localPosition = transform.localPosition;

        var pos = GetComponent<RectTransform>().position;
        pos.x += 145;
        pos.y -= 68;

        copy.GetComponent<RectTransform>().localPosition = pos;
        copy.transform.parent = Global.Hud.transform;
        copy.GetComponent<ToolTipBehavior>().FillByOrder(task);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
