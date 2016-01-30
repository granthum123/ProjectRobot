using UnityEngine;
using System.Collections;

public class RobotManager {

	public Color m_PlayerColor;				
	public Transform m_SpawnPoint;
	[HideInInspector] public int m_PlayerNumber;
	[HideInInspector] public GameObject m_Instance;

	//private Movement m_Movement;
	//private AbilityInterface m_Ability;

	public void Setup()
	{
		// Get reference to components
	}

	public void DisableControl()
	{
	}
}
