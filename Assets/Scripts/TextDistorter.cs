using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent( typeof( Outline ) )]
public class TextDistorter : MonoBehaviour
{
    float m_x_Dist, m_y_Dist;
    Outline m_OutlineComponent;

    void Start( )
    {
        m_OutlineComponent = GetComponent<Outline>( );
        m_y_Dist = -m_NormalDist;
    }

    public void ToggleDistort( )
    {
        StartCoroutine( ToggleDistortCallback( ) );
    }

    const float m_NormalDist = 1.5f;
    IEnumerator ToggleDistortCallback( )
    {
        float _virtualTime = 0.0f;
        var _TimeScale = (float)Random.Range(-10, 10);

        //Ping pong x out of control for 2 seconds
        while ( _virtualTime < 2f )
        {
            //Add time
            _virtualTime += Time.fixedDeltaTime;

            _TimeScale = Mathf.Lerp( _TimeScale - Random.Range( 0, 3 ), _TimeScale + Random.Range( 0, 3 ), 0.2f );
            _TimeScale *= _virtualTime + 1 / 3;

            m_x_Dist = Mathf.PingPong( Time.time * ( int )_TimeScale, 5.5f );

            yield return null;
        }

        _virtualTime = 0.0f;

        //Lerp back to normal
        while ( _virtualTime < 2 )
        {
            m_x_Dist = Mathf.Lerp( m_x_Dist, m_NormalDist, 0.1f );
            yield return null;
        }
    }

    void Update( )
    {
        //Update effect distance constantly
        if ( m_x_Dist != m_OutlineComponent.effectDistance.x || m_y_Dist != m_OutlineComponent.effectDistance.y )
        {
            var newDist = new Vector2( m_x_Dist, m_y_Dist );
            m_OutlineComponent.effectDistance = newDist;
        }
    }
}