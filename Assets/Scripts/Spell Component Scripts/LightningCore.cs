using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCore : SpellCore {

	public void doOnCast()
	{
		base.doOnCast();
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.up, out hit))
		{
			Resources.Load("ArrowPrefab", typeof(GameObject));
		}

	}
}
