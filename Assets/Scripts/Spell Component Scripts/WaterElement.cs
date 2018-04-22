using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterElement : ElementalComponent {

	public override void applyAffect (GameObject spell)
	{

		spell.GetComponent<SpellCore>().elementalAlignment = "Water";
		Renderer[] renderers = spell.GetComponentsInChildren<Renderer>();
		foreach (Renderer someRenderer in renderers)
		{
			someRenderer.material.color = Color.blue;
		}


	}
}
