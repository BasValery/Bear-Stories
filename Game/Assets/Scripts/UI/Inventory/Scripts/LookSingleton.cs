using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LookSingleton : MonoBehaviour
{

    public GameObject lookPanelPrefab;
    public GameObject inventoryPanel;
    private GameObject lookPanel;
    public bool created = false;
    public GameObject getLook()
    {
        if (created)
            return lookPanel;

        lookPanel = Instantiate(lookPanelPrefab);
        lookPanel.transform.SetParent(inventoryPanel.transform);
        var lookTransform = lookPanel.GetComponent<RectTransform>();
        lookTransform.localPosition = new Vector3(-210f, -95f, 0f);
        created = true;
        return lookPanel;
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
