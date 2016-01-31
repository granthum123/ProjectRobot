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
    }

    IEnumerator ToggleDistort( )
    {
        float _virtualTime = 0.0f;

        while ( _virtualTime < 2.0f )
        {
            //Add time
            _virtualTime += Time.fixedDeltaTime;

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