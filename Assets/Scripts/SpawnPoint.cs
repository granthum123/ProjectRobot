using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    //The object to be spawned
    [HideInInspector] public GameObject Owner;

    public void Spawn( )
    {
        if ( Owner == null )
        {
            Debug.LogError( "No Owner, cannot spawn" );
            return;
        }

        //Create obj
        GameObject.Instantiate( Owner, transform.position, Quaternion.identity );
    }

    void OnDrawGizmos( )
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube( transform.position, new Vector3( 1, 0.05f, 1 ) );
    }
}