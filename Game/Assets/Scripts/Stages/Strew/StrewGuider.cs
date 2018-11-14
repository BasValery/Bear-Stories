using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrewGuider : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Global.PlayerInfo.SceneLoadedFirst("Strew"))
        {
            Global.Time.Freeze(true);
            var guideDialogue = new GuideDialogue();

            guideDialogue.AddSentence("Это самый важный этап! Нужно засыпать все в колбу.");
            guideDialogue.AddSentence("Аккуратнее, если ты просыпешь слишком много нам не заплатят.", () =>
                {
                    Global.Time.Freeze(false);
                });
            Global.Hud.GuideManager.StartDialouge(guideDialogue);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
