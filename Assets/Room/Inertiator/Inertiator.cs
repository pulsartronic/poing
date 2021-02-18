using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inertiator : MonoBehaviour {
    public Character character;
    private void OnTriggerEnter(Collider other) {
        LocalCharacter otherCharacter = other.gameObject.GetComponent<LocalCharacter>();
        if (otherCharacter) {
            if (otherCharacter != character) {
                otherCharacter.crash();
            }
        }
    }
}
