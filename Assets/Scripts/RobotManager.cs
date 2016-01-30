using UnityEngine;
using System.Collections;

public class RobotManager : MonoBehaviour {

	public Color m_PlayerColor;				
	public Transform m_SpawnPoint;
	[HideInInspector] public int m_PlayerNumber;
	[HideInInspector] public GameObject m_Instance;

	private Movement m_Movement;
	private WeaponInterface m_LeftWeapon;
	private WeaponInterface m_RightWeapon;
	//private AbilityInterface m_Ability;

	public void Setup()
	{

	}
}
