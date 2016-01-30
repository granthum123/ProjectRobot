﻿using UnityEngine;

public class MovementController : MonoBehaviour
{
	public float m_Speed = 12f;                 // How fast the tank moves forward and back.
	public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
	private Rigidbody m_Rigidbody;              // Reference used to move the tank.
	
	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
	}
	
	public void Move(float inputValue)
	{
		// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
		Vector3 movement = transform.forward * inputValue * m_Speed * Time.deltaTime;
		
		// Apply this movement to the rigidbody's position.
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
	}
	
	public void Turn(float inputValue)
	{
		// Determine the number of degrees to be turned based on the input, speed and time between frames.
		float turn = inputValue * m_TurnSpeed * Time.deltaTime;
		
		// Make this into a rotation in the y axis.
		Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
		
		// Apply this rotation to the rigidbody's rotation.
		m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
	}
	
	public void Jump()
	{
		m_Rigidbody.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
	}
}