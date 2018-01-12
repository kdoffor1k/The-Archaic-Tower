using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCore : MonoBehaviour {

	public float damageMulti = 1.0f;
	public float speedMulti = 1.0f;
  //public float sizeMulti = 1.0f;

	public float livingTime = 100;
	public float livingTimeDecay = 0;

	public bool hasItBeenCastedYet = false;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (livingTime < 0)
		{
			Destroy(gameObject);
		}
		if (hasItBeenCastedYet)
		{
			livingTime -= livingTimeDecay;
		}




	}

	public void doOnCast()
	{
		if (gameObject.tag == "Persistent")
		{
			transform.localScale = new Vector3(transform.localScale.x, 10, transform.localScale.z);
			transform.localPosition = new Vector3(0, 0, 10);
		}
	}


}
