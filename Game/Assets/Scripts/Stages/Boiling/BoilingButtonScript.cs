using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoilingButtonScript : MonoBehaviour
{
    public GameControl gameController;
    public float Heating;
  
    void OnMouseDown()
    {
        if(gameController.GetComponent<GameControl>().boilingItem != null)
        gameController.Heating += Heating;
    }


        // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
