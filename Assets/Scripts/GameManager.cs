using UnityEngine;
using System.Collections;

public sealed class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public GameObject m_RobotPrefab;				// Robot prefab for players to control
	public bool m_Paused;							// Game state

    public float CurrentGameTime { get; private set; }
    public int PlayerScore { get; private set; }

    //public delegate void StartMatch();
    //public static event StartMatch;

	public void StartMatch()
	{

	}

	public void EndMatch()
	{

	}

	public void TogglePause()
	{
		m_Paused = !m_Paused;
        Time.timeScale = m_Paused ? 0 : 1;
	}

	public void ResetMatch()
	{

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
