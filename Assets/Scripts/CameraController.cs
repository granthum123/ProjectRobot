using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public bool bLockPosition = false;
    public Transform m_Target;
    public Vector3  m_CameraPositionOffset, m_TargetRotation;

    void Awake( )
    {
        //Set player as target if we don't have one already
        if ( m_Target == null ) m_Target = GameObject.FindGameObjectWithTag( "Player" ).transform;
    }

    public Vector3 m_CameraToLocation { get; private set; }
    void FixedUpdate( )
    {
        if ( m_Target != null )
        {
            //Update Camera to location
            m_CameraToLocation = m_Target.transform.position + m_CameraPositionOffset;

            //Update camera location and rotation
            if ( !bLockPosition )
            {
                transform.rotation = Quaternion.Euler( m_TargetRotation );
                transform.position = Vector3.Lerp( transform.position, m_CameraToLocation, 0.2f );
            }
        }
    }
}