using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeGuider : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        if (Global.PlayerInfo.SceneLoadedFirst("MiniGameKnife"))
        {
            var guideDialogue = new GuideDialogue();
            guideDialogue.AddSentence("Начинай резать! Выбери ингридиент и кинь его на доску, потом в нужный момент нажимай пробел.");
            guideDialogue.AddSentence("Ты должен попасть 3 раза. Чем меньше ты ошибешься, тем выше будет качество зелья.");

            Global.Hud.GuideManager.StartDialouge(guideDialogue);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
