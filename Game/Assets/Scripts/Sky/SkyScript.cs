using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imphenzia;
using UnityEngine;

namespace Assets.Scripts.Sky
{
    public class SkyScript : MonoBehaviour, ISky
    {
        #region Fields
        private MeshRenderer _mesh;
        #endregion //Fields

        #region Messages
        private void Start()
        {
            _mesh = GetComponent<MeshRenderer>();
        }
        #endregion //Messages

        #region ISky members
        public Bounds Bounds
        {
            get { return _mesh.bounds; }
        }
        #endregion //Isky members
    }
}
