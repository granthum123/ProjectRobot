using UnityEngine;
using System.Collections;

public class SlowRotate : MonoBehaviour
{
    void FixedUpdate( )
    {
        transform.Rotate( Vector3.up, 5 * Time.deltaTime);
    }
}