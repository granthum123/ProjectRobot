using UnityEngine;
using System.Collections;

[RequireComponent (typeof(LineRenderer))]

public class LaserWeapon : MonoBehaviour {

	public float laserWidth = 1.0f;
	public float noise = 1.0f;
	public float maxLength = 50.0f;
	public Color color = Color.red;

	LineRenderer lineRenderer;
	int length;
	Vector3[] position;

	Transform myTransform;
	Transform endEffectTranform;

	public ParticleSystem endEffect;
	Vector3 offset;

	// Use this for initialization
	void Start () {

		// Get line renderer component and set the width
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetWidth (laserWidth, laserWidth);
		myTransform = transform;
		offset = new Vector3 (0, 0, 0);
		endEffect = GetComponentInChildren<ParticleSystem> ();
		if (endEffect)
			endEffectTranform = endEffect.transform;
	}
	
	// Update is called once per frame
	void Update () {
		RenderLaser ();
	}

	void RenderLaser()
	{
		UpdateLength ();

		lineRenderer.SetColors (color, color);
		for (int i = 0; i < length; ++i) {
			offset.x = myTransform.position.x + i * myTransform.forward.x + Random.Range (-noise, noise);
			offset.z = i * myTransform.forward.z+Random.Range(-noise,noise)+myTransform.position.z;
			position[i] = offset;
			position[0] = myTransform.position;

			lineRenderer.SetPosition (i, position [i]);
		}
	}

	void UpdateLength()
	{
		// Raycast from shoot location forward
		RaycastHit[] hit;
		hit = Physics.RaycastAll (myTransform.position, myTransform.forward, maxLength);
		for (int i = 0; i < hit.Length; ++i) 
		{
			// hit colliders not triggers
			if (!hit [i].collider.isTrigger) 
			{
				length = (int)Mathf.Round (hit [i].distance) + 2;
				position = new Vector3[length];
				//Move end effect particle to hit position
				if (endEffect) 
				{
					endEffectTranform.position = hit [i].point;
					if (!endEffect.isPlaying) 
					{
						endEffect.Play ();
					}
				}
				lineRenderer.SetVertexCount (length);
				return;
			}
		}

		// No hit dont play effect
		if (endEffect && endEffect.isPlaying) {
			endEffect.Stop ();			
		}

		length = (int) maxLength;
		position = new Vector3[length];
		lineRenderer.SetVertexCount(length);
	}
}
