using UnityEngine;
using System.Collections;

public interface WeaponInterface {
	void Fire();
	void Drop();
	void PickUp(GameObject pickup);
}
