using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public sealed class GameManager : MonoBehaviour
{
    #region Variables
    ///Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if ( instance == null )
                instance = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<GameManager>( );

            return instance;
        }
    }

    public GameObject m_Player, m_GameCamera, m_FadeScreenObj, m_RobotPrefab, m_BigTimeTextPrefab;

    private Animation m_CamAnimationComponent;

    public bool m_Paused;							// Game state

    public bool m_RoundStarted = false, 
                m_MatchStarted = false;

    public float m_CurrentGameTime { get; private set; }
    public int m_PlayerScore { get; private set; }

    public delegate void MatchEventDelegate( );
    public static event MatchEventDelegate  OnMatchStart,
                                            OnMatchEnd,
                                            OnMatchReset;

    public delegate void PlayerStartDelegate( GameObject newPlayer );
    public static event PlayerStartDelegate OnPlayerSpawned;

    public float ScreenAlpha = 0.5f;

    public int NumberOfBots = 0;

    private SpawnPoint[] m_SpawnPoints;
    private SpawnPoint m_PlayerSpawn;

    #endregion

    #region Match Control
    public void StartMatch( )
    {
        m_MatchStarted = true;
        m_PlayerSpawnToggled = false;
        m_BigTimeText.text = ""; //Clear time text

        if ( OnMatchStart != null )
            OnMatchStart( );
    }

    public void EndMatch( )
    {
        if ( OnMatchEnd != null )
            OnMatchEnd( );
    }

    public void ResetMatch( )
    {

    }
    #endregion

    #region GameControl

    void Awake( )
    {
        m_RobotPrefab = GameObject.FindGameObjectWithTag( "Player" );
    }

    void Start( )
    {
        m_GameCamera = Camera.main.gameObject;
        m_CamAnimationComponent = m_GameCamera.GetComponent<Animation>( );
        m_FadeScreenObj = (GameObject)GameObject.Instantiate( m_FadeScreenObj, Vector3.zero, Quaternion.identity );
        m_FadeScreenObj.transform.SetParent( GameObject.FindGameObjectWithTag( "Canvas" ).transform, false );

        //Set countdown timer
        m_CurrentGameTime = 3;

        //Assign player spawn point
        m_PlayerSpawn = GetRandomSpawnPoint( );
        m_PlayerSpawn.m_Owner = SpawnPoint.SpawnOwnerType.PLAYER;

        //Assign bots to spawn point
        for ( int i = 0; i < NumberOfBots + 1; i++ )
        {
            if ( m_SpawnPoints[ i ].m_Owner == SpawnPoint.SpawnOwnerType.NONE ) 
                m_SpawnPoints[ i ].m_Owner = SpawnPoint.SpawnOwnerType.BOT;
        }

        //Do Start-up animation
        m_CamAnimationComponent.Play( "Camera_Intro" );

        m_CamController = m_GameCamera.GetComponent<CameraController>( );
        m_CamController.bLockPosition   = true;
        m_CamController.enabled         = true;


        m_CurrentGameTime = 4.0f;

        StartCoroutine( FadeInScreen( ) );
    }

    SpawnPoint GetRandomSpawnPoint( )
    {
        //Find spawns
        if(m_SpawnPoints == null) m_SpawnPoints = Component.FindObjectsOfType<SpawnPoint>( );

        var spawnToTest = m_SpawnPoints[ Random.Range( 0, m_SpawnPoints.Length - 1 ) ];

        if ( spawnToTest.m_Owner != SpawnPoint.SpawnOwnerType.NONE ) return GetRandomSpawnPoint( );
        else return spawnToTest;
    }

    CameraController m_CamController;
    Text m_BigTimeText;
    bool m_PlayerSpawnToggled = false;
    float m_StartMatchTimer = 1;
    void FixedUpdate( )
    {
        //Wait for camera animation to finish
        if ( !m_RoundStarted && !m_MatchStarted )
        {
            if ( !m_CamAnimationComponent.isPlaying && ScreenAlpha <= 0 && !m_PlayerSpawnToggled )
            {
                if ( m_CamController == null ) m_CamController = m_GameCamera.GetComponent<CameraController>( );

                //Spawn player 
                m_PlayerSpawn.Spawn( );
                m_PlayerSpawnToggled = true;
            }

            //Start countdown and zoom to player
            if ( m_PlayerSpawnToggled && m_PlayerSpawn.m_HasSpawned && !m_MatchStarted )
            {
                m_CurrentGameTime -= Time.fixedDeltaTime;

                //Lerp camera to player location + offsets
                m_GameCamera.transform.position = Vector3.Lerp( m_GameCamera.transform.position, m_CamController.m_CameraToLocation, 0.02f );

                //Lerp Rotation to target rotation
                m_GameCamera.transform.rotation = Quaternion.Lerp( m_GameCamera.transform.rotation, Quaternion.Euler( m_CamController.m_TargetRotation ), 0.02f );

                if ( m_CurrentGameTime <= 1 )
                {

                }
            }

            if ( m_BigTimeText == null )
            {
                m_BigTimeText = ( ( GameObject )GameObject.Instantiate( m_BigTimeTextPrefab, Vector3.zero, Quaternion.identity ) ).GetComponent<Text>( );
                m_BigTimeText.transform.SetParent( GameObject.FindGameObjectWithTag( "Canvas" ).transform, false );
            }

            if ( m_PlayerSpawn != null && m_PlayerSpawn.m_HasSpawned )
            {
                if ( m_CurrentGameTime >= 1.0f )
                {
                    m_BigTimeText.text = Mathf.RoundToInt( m_CurrentGameTime ).ToString( );
                }
                else
                {
                    m_BigTimeText.text = "BATTLE!";

                    m_StartMatchTimer -= Time.fixedDeltaTime;
                    if ( m_StartMatchTimer <= 0 )
                    {
                        StartMatch( );
                    }
                }
            }
            else
                m_BigTimeText.text = "";
        }
    }

    /// <summary>
    /// TERRIBLE IDEA HERE
    /// </summary>
    public static void HasSpawnedPlayer( GameObject _player )
    {
        if ( OnPlayerSpawned != null )
            OnPlayerSpawned( _player );
    }

    IEnumerator FadeInScreen( )
    {
        Image _fadeImg = m_FadeScreenObj.GetComponent<Image>( );
        _fadeImg.color = new Color( 0, 0, 0, 1 );

        while ( _fadeImg.color.a >= 0 )
        {
            Color _c = _fadeImg.color;
            _c.a -= Time.fixedDeltaTime / 3;

            _fadeImg.color = _c;

            ScreenAlpha = _c.a;
            yield return null;
        }

        ScreenAlpha = _fadeImg.color.a;
        yield return null;
    }

    IEnumerator FadeOutScreen( )
    {
        Image _fadeImg = m_FadeScreenObj.GetComponent<Image>( );

        while ( _fadeImg.color.a <= 1 )
        {
            Color _c = _fadeImg.color;
            _c.a += Time.fixedDeltaTime;

            _fadeImg.color = _c;
            ScreenAlpha = _c.a;
            yield return null;
        }

        ScreenAlpha = _fadeImg.color.a;
        yield return null;
    }

    public void TogglePause( )
    {
        m_Paused = !m_Paused;
        Time.timeScale = m_Paused ? 0 : 1;
    }
    #endregion
}
