using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Utility
{
    class Timer : MonoBehaviour
    {
        #region Fields
        public float TimeLeft;
        #endregion //Fields

        #region Properties
        public bool Launched
        {
            get { return TimeLeft == 0; }
        }
        #endregion //Properties

        #region events
        public UnityEvent TimeUp;
        #endregion //Events

        #region Messages
        public void Update()
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                if (TimeLeft <= 0)
                {
                    TimeLeft = 0;
                    TimeUp.Invoke();
                }
            }
            
        }
        #endregion //Messages
    }
}
