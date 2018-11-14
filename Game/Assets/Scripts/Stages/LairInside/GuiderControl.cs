using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiderControl : MonoBehaviour
{

    public RectTransform board;
    public RectTransform bottle;
    // Use this for initialization
    void Start()
    {
        if (Global.PlayerInfo.SceneLoadedFirst("LayerInside"))
        {
            Global.Time.Freeze(true);
            var guideDialogue = new GuideDialogue();
            var arrowController = Global.Hud.m_arrowcontroller;
            guideDialogue.AddSentence("Настало время поработать, давай я быстро тебе все объясню.");
            guideDialogue.AddSentence("Это доска для нарезания, на ней ты можешь измельчить ингредиенты.", () =>
            {
                arrowController.PointOut(board, ArrowDirection.RIGHT);
            }
            );

            guideDialogue.AddSentence("А это горелка, тут ты можешь нагреть жидкость и засыпать в нее измельченные ингредиенты.", () =>
            {
                arrowController.PointOut(bottle, ArrowDirection.LEFT);
            }
           );

            guideDialogue.AddSentence("Теперь ты можешь попробовать сварганить свое первое зелье!", () =>
                {
                    arrowController.Hide();
                    Global.Time.Freeze(false);
                }
                );
            Global.Hud.GuideManager.StartDialouge(guideDialogue);
        }
       
    }


    // Update is called once per frame
    void Update()
    {

    }
}
