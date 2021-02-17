using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inertiator : MonoBehaviour {
    public Character character;
    private void OnTriggerEnter(Collider other)
    {
        Character otherCharacter = other.gameObject.GetComponent<Character>();
        if (otherCharacter)
        {
            if (otherCharacter != character)
            {
                otherCharacter.crash();
            }
        }
    }
}
