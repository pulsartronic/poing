using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACharacter : MonoBehaviour {
    public Character character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (character.ragDoll.animator.enabled && !character.tackling)
        {
            character.flex += (character.toFlex - character.flex) * Time.deltaTime * character.flexSpeed;

            character.body.velocity = character.speed * this.transform.forward;
            character.ragDoll.animator.SetBool("running", true);
            character.ragDoll.animator.SetFloat("flex", character.flex);
            //character.toFlex = 0.5f;

            character.body.angularVelocity = -character.rotationSpeed * Vector3.up;
            character.toFlex = 1f;
        }
    }
}
