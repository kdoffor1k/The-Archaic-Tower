using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeteorCloudEffect : MonoBehaviour {
	public GameObject meteorBallPrefab;
	public int spawningPeriod = 10;
	public int frameTracker = 0;
	public int lifeSpan = 500;
	// Use this for initialization
	void Start () {
		Vector3 position = transform.position;
		position += Vector3.up * 50;
		transform.position = position;
	}

	// Update is called once per frame
	void Update () {
		frameTracker++;
		lifeSpan--;
		if (frameTracker % spawningPeriod == 0)
		{
			System.Random random = new System.Random();
			GameObject meteor = GameObject.Instantiate(meteorBallPrefab, new Vector3( (float) (random.NextDouble() * 14) + transform.position.x - 7, transform.position.y, (float) (random.NextDouble() * 14) + transform.position.z - 7 ),
			new Quaternion());
			meteor.GetComponent<Rigidbody>().velocity = new Vector3(0f,-70f,0f);
			meteor.GetComponent<Rigidbody>().angularVelocity = new Vector3(((float)random.NextDouble() * 2) - 1 , ((float)random.NextDouble() * 2) - 1 , ((float)random.NextDouble() * 2) - 1 ) * 10;
			spawningPeriod = random.Next(10, 25);
			meteor.transform.rotation = new Quaternion((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());

			SpellCore spellCore = meteor.GetComponent<SpellCore>();
			CopySpellCoreValues(spellCore);
			spellCore.hasItBeenCastedYet = true;
			//spellCore.DoOnCast();

			if (spellCore.explosionDamage)
			{
				ExplosionModifier explosionModifier = new ExplosionModifier();
				explosionModifier.applyAffect(spellCore.gameObject);


			}

			if (spellCore.gravity)
			{
				GravityModifier gravityModifier = new GravityModifier();
				gravityModifier.applyAffect(spellCore.gameObject);
			}

			if (spellCore.scale.x > 1)
			{
				Vector3 effectScale = spellCore.gameObject.transform.localScale;
				effectScale.x *= spellCore.scale.x;
				effectScale.y *= spellCore.scale.y;
				effectScale.z *= spellCore.scale.z;
				spellCore.gameObject.transform.localScale = effectScale;
			}

			if (spellCore.elementalAlignment != "")
			{
				Renderer[] renderers = spellCore.gameObject.GetComponentsInChildren<Renderer>();
				Texture newTexture = Resources.Load("Asteroids_Grey") as Texture;
				foreach (Renderer someRenderer in renderers)
				{
					someRenderer.material.SetTexture("_MainTex", newTexture);
					if (spellCore.elementalAlignment == "Water")
					{

						someRenderer.material.color = Color.blue;
					}
					else if (spellCore.elementalAlignment == "Nature")
					{

						someRenderer.material.color = Color.green;

					}
					else if (spellCore.elementalAlignment == "Fire")
					{

					someRenderer.material.color = Color.red;

					}
				}
			}



			frameTracker = 1;
		}

		if (lifeSpan <= 0)
		{
			Destroy(gameObject);
		}
	}

	private void CopySpellCoreValues (SpellCore toBeCopiedTo)
	{
		SpellCore thisSpellCore = gameObject.GetComponent<SpellCore>();
		toBeCopiedTo.damageMulti = thisSpellCore.damageMulti;
		toBeCopiedTo.speedMulti = thisSpellCore.speedMulti;
	  toBeCopiedTo.livingTime = thisSpellCore.livingTime;
		toBeCopiedTo.livingTimeDecay = thisSpellCore.livingTimeDecay;

		toBeCopiedTo.hasItBeenCastedYet = thisSpellCore.hasItBeenCastedYet;
		toBeCopiedTo.animateTextureOffsetY = thisSpellCore.animateTextureOffsetY;
		toBeCopiedTo.animateTextureOffsetX = thisSpellCore.animateTextureOffsetX;
	  toBeCopiedTo.elementalAlignment = thisSpellCore.elementalAlignment;
		toBeCopiedTo.scale = thisSpellCore.scale;
		toBeCopiedTo.dontDestroyOnImpactWithEnemies = thisSpellCore.dontDestroyOnImpactWithEnemies;
	  toBeCopiedTo.isBeam = thisSpellCore.isBeam;
	  toBeCopiedTo.isProjectile = thisSpellCore.isProjectile;
		toBeCopiedTo.residualDamage = thisSpellCore.residualDamage;
		toBeCopiedTo.isThrowable = thisSpellCore.isThrowable;
		toBeCopiedTo.explosionDamage = thisSpellCore.explosionDamage;
		toBeCopiedTo.paralyzer = thisSpellCore.paralyzer;
		toBeCopiedTo.gravity = thisSpellCore.gravity;
	}
}
