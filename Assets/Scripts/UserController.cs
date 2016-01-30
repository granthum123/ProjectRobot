﻿using UnityEngine;
using System.Collections;

public class UserController : MonoBehaviour {
	
	private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
	private string m_TurnAxisName;
	private float m_MovementInputValue;         // The current value of the movement input.
	private float m_TurnInputValue;
	private MovementController m_movement;
	
	private void Start()
	{
        m_movement = GetComponent<MovementController>( );
		// The axes names are based on player number.
		m_MovementAxisName = "Vertical";
		m_TurnAxisName = "Horizontal";
	}
	
	
	private void Update()
	{
		// Store the value of both input axes.
		m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
		m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
	}
	
	private void FixedUpdate()
	{
		// Adjust the rigidbodies position and orientation in FixedUpdate.
        //m_movement.Move(m_MovementInputValue);
        //m_movement.Turn(m_TurnInputValue);
		
        //if (Input.GetKeyDown (KeyCode.Space))
        //{
        //    m_movement.Jump();
        //}
	}
}