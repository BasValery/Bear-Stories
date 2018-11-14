using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility;
using Dialogs;

namespace AI
{
    class SuperDuck : AIBehavior
    {
        #region Fields
        public Bunny m_bunny;
        private SuperDuckDialogManager m_dialogManager;
        private List<int> m_potions;
        #endregion //Fields

        #region Messages
        protected override void Awake()
        {
            base.Awake();
            m_potions = new List<int>();
            m_dialogManager = new SuperDuckDialogManager(this, m_bunny);
            OnLaunch("Duck", "Утка");
        }

        protected override bool ItemDragged(Item item)
        {
            if (item.Id == 15)
            {
                if (m_potions.Contains(15))
                {
                    return false;
                }
                m_dialogManager.Skip(5);
                m_potions.Add(15);
                return true;
            }
            return base.ItemDragged(item);
        }
        #endregion //Messages

        #region Methods
        public void CallBunny()
        {
            m_movementGuider.Destination = new Destination(
                m_bunny.Bottom,
                BunnyReached,
                null
                );
            m_playerMovementGuider.Locked = true;
        }

        public void BunnyReached()
        {
            m_bunny.AttackPlayer();
        }

        protected override void OnInteraction()
        {
            CallPlayer(m_dialogManager);
        }

        protected override void TimeOfDayChanged(object sender, DayChangedEventArgs e)
        {

        }

        public void Grunt()
        {
            m_audioSource.Play();
        }
        

        #endregion //Methods
    }

}