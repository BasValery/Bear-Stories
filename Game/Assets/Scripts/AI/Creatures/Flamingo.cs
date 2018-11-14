using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    class Flamingo : MonoBehaviour
    {
        #region Fields
        private Animator m_animator;
        private Collider2D m_collider;
        #endregion //Fields

        #region Properties
        #endregion //Properties

        #region Messages
        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_collider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                m_animator.SetTrigger("CowerDown");
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                m_animator.SetTrigger("CowerUp");
            }
        }
        #endregion //Messages

        #region Methods
        #endregion //Methods
    }

}