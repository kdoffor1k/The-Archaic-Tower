using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : ElementalComponent {

	public override void applyAffect (GameObject spell)
	{
		//spell.GetComponent<Renderer>().material.color = Color.red;
		spell.GetComponent<SpellCore>().elementalAlignment = "Fire";
		Renderer[] renderers = spell.GetComponentsInChildren<Renderer>();
		foreach (Renderer someRenderer in renderers)
		{
			someRenderer.material.color = Color.red;
		}
	}
}
