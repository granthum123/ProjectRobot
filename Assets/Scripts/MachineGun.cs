using UnityEngine;
using System.Collections;

public class MachineGun : Weapon {

	public float DamagePerShot = 20.0f;
	public float TimeBetweenBullets = 0.15f;
	public float Range = 100f;

	float timer;
	Ray shootRay;
	RaycastHit shootHit;

	AudioSource gunAudio;			
	Light gunLight;

	void Awake()
	{
		gunAudio = GetComponent<AudioSource> ();
	}

	public override void Fire()
	{
		timer = 0.0f;

		gunAudio.Play ();

		gunLight.enabled = true;

		// Stop and/or start particles

		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

		if (Physics.RayCast(shootRay, out shootHit, range))
		{
			Robot hitRobot = shootHit.collider.GetComponent<Robot> ();

			hitRobot.TakeDamage (DamagePerShot, collide.contacts[0].point);
		}
	}

	public void DisableEffects()
	{
		gunLight.enabled = false;
	}

	void Update()
	{

	}
}
