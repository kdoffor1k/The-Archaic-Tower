using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSelfDestruct : MonoBehaviour {
    int countDown = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        countDown--;
        if (countDown < 0)
        {
            Destroy(gameObject);
        }
		
	}
}
