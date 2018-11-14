using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {
    #region //Fields
    public string m_sceneName;
    #endregion //Fields

    // Use this for initialization
    private void Awake()
    {
        Global.Load.LoadScene(m_sceneName);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
