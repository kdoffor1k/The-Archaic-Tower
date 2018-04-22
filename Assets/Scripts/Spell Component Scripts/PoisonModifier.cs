using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonModifier : SpellComponent {

    public override void applyAffect(GameObject spell)
    {
        SpellCore spellCoreScript = spell.GetComponent<SpellCore>();
        spellCoreScript.residualDamage = true;

    }
}
