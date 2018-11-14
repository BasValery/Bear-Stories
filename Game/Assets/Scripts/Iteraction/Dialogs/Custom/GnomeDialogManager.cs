using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Dialogs;

namespace Dialogs
{

    public class GnomeDialogManager : DialogManager
    {
        #region TutorialFields
        public static bool GnomeSpoke = false;
        #endregion


        #region Methods
        public override string Path
        {
            get { return "Gnome/GnomeBase.json"; }
        }

        public override Dictionary<int, Action> ActionFactory()
        {
            var dictionary = new Dictionary<int, Action>();
            dictionary.Add(3, () =>
            {
                var node = GetNodeById(1);
                var addNode = GetNodeById(4);
                var index = node.Index(addNode);
                if (index == -1)
                {
                    node.AddChildren(addNode);
                }
            });

            dictionary.Add(7, () =>
            {
                Global.PlayerInfo.DailyTaskCompleted(2);

                var guideDialogue = new GuideDialogue();
                guideDialogue.AddSentence("Надо помочь бедняге. Попробуй узнать у лесничего, может у него найдется что-то на такой случай.");


                Global.Hud.GuideManager.StartDialouge(guideDialogue);

                GnomeSpoke = true;
            });
            dictionary.Add(6, () =>
            {
                Global.DataBase.deleteFromInventory(8);
                Global.PlayerInfo.DailyTaskCompleted(4);
            });
            dictionary.Add(1, () =>
            {
                if (Global.DataBase.getFromInventoryById(8).Id != -1)
                {
                    var node = GetNodeById(1);
                    var addNode = GetNodeById(6);
                    var index = node.Index(addNode);
                    if (index == -1)
                    {
                        node.AddChildren(addNode);
                    }
                }
            });

            return dictionary;
        }

        public override MainParitipant GetMainParticipant()
        {
            return MainParitipant.Second;
        }
        #endregion //Methods
    }

}