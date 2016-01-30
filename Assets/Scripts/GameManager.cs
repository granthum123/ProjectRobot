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

    public GameObject m_RobotPrefab, m_GameCamera, m_FadeScreenObj;

    public bool m_Paused;							// Game state

    public bool m_RoundStarted = false, 
                m_MatchStarted = false;

    public float m_CurrentGameTime { get; private set; }
    public int m_PlayerScore { get; private set; }

    public delegate void MatchEventDelegate( );
    public static event MatchEventDelegate  OnMatchStart,
                                            OnMatchEnd,
                                            OnMatchReset;

    #endregion

    #region Match Control
    public void StartMatch( )
    {
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
    void Start( )
    {
        m_GameCamera = Camera.main.gameObject;
        m_FadeScreenObj = (GameObject)GameObject.Instantiate( m_FadeScreenObj, Vector3.zero, Quaternion.identity );
        m_FadeScreenObj.transform.SetParent( GameObject.FindGameObjectWithTag( "Canvas" ).transform, false );

        //Do Start-up animation
        m_GameCamera.GetComponent<Animation>( ).Play( "Camera_Intro" );

        StartCoroutine( FadeInScreen( ) );
    }

    IEnumerator FadeInScreen( )
    {
        Image _fadeImg = m_FadeScreenObj.GetComponent<Image>( );
        _fadeImg.color = new Color( 0, 0, 0, 1 );

        while ( _fadeImg.color.a > 0 )
        {
            Color _c = _fadeImg.color;
            _c.a -= Time.fixedDeltaTime;

            _fadeImg.color = _c;

            yield return null;
        }
        yield return null;
    }

    IEnumerator FadeOutScreen( )
    {
        Image _fadeImg = m_FadeScreenObj.GetComponent<Image>( );

        while ( _fadeImg.color.a < 1 )
        {
            Color _c = _fadeImg.color;
            _c.a += Time.fixedDeltaTime;

            _fadeImg.color = _c;

            yield return null;
        }

        yield return null;
    }

    public void TogglePause( )
    {
        m_Paused = !m_Paused;
        Time.timeScale = m_Paused ? 0 : 1;
    }
    #endregion
}
