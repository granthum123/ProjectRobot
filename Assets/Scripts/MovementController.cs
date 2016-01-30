﻿using UnityEngine;

public class MovementController : MonoBehaviour
{
    /// <summary>
    /// Locomotion
    /// </summary>
	public float    m_MovementSpeed = 2.0f,
                    m_Acceleration = 1.0f,
                    m_Deceleration = 0.2f,
                    m_JumpHeight = 5.0f;

    private Vector3 m_MoveDirection = Vector3.zero;

    /// <summary>
    /// Components
    /// </summary>
    private Rigidbody m_RigidBodyComponent;

    /// <summary>
    /// Accessors
    /// </summary>
    private bool IsGrounded
    {
        get
        {
            //Cast a ray from bottom of player
            RaycastHit _hit;

            Ray _ray = new Ray( );
            _ray.origin = transform.position + ( -transform.up * 0.2f );
            _ray.direction = -transform.up;

            Debug.DrawLine( _ray.origin, transform.position + _ray.direction );
            return Physics.Raycast( _ray, out _hit, 1) && _hit.collider.gameObject.isStatic;
        }
    }

	private void Awake()
	{
        //Set rigid body
        m_RigidBodyComponent = GetComponent<Rigidbody>( );
	}

    void FixedUpdate( )
    {
        print( IsGrounded );

        m_MoveDirection += new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) );

        //Add final velocity
        m_RigidBodyComponent.velocity += m_MoveDirection * Time.fixedDeltaTime;
    }

    void Jump( )
    {
        m_MoveDirection.y += m_JumpHeight;
    }
}