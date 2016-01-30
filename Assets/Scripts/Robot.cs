using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    public GameObject m_HealthBar;
	public GameObject LeftWeapon;
	public GameObject RightWeapon;
    public Color m_RobotColor { get; private set; }
    public float m_MaxHealth { get; private set; }

	float currentHealth;
	CapsuleCollider capsuleCollider;				//Reference to capsule collider component
	AudioSource 	audioSource;					//Reference to audio source

	bool IsDead;

	void Start()
	{
		IsDead = false;		
		InitializeHealthBar ();

		currentHealth = m_MaxHealth;
	}

	void InitializeHealthBar()
	{
		m_HealthBar.transform.SetParent (transform, false);
		Slider bar = m_HealthBar.GetComponent<Slider> ();
		bar.maxValue = m_MaxHealth;
		bar.Set (m_MaxHealth);
	}
		
	void Update()
	{
		m_HealthBar.GetComponent<Slider> ().Set (currentHealth);
	}

	void TakeDamage(float damage, Vector3 hitPos)
	{
		currentHealth -= damage;

		if (currentHealth <= 0.0f) {
			OnDeath ();
		}
	}

	void OnDeath()
	{
		IsDead = true;

		Destroy (gameObject, 2.0f);
	}

    public Color SetRobotColor( Color newColor )
    {
        m_RobotColor = newColor;
        return m_RobotColor;
    }
}
