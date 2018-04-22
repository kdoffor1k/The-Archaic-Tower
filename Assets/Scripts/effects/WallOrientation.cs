using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOrientation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject tower = GameObject.FindWithTag("tower");
		gameObject.transform.LookAt(tower.transform);
	}

	// Update is called once per frame
	void Update () {

	}
}
