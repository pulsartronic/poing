using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState : int {
	RUNNING,
	PREPARING,
	TACKLING,
	RAGDOLING
}

public class Character : MonoBehaviour {
    public Rigidbody body;
    public float speed;
    public float rotationSpeed;

    public RagDoll ragDoll;

    public float flex = 0.5f;
    public float flexSpeed;
    public float startFlex;
    public float toFlex;

	public CharacterState state = CharacterState.RUNNING;
	
    //public bool tackling = false;
	public int direction;
	public bool beating = false;
	
	public virtual void Update() {
		switch (state) {
			case CharacterState.RUNNING:
				this.running();
			break;
			case CharacterState.PREPARING:
				this.preparing();
			break;
			case CharacterState.TACKLING:
				this.tackling();
			break;
			case CharacterState.RAGDOLING:
				this.ragdoling();
			break;
			
		}
	}
	
	void running() {
		if (!this.ragDoll.animator.enabled) {
			this.ragDoll.restore();
		}
		
		this.flex += (toFlex - flex) * Time.deltaTime * flexSpeed;
        this.body.velocity = this.speed * this.transform.forward;
        this.ragDoll.animator.SetBool("running", true);
        this.ragDoll.animator.SetBool("tackling", false);
        this.ragDoll.animator.SetFloat("flex", flex);
        this.body.angularVelocity = direction * this.rotationSpeed * Vector3.up;
        this.toFlex = (1 + direction) / 2f;
	}
	
	void preparing() {
		this.ragDoll.animator.SetBool("tackling", true);
        this.body.velocity = Vector3.zero;
	}
	
	void tackling() {
		this.ragDoll.animator.SetBool("tackling", true);
		this.body.velocity = 2f * this.speed * this.transform.forward;
	}
	
	void ragdoling() {
		this.body.velocity = Vector3.zero;
		this.direction = 0;
		if (this.ragDoll.animator.enabled) {
			this.ragDoll.inertiate(this.body.velocity);
		}
	}
	
	public void setDirection(int direction) {
		this.direction = direction;
	}
}
