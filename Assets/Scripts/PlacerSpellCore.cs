using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerSpellCore : SpellCore {

	public GameObject effectToBePlaced;
	public bool setOffChildEffects = true;
	public void DoOnCast()
	{

		base.DoOnCast();

		RaycastHit hit;
		GameObject.Destroy(gameObject.GetComponent<Collider>());
		GameObject.Destroy(gameObject.GetComponent<MeshRenderer>());
		GameObject.Destroy(gameObject.GetComponent<MeshFilter>());

		if (Physics.Raycast(transform.position, transform.up, out hit))
		{
			GameObject effect = Instantiate(effectToBePlaced);
			effect.transform.position = hit.point;

			SpellCore effectSpellCore = effect.GetComponent<SpellCore>();
			if (effectSpellCore != null)
			{
				CopySpellCoreValues(effectSpellCore);
				if (effectSpellCore.explosionDamage)
				{
					ExplosionModifier explosionModifier = new ExplosionModifier();
					explosionModifier.applyAffect(effect);
					if (setOffChildEffects)
					{
						effect.GetComponentInChildren<ExplosionEffect>().Explode();
					}


				}

				if (effectSpellCore.gravity)
				{
					GravityModifier gravityModifier = new GravityModifier();
					gravityModifier.applyAffect(effect);
				}

				if (effectSpellCore.scale.x > 1)
				{
					Vector3 effectScale = effect.transform.localScale;
					effectScale.x *= effectSpellCore.scale.x;
					effectScale.y *= effectSpellCore.scale.y;
					effectScale.z *= effectSpellCore.scale.z;
					effect.transform.localScale = effectScale;
				}

				if (effectSpellCore.elementalAlignment != "")
				{
					Renderer[] renderers = effect.GetComponentsInChildren<Renderer>();
					foreach (Renderer someRenderer in renderers)
					{
						if (effectSpellCore.elementalAlignment == "Water")
						{

							someRenderer.material.color = Color.blue;
						}
						else if (effectSpellCore.elementalAlignment == "Nature")
						{

							someRenderer.material.color = Color.green;

						}
						else if (effectSpellCore.elementalAlignment == "Fire")
						{

						someRenderer.material.color = Color.red;

						}
					}
				}
			}
		}

		GameObject.Destroy(gameObject);

	}

	private void CopySpellCoreValues (SpellCore toBeCopiedTo)
	{
		toBeCopiedTo.damageMulti = this.damageMulti;
		toBeCopiedTo.speedMulti = this.speedMulti;
	  toBeCopiedTo.livingTime = this.livingTime;
		toBeCopiedTo.livingTimeDecay = this.livingTimeDecay;
		if (this.setOffChildEffects)
		{
			toBeCopiedTo.hasItBeenCastedYet = this.hasItBeenCastedYet;
		}
		toBeCopiedTo.animateTextureOffsetY = this.animateTextureOffsetY;
		toBeCopiedTo.animateTextureOffsetX = this.animateTextureOffsetX;
	  toBeCopiedTo.elementalAlignment = this.elementalAlignment;
		toBeCopiedTo.scale = this.scale;
		toBeCopiedTo.dontDestroyOnImpactWithEnemies = this.dontDestroyOnImpactWithEnemies;
	  toBeCopiedTo.isBeam = this.isBeam;
	  toBeCopiedTo.isProjectile = this.isProjectile;
		toBeCopiedTo.residualDamage = this.residualDamage;
		toBeCopiedTo.isThrowable = this.isThrowable;
		toBeCopiedTo.explosionDamage = this.explosionDamage;
		toBeCopiedTo.paralyzer = this.paralyzer;
		toBeCopiedTo.gravity = this.gravity;
	}


}
