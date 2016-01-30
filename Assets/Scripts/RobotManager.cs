using UnityEngine;
using System.Collections;

[Serializable]
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
		m_LeftWeapon = GetComponent<RobotShooting> ();
	}

	public void DisableControl()
	{
		m_LeftWeapon.enabled = false;
		m_RightWeapon.enabled = false;
	}
}
