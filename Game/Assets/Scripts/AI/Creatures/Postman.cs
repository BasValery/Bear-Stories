using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AI
{
    class Postman : AIBehavior
    {
        #region Fields
        public Vector2 m_exit;
        #endregion //Fields

        #region Messages
        protected override void Awake()
        {
            base.Awake();
        }

        protected void Start()
        {
            var destPos = new Vector3(
                m_exit.x,
                m_exit.y,
                transform.position.z
                );
            m_movementGuider.Destination = new Destination(destPos);
        }
        #endregion //Messages

        #region Methods
        protected override void OnInteraction()
        {
            
        }

        protected override void TimeOfDayChanged(object sender, DayChangedEventArgs e)
        {

        }
        #endregion //Methods
    }
}
