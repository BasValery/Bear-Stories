using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dialogs
{
    public class WitcherDialogManager : DialogManager
    {

        #region TutorialFields
        public static bool WhitcherSpoke = false;
        private List<int> acceptPotions;
        #endregion

        #region Constructors
        public WitcherDialogManager(List<int> acceptList)
        {
            acceptPotions = acceptList;
        }
        #endregion //Constructors

        #region Properties
        public override string Path
        {
            get { return "Witcher/WitcherBase.json"; }
        }
        #endregion //Properties

        #region Methods
        public override Dictionary<int, Action> ActionFactory()
        {
            var dictionary = new Dictionary<int, Action>();
            dictionary.Add(3, CheckDiscount);
            dictionary.Add(8, () =>
            {
                Global.PlayerInfo.DailyTaskCompleted(3);
            });
            dictionary.Add(13, () =>
            {
                Global.DataBase.WhitcherAddQuestItem(8, 10);
                WhitcherSpoke = true;

            });
            dictionary.Add(14, () =>
            {
                Global.DataBase.WhitcherAddQuestItem(8, 10);
                WhitcherSpoke = true;
            });
            dictionary.Add(15, () =>
            {
                Global.DataBase.WhitcherAddQuestItem(8, 10);
                WhitcherSpoke = true;
            });
            dictionary.Add(16, () =>
            {
                Global.DataBase.WhitcherAddQuestItem(8, 10);
                WhitcherSpoke = true;
            });
            dictionary.Add(17, () =>
            {
                acceptPotions.Add(9);
                WhitcherSpoke = true;
            });
            return dictionary;
        }

        private void CheckDiscount()
        {
            var itemId = Global.DataBase.getById(1).Id;
            var node = GetNodeById(3);
            if (itemId != -1)
            {
                if (node != null)
                {
                    node.AddChildren(GetNodeById(4));
                }
            }
            else
            {
                node.RemoveChildren(GetNodeById(4));
            }
        }
        #endregion //Methods
    }
}