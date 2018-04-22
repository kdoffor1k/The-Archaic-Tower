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
		if (gameObject.GetComponent<GravityBallEffect>() != null)
		{
			new GravityModifier().applyAffect(lighting);
		}
		if (Physics.Raycast(transform.position, transform.up, out hit))
		{
			print("YOOOOO");
			lighting.transform.position = hit.point;

			//print("3 ==================================");
		}
		else
		{
			print("COME ON");
			GameObject.Destroy(lighting);
			//print("4 ==================================");
		}
		//print("5 ==================================");
		GameObject.Destroy(gameObject);
		//print("6 ==================================");
	}
}
