using UnityEngine;
using System.Collections;

public class RocketProjectile : MonoBehaviour {

	public float damage = 10.0f;
	public float m_MaxLifeTime = 3.0f;

	// Use this for initialization
	void Start () {
		//Destroy after life time if not already destroyed
		Destroy (gameObject, m_MaxLifeTime);
	}

	void OnCollisionEnter(Collision collision)
	{
		ContactPoint contact = collision.contacts [0];

		GameObject collided = collision.gameObject;

		if ( collided.GetComponent<Robot>()) {
			collided.GetComponent<Robot>().TakeDamage (damage, contact.point);
		}

	}
}
