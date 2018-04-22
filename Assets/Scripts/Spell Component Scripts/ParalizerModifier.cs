using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalizerModifier : SpellComponent { 

    public override void applyAffect(GameObject spell)
    {
        SpellCore spellCoreScript = spell.GetComponent<SpellCore>();
        spellCoreScript.paralyzer = true;
    }
  
}
