using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressManager : MonoBehaviour {
    List<GameObject> orbs = new List<GameObject>();
    int totalOrbs;
    int deliveredOrbs = 0;
    int carriedOrbs = 0;
    public GameObject carryingText;
    public GameObject deliveredText;

	// Use this for initialization
	void Start () {
        //Get orbs
        GameObject[] orbArray = GameObject.FindGameObjectsWithTag("Orb");
        totalOrbs = orbArray.Length;
        for (int i = 0; i < totalOrbs; i++) {
            orbs.Add(orbArray[i]);
        }
    }


	void Update()
	{
        carryingText.GetComponent<Text>().text = "Carrying " + carriedOrbs;	
        deliveredText.GetComponent<Text>().text = "Delivered " + deliveredOrbs + "/" + totalOrbs;
	}

	//count orbs that have been picked up
	public void grabOrb() {
        carriedOrbs++;
    }

    //count orbs that have been delivered to the deposit
    void depositOrbs(int deliveries) {
        carriedOrbs = 0;
        deliveredOrbs += deliveries;
	}

    //deposit the orbs
    void OnTriggerStay(Collider other) {
        if ((other.tag == "Player") && (carriedOrbs > 0)) {
            if (other.gameObject.GetComponent<playerMovement>().deliverOrbs) {
                depositOrbs(carriedOrbs);
            }
        }
    }
}
