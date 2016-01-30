using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    private ParticleSystem m_SpawnParticle;

    public bool m_HasSpawned = false;
    public SpawnOwnerType m_Owner = SpawnOwnerType.NONE;
    public enum SpawnOwnerType
    {
        PLAYER,
        BOT,
        NONE
    }

    void Awake( )
    {
        m_SpawnParticle = GetComponent<ParticleSystem>( );
    }

    private bool IsSpawning = false;
    public void Spawn( )
    {
        if ( m_Owner == null )
        {
            Debug.LogError( "No m_Owner, cannot spawn" );
            return;
        }

        //Play spawn anim
        m_SpawnParticle.Play( );
        m_SpawnTime = 0.5f;

        IsSpawning = true;
    }

    float m_SpawnTime = 0.5f;
    void FixedUpdate( )
    {
        if ( IsSpawning )
        {
            m_SpawnTime -= Time.fixedDeltaTime;
            if ( m_SpawnTime <= 0 )
            {
                GameObject _newFighter = null;

                //Create obj\
                if ( m_Owner == SpawnOwnerType.PLAYER )
                {
                    _newFighter = Resources.Load<GameObject>( "PlayerBot" );
                }
                else
                {
                    //_newFighter = Resources.Load<GameObject>( "PlayerBot" ); Spawn a random bot instead
                }

                if ( _newFighter != null )
                    GameObject.Instantiate( _newFighter, transform.position, Quaternion.identity );

                //Rotate towards direction
                _newFighter.transform.rotation = Quaternion.Euler( new Vector3( 0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z ) );

                GameManager.HasSpawnedPlayer( _newFighter );

                m_HasSpawned = true;
                IsSpawning = false;
            }
        }
    }

    void OnDrawGizmos( )
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube( transform.position, new Vector3( 1, 0.05f, 1 ) );

        Gizmos.DrawLine( transform.position, transform.position + transform.right * 2);
    }
}