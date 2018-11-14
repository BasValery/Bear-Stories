using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility;

namespace AI
{

    [RequireComponent(typeof(MovementGuider))]
    [RequireComponent(typeof(OrderLayerByY))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Timer))]
    class Pigeon : MonoBehaviour
    {
        #region Fields
        public Vector2 m_ground;
        public Vector2[] m_landingSpaces = null;
        private MovementGuider m_movementGuider;
        private Vector3 m_initialPos;
        private GameObject m_player;
        private Timer m_timer;
        private Vector2 m_lastLandingPosition;
        private bool m_landed;
        #endregion //Fields

        #region Messages
        private void Awake()
        {
            m_movementGuider = GetComponent<MovementGuider>();
            m_timer = GetComponent<Timer>();
            m_initialPos = transform.position;
            m_player = GameObject.FindGameObjectWithTag("Player");
            m_timer.TimeUp.AddListener(TimerTimeElapsed);
        }

        private void OnEnable()
        {
            Global.Time.TimeOfDayChanged += Time_TimeOfDayChanged;
        }

        private void OnDisable()
        {
            Global.Time.TimeOfDayChanged -= Time_TimeOfDayChanged;
        }

        private void Start()
        {
            m_movementGuider.SpecialState = MovementSpecialState.Flying;
            m_movementGuider.Destination = new Destination(m_ground, LandingSpaceReached, null);
        }

        private void Update()
        {
            CheckPlayerForthcoming();
        }
        #endregion //Messages

        #region Mehtods
        public void TimerTimeElapsed()
        {
            m_movementGuider.Destination = new Destination(
                DetermineNewLandingPosition(),
                LandingSpaceReached,
                null
                );
            m_landed = false;
        }

        private void CheckPlayerForthcoming()
        {
            if (m_landed == true)
            {
                Vector3 playerPos = m_player.transform.position;
                if (Vector2.Distance(playerPos, transform.position) < 2f)
                {
                    m_movementGuider.Destination = new Destination(
                        DetermineNewLandingPosition(),
                        LandingSpaceReached,
                        null
                        );
                    m_landed = false;
                }
            }
        }

        private void LandingSpaceReached()
        {
            m_timer.TimeLeft = 5f;
            m_landed = true;
            if (m_lastLandingPosition == m_ground)
            {
                m_movementGuider.Animator.SetInteger("state", (int)NPCSTate.Peck);
            }
        }

        private Vector3 DetermineNewLandingPosition()
        {
            Vector2 result;
            if (m_landingSpaces.Length > 0)
            {
                result = m_landingSpaces[UnityEngine.Random.Range(0, m_landingSpaces.Length)];
                if (m_lastLandingPosition == result)
                {
                    result = m_ground;
                }
                m_lastLandingPosition = result;
            }
            else
            {
                result = m_initialPos;
            }

            return result;
        }

        private void Time_TimeOfDayChanged(object sender, DayChangedEventArgs e)
        {
            switch (e.CurrentTimeOfDay)
            {
                case TimeOfDay.Day:
                    m_movementGuider.Destination = new Destination(m_ground, LandingSpaceReached);
                    break;
                case TimeOfDay.Night:
                    m_movementGuider.Destination = new Destination(m_initialPos, null);
                    break;
            }
        }
        #endregion //Methods
    }

}