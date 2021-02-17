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

            //this.flex += (toFlex - flex) * Time.deltaTime * flexSpeed;

            //this.body.velocity = this.speed * this.transform.forward;
            //this.ragDoll.animator.SetBool("running", true);
            //this.ragDoll.animator.SetFloat("flex", flex);
            //this.toFlex = 0.5f;
            
            if (Input.GetKey(KeyCode.W))
            {
                //this.body.velocity = this.speed * this.transform.forward;
                //this.ragDoll.animator.SetBool("running", true);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                //this.body.velocity = Vector3.zero;
                //this.ragDoll.animator.SetBool("running", false);
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                //this.body.angularVelocity = -this.rotationSpeed * Vector3.up;
                //this.toFlex = 1f;
                this.setDirection(-1);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
            	this.setDirection(0);
                //this.body.angularVelocity = Vector3.zero;
            }

            if (Input.GetKey(KeyCode.D))
            {
            	this.setDirection(1);
                //this.body.angularVelocity = this.rotationSpeed * Vector3.up;
                //this.toFlex = 0f;
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
            	this.setDirection(0);
                //this.body.angularVelocity = Vector3.zero;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                //this.tackling = true;
                //this.StartCoroutine(CoTackle());
            }
        }
    }
}
