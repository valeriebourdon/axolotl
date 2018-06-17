using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbs : MonoBehaviour {
    
    //pick up the orbs
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            GameObject.Find("Deposit").GetComponent<progressManager>().grabOrb();
            Destroy(this.gameObject);
        }
    }
}
