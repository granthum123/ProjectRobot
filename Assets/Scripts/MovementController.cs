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
    private ParticleSystem m_SmokeParticleSys;
    private ParticleSystem.EmissionModule m_SmokeParticleEmit;

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
        m_SmokeParticleSys = transform.FindChild( "RobotSmokeLeft" ).GetComponent<ParticleSystem>( );
        m_SmokeParticleEmit = m_SmokeParticleSys.emission;
	}

    private float m_Speed = 0.0f;
    private Vector3 m_Dir = Vector3.zero, m_TargetDir = Vector3.zero;
    private float DirDiff = 0.0f;
    void FixedUpdate( )
    {
        m_MoveDirection = Vector3.zero;

        m_MoveDirection = Vector3.ClampMagnitude( new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) ), 1 );
        m_TargetDir = m_MoveDirection;
        if ( m_MoveDirection != Vector3.zero )
        {
            m_Speed = Mathf.Clamp( m_Speed + ( Time.fixedDeltaTime * m_Acceleration ), 0, 1 );
        }
        else
        {
            m_Speed = Mathf.Clamp( m_Speed - ( Time.fixedDeltaTime * m_Deceleration ), 0, 1 );
        }

        //Add final velocity
        m_RigidBodyComponent.velocity += m_MoveDirection * m_MovementSpeed * m_Speed;
        m_RigidBodyComponent.velocity = Vector3.ClampMagnitude( m_RigidBodyComponent.velocity, 5 );

        //Rotate towards direction
        var calcDir = new Vector3( m_MoveDirection.z, 0, -m_MoveDirection.x );
        float step = m_Speed * Time.deltaTime * 3.0f;
        m_Dir = Vector3.RotateTowards( transform.forward, calcDir, step, 0.0f );

        DirDiff = Vector3.Dot( transform.TransformDirection( Vector3.forward ), ( transform.position + m_TargetDir ) - transform.position );
        print( DirDiff );

        transform.rotation = Quaternion.LookRotation( m_Dir );

        //Are we moving?
        if ( m_RigidBodyComponent.velocity != Vector3.zero && !m_SmokeParticleSys.isPlaying)
        {
            m_SmokeParticleSys.Play( );
        }
        else if ( m_RigidBodyComponent.velocity == Vector3.zero && m_SmokeParticleSys.isPlaying )
        {
            m_SmokeParticleSys.Stop( );
        }
    }

    public float m_JetpackFuelVal = 10.0f;
    [HideInInspector] public float m_Fuel = 0.0f;
    private float m_JetpackTimer = 0.0f;
    void UseJetpack( )
    {
        //Are we using it for the first time?
        //If so, reset fuel amount
        if ( IsGrounded && m_JetpackTimer == 0.0f )
        {
            m_Fuel = m_JetpackFuelVal;
        }

        m_Fuel -= 30 * Time.fixedDeltaTime;
        m_MoveDirection.y += m_JumpHeight;

        print( "New Fuel Amount: " + m_Fuel );
    }
}