using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCharacter : Character {
	float initialSpeed;
	
    void Awake() {
        this.transform.position = Random.Range(-7f, 7f) * Vector3.right;
    }
    
    public override void Update() {
    	base.Update();
    	
        if (this.ragDoll.animator.enabled)
        {
            
        }
        
        switch (state) {
			case CharacterState.RUNNING:
				if (Input.GetKey(KeyCode.A)) {
		            this.setDirection(-1);
		        } else if (Input.GetKeyUp(KeyCode.A)) {
		        	this.setDirection(0);
		        }

		        if (Input.GetKey(KeyCode.D)) {
		        	this.setDirection(1);
		        } else if (Input.GetKeyUp(KeyCode.D)) {
		        	this.setDirection(0);
		        }
		        
		        if (Input.GetKeyDown(KeyCode.Space)) {
		        	this.state = CharacterState.PREPARING;
		        	this.StartCoroutine(CoPrepare());
		        }
			break;
		}
    }
    
    public IEnumerator CoPrepare() {
        yield return new WaitForSeconds(0.15f);
        if (this.ragDoll.animator.enabled) {
		    this.state = CharacterState.TACKLING;
			yield return new WaitForSeconds(0.6f);
		    if (this.ragDoll.animator.enabled) {
		        this.state = CharacterState.RAGDOLING;
		        this.StartCoroutine(CoRestore());
		    }
        }
    }

    public IEnumerator CoRestore() {
        yield return new WaitForSeconds(3f);
        this.state = CharacterState.RUNNING;
    }
    
	public void crash() {
        if (this.ragDoll.animator.enabled) {
        	this.state = CharacterState.RAGDOLING;
        }
    }
}

