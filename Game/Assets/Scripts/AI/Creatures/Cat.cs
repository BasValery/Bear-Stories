using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility;

namespace AI
{
	[RequireComponent(typeof(Timer))]
	class Cat : AIBehavior
	{
	    #region Fields
		public float m_minRelaxTime = 10f;
		public float m_maxRelaxTime = 20f;

	    public Vector2[] m_positions;
	    private Timer m_timer;
	    private Vector3 m_lastDecision;
	    #endregion //Fields

	    #region Messages
	    protected void Start()
	    {
	    	m_timer = GetComponent<Timer>();
	    	m_timer.TimeUp.AddListener(TimeUp);
	    	SelectNewPath();
	    }
	    #endregion //Messages

		#region	Methods
	    protected void TimeUp()
	    {
	    	SelectNewPath();
	    }

	    protected override void OnInteraction()
	    {
            m_playerMovementGuider.Destination = new Destination(ToVector3(transform.position), PlayerReached, null);
	    }

        protected void PlayerReached()
        {
            m_audioSource.Play();
        }

	    protected override void TimeOfDayChanged(object sender, DayChangedEventArgs e)
	    {
	    	switch(e.CurrentTimeOfDay)
	    	{
	    		case TimeOfDay.Night:
                    m_movementGuider.Destination = null;
                    m_animator.SetInteger("state", (int)NPCSTate.Lying);
		    		break;
	    		case TimeOfDay.Day:
		    		SelectNewPath();
		    		break;
	    	}
	    }

	    protected void SelectNewPath()
	    {
	    	m_movementGuider.Destination = new Destination(IdentifyDestinationPosition(), DestiantionReached, null);
	    }

	    protected Vector3 IdentifyDestinationPosition()
	    {
	    	Vector3 result;
	    	if (m_positions.Length == 0)
	    	{
	    		result = Bottom;
	    	}
	    	else
	    	{
	    		do
	    		{
	    			result = m_positions[UnityEngine.Random.Range(0, m_positions.Length)];
	    		} while (result == m_lastDecision);
	    		m_lastDecision = result;
	    	}
	    	return result;
	    }

	    protected virtual void DestiantionReached()
	    {
	    	m_timer.TimeLeft = UnityEngine.Random.Range(m_minRelaxTime, m_maxRelaxTime);
	    	m_animator.SetInteger("state", (int)NPCSTate.Lying);
	    }
	    #endregion //Methods
	}
}