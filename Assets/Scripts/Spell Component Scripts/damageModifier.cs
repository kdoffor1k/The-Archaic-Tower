using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifier : SpellComponent {

    // Use this for initialization
    public override void applyAffect(GameObject spell)
    {
        SpellCore spellCoreScript = spell.GetComponent<SpellCore>();
        spellCoreScript.damageMulti *= (float)1.2;

    }
}
