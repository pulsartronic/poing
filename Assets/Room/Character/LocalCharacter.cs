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
        if (this.ragDoll.animator.enabled && !this.tackling)
        {
            if (Input.GetKey(KeyCode.A))
            {
                this.setDirection(-1);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
            	this.setDirection(0);
            }

            if (Input.GetKey(KeyCode.D))
            {
            	this.setDirection(1);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
            	this.setDirection(0);
            }
        }
        
        this.beating = Input.GetKey(KeyCode.Space);
    }
}

