using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour, WeaponInterface {

	public GameObject bullet;

	// Use this for initialization
	public void Start () {
	}
	
	// Update is called once per frame
	public void Update () {
		if(Input.GetKeyDown(KeyCode.F))
		{
			Fire();
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			Destroy (bullet.gameObject, 0.5f);
		}
	}

	public void Fire()
	{
		Instantiate(bullet);
		Transform currentLoc = GetComponent<Transform>();
		bullet.transform.position = currentLoc.position;
	}

	public void Drop()
	{

	}

	public void PickUp(GameObject pickup)
	{
		Debug.Log ("Object picked up " + pickup.name);
	}
}

