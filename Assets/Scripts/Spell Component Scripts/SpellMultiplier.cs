using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMultiplier : SpellComponent {

	// Use this for initialization
	public override void applyAffect(GameObject spell)
	{
		SpellEngine spellEngine = GameObject.FindWithTag("GameController").GetComponent<MasterGameManager>().spellEngine;
		Vector3 oldPos = spell.transform.localPosition;
		Vector3 newPos = oldPos + (Vector3.up * ((float)(new System.Random().NextDouble() + 0.1) / 2));
		//GameObject newspell = GameObject.Instantiate(spell, newpos, spell.transform.rotation);
		GameObject newSpell = GameObject.Instantiate(spell, spell.transform.parent);
		newSpell.transform.localPosition = newPos;
		spellEngine.craftedSpells.Add(newSpell);
	}
}
