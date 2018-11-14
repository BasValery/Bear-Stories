using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolTipBehavior : MonoBehaviour {

    public Text description;
    public Text itemTitle;
    public Image itemImage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
            Destroy(gameObject);
	}

    public void FillByOrder(PlayerTask task)
    {
        description.text = task.Defenition;
        itemTitle.text = task.Item.Title;
        itemImage.sprite = task.Item.getSprite();
    }
}
