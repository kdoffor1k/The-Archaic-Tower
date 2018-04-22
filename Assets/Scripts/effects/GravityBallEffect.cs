using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBallEffect : MonoBehaviour {

	// Use this for initialization
	private bool activated = false;
	public GameObject oldParent;
	// Update is called once per frame
	void Update () {
		//transform.localScale = transform.localScale;\
		if (!activated && transform.parent.GetComponent<SpellCore>() != null)
		{
			if (transform.parent.GetComponent<SpellCore>().hasItBeenCastedYet)
			{
				gameObject.GetComponent<ParticleSystem>().Play();
				activated = true;
				oldParent = transform.parent.gameObject;
				transform.parent = null;
				transform.localScale = new Vector3(0.7f,0.7f,0.7f) * oldParent.GetComponent<SpellCore>().scale.x;
			}
		}
		if (activated)
		{
			if (oldParent == null)
			{
					Destroy(gameObject);
			}
			else
			{
				transform.position = oldParent.transform.position;
			}

		}
	}

	public void OnTriggerStay(Collider other)
	{
		//bool bool1 = false;
		//bool bool2 = false;
		//bool bool3 = false;

		//if (transform.parent.GetComponent<SpellCore>().hasItBeenCastedYet)
		//{
			//bool1 = true;

			if(activated && other.GetComponent<SpellCore>() == null)
			{
				//bool2 = true;
				if(other.GetComponent<Rigidbody>() != null)
				{
					//bool3 = true;
					other.GetComponent<Rigidbody>().AddExplosionForce(-100.0f, transform.position, 10.0f);
				}
			}
		//}

		//print(gameObject.name + ", collider " + other.name + ": " + bool1 + ", " + bool2 + ", " + bool3);

	}



}
