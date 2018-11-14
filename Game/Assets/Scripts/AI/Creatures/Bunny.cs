using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Dialogs;
using System.Collections;

namespace AI
{
    class Bunny : AIBehavior
    {
        #region Fields
        private BunnyDialogManager m_dialogManager;
        private Vector3 m_intialPosition;
        #endregion //Fields

        #region Messages
        protected override void Awake()
        {
            base.Awake();
            m_name = "Bunny";
            m_displayName = "Кролик";
            m_dialogManager = new BunnyDialogManager();
            m_conversationDistance = 3f;
        }

        protected void Start()
        {
            m_intialPosition = m_movementGuider.Bottom;
        }
        #endregion //Messages

        #region Methods
        public void AttackPlayer()
        {
            var dest = new Vector3(
                m_playerMovementGuider.Bounds.min.x - m_playerMovementGuider.Bounds.extents.x,
                m_playerMovementGuider.Bottom.y + 0.3f,
                transform.position.z
                );
            m_audioSource.Play();
            m_movementGuider.Destination = new Destination(dest, PlayerReched, null);
        }

        public void PlayerReched()
        {
            m_playerMovementGuider.Animator.SetInteger("state", (int)NPCSTate.Falling);
            m_playerMovementGuider.Locked = false;
            m_animator.SetInteger("state", (int)NPCSTate.Hit);
            m_audioSource.Stop();
            StartCoroutine("Return");
        }

        private IEnumerator Return()
        {
            yield return new WaitForSeconds(0.2f);
            m_movementGuider.Destination = new Destination(m_intialPosition);
            m_playerMovementGuider.Destination = new Destination(m_intialPosition, InitialPositionReached, null);
            m_animator.SetBool("drag", true);
        }
        
        private void InitialPositionReached()
        {
            Global.Load.FadeScene("Development");
        }

        protected override void OnInteraction()
        {
            CallPlayer(m_dialogManager);
        }

        protected override void TimeOfDayChanged(object sender, DayChangedEventArgs e)
        {
        }
        #endregion //Methods
    }
}