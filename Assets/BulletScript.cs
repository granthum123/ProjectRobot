using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	
	public Vector3 Velocity;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Transform> ().position += Velocity;
	}
}
