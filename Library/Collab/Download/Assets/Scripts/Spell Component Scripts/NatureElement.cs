using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureElement : ElementalComponent {

	public override void applyAffect (GameObject spell)
	{
		spell.GetComponent<Renderer>().material.color = Color.green;
		spell.GetComponent<SpellCore>().elementalAlignment = "Nature";
	}
}
