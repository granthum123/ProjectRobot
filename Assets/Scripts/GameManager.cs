using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public GameObject m_RobotPrefab;				// Robot prefab for players to control
	public bool m_Paused;							// Game state

	public RobotManager Player;
	public SpawnPoint[] m_SpawnPoints;

	public delegate void StartMatch();
	public static event StartMatch OnStart; 

	private int RoundNumber;

	public void InitializeMatch()
	{

	}

	public void EndMatch()
	{

	}

	public void Pause()
	{
		m_Paused = true;
		Time.timeScale = 0;
	}

	public void Resume()
	{
		m_Paused = false;
		Time.timeSclae = 1;
	}

	public IEnumerator RoundStarting()
	{

	}

	public IEnumerator RoundPlaying()
	{

	}

	public IEnumerator RoundEnding()
	{

	}

	public IEnumerator GameLoop()
	{
		// Run Roundstart don't return until finished
		yield return StartCoroutine (RoundStarting ());

		// Run until finished playing	
		yield return StartCoroutine (RoundPlaying ());

		yield return StartCoroutine (RoundEnding ());

		StartCoroutine (GameLoop ());			
	}

	// Use this for initialization
	private void Start () 
	{
		m_SpawnPoints = GameObject.FindObjectOfType<SpawnPoint>();	

		StartCoroutine (GameLoop ());
	}

	// Update is called once per frame
	private void Update () {

	}
}
