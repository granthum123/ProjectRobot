using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int StartingHealth = 100;
	public int ScoreValue = 10;
	public float SinkSpeed = 2.5f;
	public AudioClip DeathClip;

	int currentHealth;
	AudioSource enemyAudio; 			// Reference to audio source to play on hit
	ParticleSystem onHitParticle; 		// Reference to particle system to play on hit
	CapsuleCollider capsuleCollider; 	// Reference to capsule collider
	bool IsDead;
	bool IsSinking;

	void Start()
	{
		enemyAudio = GetComponent<AudioSource> ();
		onHitParticle = GetComponent<ParticleSystem> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();

		currentHealth = StartingHealth;
	}

	// Update is called once per frame
	void Update () {
		if (IsSinking) {
			transform.Translate (-Vector3.up * SinkSpeed * Time.deltaTime);
		}
	}

	void TakeDamage(int amount, Vector3 HitPoint)
	{
		if (IsDead) {
			//already dead no damage taken
			return;
		}

		// Play hit audio
		enemyAudio.Play ();
		// Take damage
		currentHealth -= amount;

		onHitParticle.transform.position = HitPoint;

		onHitParticle.Play ();

		if (currentHealth <= 0) {
			Death ();
		}
	}

	void Death()
	{
		IsDead = true;
		// turn collider into trigger so shots pass through it
		capsuleCollider.isTrigger = true;

		enemyAudio.clip = DeathClip;
		enemyAudio.Play ();

		StartSinking ();
	}

	void StartSinking()
	{
		GetComponent<NavMeshAgent> ().enabled = false;

		GetComponent<Rigidbody> ().isKinematic = true;

		IsSinking = true;

		//ScoreManager.score += score;

		Destroy (gameObject, 2.0f);
	}
}
