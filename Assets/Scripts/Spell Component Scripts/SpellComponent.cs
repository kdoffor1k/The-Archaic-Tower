using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellComponent {

	public virtual void applyAffect (GameObject spell)
	{
		Debug.Log("THIS SHOULD NEVER HAPPENED, SOMEONE IS CALLING SPELL COMPONENT INTERFACE");
	}

}
