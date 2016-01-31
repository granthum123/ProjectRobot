using UnityEngine;
using System.Collections;

public class RocketLauncher : Weapon
{
	public Rigidbody rocket;
	public float projectileSpeed = 10.0f;

	bool m_Fired;

	public override void Fire()
	{
		m_Fired = true;

		Rigidbody rocketClone = Instantiate (rocket, transform.position, transform.rotation) as Rigidbody;
		rocketClone.velocity = transform.forward * projectileSpeed;

		//RocketProjectile rocketClone = Instantiate (rocket, transform.position, transform.rotation) as RocketProjectile;
		//rocketClone.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
	}
}
