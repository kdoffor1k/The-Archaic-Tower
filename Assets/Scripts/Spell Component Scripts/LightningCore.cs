using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCore : SpellCore {


    Vector3 towerPosition = new Vector3(0.3f, 0, 0.2f);
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
		Object lightningPrefab = Resources.Load("PartSysShckwve", typeof(GameObject));

		//if (gameObject.GetComponent<GravityBallEffect>() != null)
		//{
			//new GravityModifier().applyAffect(lightning);
		//}
		if (Physics.Raycast(transform.position, transform.up, out hit))
		{
			print("YOOOOO");
			GameObject lightning = (GameObject) Object.Instantiate(lightningPrefab);
			lightning.transform.position = hit.point;
      //lightning.transform.position = new Vector3(hit.point.x, hit.point.y + 3, hit.point.z);
      Vector3 direction = lightning.transform.position - new Vector3(towerPosition.x, lightning.transform.position.y, towerPosition.z);
      lightning.transform.LookAt(lightning.transform.position + direction, Vector3.up);

			//print("3 ==================================");
		}
		else
		{
			print("COME ON");
			//GameObject.Destroy(lightning);
			//print("4 ==================================");
		}
		//print("5 ==================================");
		GameObject.Destroy(gameObject);
		//print("6 ==================================");
	}
}
