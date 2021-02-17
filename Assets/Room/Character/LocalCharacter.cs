using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCharacter : Character {
    void Start() {
        
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
        
        if (Input.GetKey(KeyCode.Space))
        {
            this.beating = true;
        }
    }
}

