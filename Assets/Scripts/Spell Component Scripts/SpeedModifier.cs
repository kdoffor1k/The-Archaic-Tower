using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModifier : SpellComponent {

    // Use this for initialization
    public override void applyAffect(GameObject spell)
    {
        SpellCore spellCoreScript = spell.GetComponent<SpellCore>();
        if (spellCoreScript.speedMulti == 0)
        {
          spellCoreScript.livingTime *= (float)1.4;
        }
        else
        {
          spellCoreScript.speedMulti *= (float)1.4;
        }


    }
}
