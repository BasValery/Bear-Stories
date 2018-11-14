using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfacePoint : MonoBehaviour {
    #region Fields

    #endregion //Fields

    #region Messages
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    #endregion //Messages

    #region Methods
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (gameObject != null)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    #endregion //Methos
}
