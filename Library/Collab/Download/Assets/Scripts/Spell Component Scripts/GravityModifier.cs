using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityModifier : SpellComponent {

	public override void applyAffect(GameObject spell)
	{
		Object gravityBallPrefab = Resources.Load("GravityBallPrefab", typeof(GameObject));
		spell.GetComponent<SpellCore>().gravity = true;

		GameObject gravityBall = (GameObject) Object.Instantiate(gravityBallPrefab);
		gravityBall.transform.parent = spell.transform;
		gravityBall.transform.localPosition = new Vector3();
		//gravityBall.transform.scale
	}
}




//gameObject.GetComponent<Renderer>().material.name;
