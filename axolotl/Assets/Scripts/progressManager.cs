using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progressManager : MonoBehaviour {
    List<GameObject> orbs = new List<GameObject>();
    int totalOrbs;
    int currentOrbs = 0;


	// Use this for initialization
	void Start () {
        GameObject[] orbArray = GameObject.FindGameObjectsWithTag("Orb");
        totalOrbs = orbArray.Length;
        for (int i = 0; i < totalOrbs; i++) {
            orbs.Add(orbArray[i]);
        }
    }

    void updateProgress(int deliveredOrb) {
        
		
	}


}
