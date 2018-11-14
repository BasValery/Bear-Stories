using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : Target
{

    // Use this for initialization


    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnReached()
    {
        base.OnReached();
        
      
        var guideDialogue = new GuideDialogue();
        var arrowController = Global.Hud.m_arrowcontroller;
        var letter = GameObject.FindGameObjectWithTag("invite");
        var letterImg = letter.transform.GetChild(0);
        letterImg.gameObject.SetActive(true);
        guideDialogue.AddSentence("Ух ты, это же приглашение на турнир!");
        guideDialogue.AddSentence("Я помню, когда был твоего возраста, участвовал в нем.");
        guideDialogue.AddSentence("Я почти победил, если бы не этот жулик Вальдемар...");

        guideDialogue.AddSentence("На этот раз мы должны одолеть его, сейчас я кое-что подправлю...", ()=> {
             letterImg.GetComponent<Animator>().SetTrigger("rewrite");
        });

        guideDialogue.AddSentence("Чтобы туда попасть нужно постараться, вот кирпич для телепортации.", () =>
        {
            Global.Hud.m_brick.SetActive(true);
            Global.Hud.GuideManager.SpecialState(SpecialGuideState.ShowBrick);
                
            arrowController.PointOut(Global.Hud.m_brick.GetComponent<RectTransform>(), ArrowDirection.DOWN);

        });
        guideDialogue.AddSentence("Правда чтобы попасть куда-либо, тебе понадобится карта того места.", () =>
        {
            arrowController.Hide();
            Global.Hud.GuideManager.SpecialState(SpecialGuideState.HideBrick);

            var task = new DailyTask("Получить карту леса", 20, false, false, false, ()=> {
                var tournament = new DailyTask("Попасть на турнир", 21, false, false);
                Global.PlayerInfo.NewDailyTask(tournament);
            });
            Global.PlayerInfo.NewDailyTask(task);
        });

        Global.Hud.GuideManager.StartDialouge(guideDialogue);

        
   
        this.gameObject.SetActive(false);
    }

}
