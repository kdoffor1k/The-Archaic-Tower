using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionModifier : SpellComponent {

    public override void applyAffect(GameObject spell)
    {
        SpellCore spellCoreScript = spell.GetComponent<SpellCore>();
        Object explosionPrefab = Resources.Load("ExplosionPrefab", typeof(GameObject));
        GameObject explosionBall = (GameObject)Object.Instantiate(explosionPrefab, spell.transform);
        spellCoreScript.explosionDamage = true;
    }
}
