using UnityEngine;
using System.Collections;

public interface IWeapon {
	void Fire();
	void Drop();
	void PickUp(GameObject pickup);
}
