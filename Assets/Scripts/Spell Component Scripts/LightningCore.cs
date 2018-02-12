using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCore : SpellCore {

	public void DoOnCast()
	{
		//print("0 ==================================");
		base.DoOnCast();
		//print("1 ==================================");
		RaycastHit hit;
		GameObject.Destroy(gameObject.GetComponent<Collider>());
		GameObject.Destroy(gameObject.GetComponent<MeshRenderer>());
		GameObject.Destroy(gameObject.GetComponent<MeshFilter>());
		//print("2 ==================================");
		Object lightingPrefab = Resources.Load("LightningStrike", typeof(GameObject));
		GameObject lighting = (GameObject) Object.Instantiate(lightingPrefab);
		if (Physics.Raycast(transform.position, transform.up, out hit))
		{

			lighting.transform.position = hit.point;

			//print("3 ==================================");
		}
		else
		{
			GameObject.Destroy(lighting);
			//print("4 ==================================");
		}
		//print("5 ==================================");
		GameObject.Destroy(gameObject);
		//print("6 ==================================");
	}
}
