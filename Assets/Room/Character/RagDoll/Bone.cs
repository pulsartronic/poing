using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour {
	public Rigidbody body;
	public Collider coll;

	Vector3 initialPosition;
	Quaternion initialRotation;

	void Awake() {
		this.initialPosition = this.transform.localPosition;
		this.initialRotation = this.transform.localRotation;
	}

	public void restore() {
		this.body.isKinematic = true;
		this.coll.enabled = false;
		this.transform.localPosition = this.initialPosition;
		this.transform.localRotation = this.initialRotation;
	}

	public void inertiate(Vector3 velocity) {
		this.body.isKinematic = false;
		this.body.velocity = velocity;
		this.coll.enabled = true;
	}
}
