using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    public bool m_HasSpawned = false;
    public SpawnOwnerType m_Owner = SpawnOwnerType.NONE;
    public enum SpawnOwnerType
    {
        PLAYER,
        BOT,
        NONE
    }

    public void Spawn( )
    {
        if ( m_Owner == null )
        {
            Debug.LogError( "No m_Owner, cannot spawn" );
            return;
        }

        //Create obj
        //GameObject.Instantiate( Owner, transform.position, Quaternion.identity );
    }

    void OnDrawGizmos( )
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube( transform.position, new Vector3( 1, 0.05f, 1 ) );
    }
}