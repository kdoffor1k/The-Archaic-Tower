using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShowerCore : SpellCore {

	public void DoOnCast()
	{
			//print("0 ==================================");
			base.DoOnCast();
			//print("1 ==================================");
			RaycastHit hit;


			//print("2 ==================================");
			GameObject prefab = (GameObject) Resources.Load("MeteorShowerCloud", typeof(GameObject));

			GameObject meteorShowerCloud = GameObject.Instantiate(prefab);
			if (gameObject.GetComponent<GravityBallEffect>() != null)
			{
					new GravityModifier().applyAffect(meteorShowerCloud);
			}
			if (Physics.Raycast(transform.position, transform.up, out hit))
			{

					meteorShowerCloud.transform.position = new Vector3(hit.point.x, meteorShowerCloud.transform.position.y, hit.point.z);
							//print("3 ==================================");

			}
			else
			{
					print("COME ON");
					GameObject.Destroy(meteorShowerCloud);
					//print("4 ==================================");
			}
			//print("5 ==================================");
			GameObject.Destroy(gameObject);
			//print("6 ==================================");
	}
}
