using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using AI;

namespace Dialogs
{
    class SuperDuckDialogManager : DialogManager
    {
        #region Fields
        private SuperDuck m_duck;
        private Bunny m_bunny;
        #endregion //Fields

        #region Constructors
        public SuperDuckDialogManager(SuperDuck duck, Bunny bunny)
        {
            m_duck = duck;
            m_bunny = bunny;
        }
        #endregion //Constructors

        #region Properties
        public override string Path
        {
            get
            {
                return "SuperDuck/SuperDuckBase.json";
            }
        }
        #endregion //Properties

        #region Methods
        public override Dictionary<int, Action> ActionFactory()
        {
            var result = new Dictionary<int, Action>();
            result.Add(6, () =>
            {
                m_duck.CallBunny();
            });

            result.Add(2, () => m_duck.Grunt());
            result.Add(3, () => m_duck.Grunt());
            result.Add(4, () => m_duck.Grunt());
            return result;
        }

        public override MainParitipant GetMainParticipant()
        {
            return MainParitipant.First;
        }
        #endregion //Methods
    }

}