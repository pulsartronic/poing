using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera : MonoBehaviour {
	public LocalCharacter target;
	public Transform cam;
	public Transform lookAtTarget;
	public float lookAtUp;

    public float up;
    public float back;
	public float distance;
	public float K = 10f;
	
	void Start() {
        //this.target.onRestore += onRestore;
    }

	void onRestore() {
		//this.transform.position = this.target.transform.position - distance * this.target.transform.forward + up * this.target.transform.up;
	}

    void FixedUpdate() {
    	this.lookAtTarget.position = this.target.transform.position + this.lookAtUp * Vector3.up;
        this.cam.LookAt(lookAtTarget);
        
        Vector3 right = Vector3.Cross(Vector3.up, this.target.transform.forward).normalized;
        Vector3 p = this.target.transform.position - back * this.target.transform.forward;
		Vector3 fposition = p + this.up * Vector3.up;
		
		Vector3 planarDistance = this.transform.position - this.target.transform.position;
		planarDistance.y = 0f;
		planarDistance = planarDistance.normalized;
		
        Vector3 distanceVector = fposition + this.distance * planarDistance - this.transform.position;
        Vector3 position = this.K * Time.fixedDeltaTime * distanceVector;
        
        this.transform.position += position;
    }
}


