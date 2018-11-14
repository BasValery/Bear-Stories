using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetInfoPanelPerson : MonoBehaviour {

    public GetInfoPanel Book;
    public Image img;
    public Text text;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setPanel()
    {
        var InfoPanel = Book.getPanel();
        if (!InfoPanel.activeSelf)
            InfoPanel.SetActive(true);
        InfoPanel.GetComponent<InfoPanelSetter>().Set(GetComponent<CurrentPerson>().person);
    }
}
