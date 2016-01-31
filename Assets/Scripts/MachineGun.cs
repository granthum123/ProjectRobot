using UnityEngine;
using System.Collections;

public class MachineGun : Weapon {

	public float DamagePerShot = 20.0f;
	public float TimeBetweenBullets = 0.15f;
	public float Range = 100f;

	float timer;
	float effectsDisplayTime = 2.0f;

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

		if (Physics.Raycast(shootRay, out shootHit, Range))
		{
			Robot hitRobot = shootHit.collider.GetComponent<Robot> ();

			hitRobot.TakeDamage (DamagePerShot, shootHit.point);
		}
	}

	public void DisableEffects()
	{
		gunLight.enabled = false;
	}

	void Update()
	{
		timer += Time.deltaTime;

		// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
		if(timer >= TimeBetweenBullets * effectsDisplayTime)
		{
			// ... disable the effects.
			DisableEffects ();
		}
	}
}
