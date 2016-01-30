using UnityEngine;
using System.Collections;

public class RobotShooting : MonoBehaviour, IWeapon {

	public int PlayerNumber = 1;
	public Rigidbody m_Shell;
	public Transform m_FireTransform;
	public float m_MinLaunchForce = 15.0f;
	public float m_MaxLaunchForce = 30.0f;
	public float m_MaxChargeTime = .75f;

	private float m_CurrentLaunchForce;
	private float m_ChargeSpeed;
	private bool m_Fired = false;

	private void OnEnable()
	{
		m_CurrentLaunchForce = m_MinLaunchForce;
	}

	// Use this for initialization
	public void Start () {

		// Charge speed is delta force per charge time
		m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
	}
	
	// Update is called once per frame
	public void Update (){

		// If current force is greater than launch and still haven't fired.
		if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired) {
			m_CurrentLaunchForce = m_MaxLaunchForce;
			Fire ();
		}
	}

	public void Fire()
	{
		m_Fired = true;

		Rigidbody shellInstance = Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation ) as Rigidbody;
		shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

		m_CurrentLaunchForce = m_MinLaunchForce;
	}

	public void Drop()
	{

	}

	public void PickUp(GameObject pickup)
	{
		Debug.Log ("Object picked up " + pickup.name);
	}
}

