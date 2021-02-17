using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDoll : MonoBehaviour {
	//public Bone root;
	//public Bone spine;

	public Bone[] bones;
	public Transform seat;
	
	public Animator animator;

	void Update() {
		Bone bone = this.bones[0];
		if (bone.body.isKinematic) {
			//this.transform.position = this.seat.position;
			//this.transform.rotation = this.seat.rotation;
		}
	}

	public void restore() {
		this.transform.parent = this.seat;
		this.transform.localPosition = Vector3.zero;
		this.transform.localRotation = Quaternion.identity;
		foreach(Bone bone in this.bones) {
			bone.restore();
		}
		this.animator.enabled = true;
	}

	public void inertiate(Vector3 velocity) {
		this.animator.enabled = false;
		this.transform.parent = null;
		foreach(Bone bone in this.bones) {
			bone.inertiate(velocity);
		}
	}
}
