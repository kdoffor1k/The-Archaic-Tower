using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterElement : ElementalComponent {

	public override void applyAffect (GameObject spell)
	{
		spell.GetComponent<Renderer>().material.color = Color.blue;
	}
}
