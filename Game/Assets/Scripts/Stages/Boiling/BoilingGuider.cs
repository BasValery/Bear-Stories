using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilingGuider : MonoBehaviour {
    public RectTransform valve;
    public RectTransform tempreture;
	// Use this for initialization
	void Start () {
        if (Global.PlayerInfo.SceneLoadedFirst("Boiling"))
        {
            Global.Time.Freeze(true);
            var guideDialogue = new GuideDialogue();
            var arrowController = Global.Hud.m_arrowcontroller;
            guideDialogue.AddSentence("Тут ты можешь нагреть жидкость");
            guideDialogue.AddSentence("Перетяни жидкость и нажимай на кнопку.", () =>
            {
                arrowController.PointOut(valve, ArrowDirection.LEFT);
            }
            );

            guideDialogue.AddSentence("Смотри внимательно на температуру, в нужный момент забирай колбу!", () =>
            {
                arrowController.PointOut(tempreture, ArrowDirection.LEFT);
              
            }
           );

            guideDialogue.AddSentence("Удачи!", () =>
            {
                arrowController.Hide();
                Global.Time.Freeze(false);
            }
            );

            Global.Hud.GuideManager.StartDialouge(guideDialogue);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
