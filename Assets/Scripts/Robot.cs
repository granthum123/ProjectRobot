using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    public GameObject m_HealthBar;
	public Weapon LeftWeapon;					// Prefab template for editor usage
	public Weapon RightWeapon;
    public Color m_RobotColor { get; private set; }
	public float m_MaxHealth;

	public Vector3 LeftWeaponPos;
	public Vector3 RightWeaponPos;

	float currentHealth;
	CapsuleCollider capsuleCollider;				// Reference to capsule collider component
	AudioSource 	audioSource;					// Reference to audio source
	Weapon 		m_LeftWeapon;						// Reference to created weapon game objects
	Weapon 		m_RightWeapon;

	bool IsDead;

	void Start()
	{
		IsDead = false;		
		InitializeHealthBar ();
		InitializeWeapons ();

		currentHealth = m_MaxHealth;
	}

	void InitializeHealthBar()
	{
		m_HealthBar.transform.SetParent (transform, false);
		Slider bar = m_HealthBar.GetComponent<Slider> ();
		bar.maxValue = m_MaxHealth;
		bar.value = m_MaxHealth;
	}

	void InitializeWeapons()
	{
		m_LeftWeapon = Instantiate (LeftWeapon) as Weapon;
		m_LeftWeapon.transform.parent = transform;
		m_LeftWeapon.transform.localPosition = LeftWeaponPos;

		m_RightWeapon = Instantiate (RightWeapon) as Weapon;
		m_RightWeapon.transform.parent = transform;
		m_RightWeapon.transform.localPosition = RightWeaponPos;
	}
		
	void Update()
	{
		m_HealthBar.GetComponent<Slider> ().value = currentHealth;
	}

	public void TakeDamage(float damage, Vector3 hitPos)
	{
		currentHealth -= damage;

		if (currentHealth <= 0.0f) {
			OnDeath ();
		}
	}

	public void LeftWeaponFire()
	{
		Debug.Log ("Left weapon fired");

		if (LeftWeapon)
			m_LeftWeapon.GetComponent<Weapon> ().Fire ();
	}

	public void RightWeaponFire()
	{
		Debug.Log ("Right weapon fired");

		if (RightWeapon)
			m_RightWeapon.GetComponent<Weapon>().Fire();
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
