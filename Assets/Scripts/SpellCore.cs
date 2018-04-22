using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class SpellCore : MonoBehaviour {

  MasterGameManager gameManager;
	public float damageMulti = 1.0f;
	public float speedMulti = 1.0f;
  public float livingTime = 100;
	public float livingTimeDecay = 0;
  public bool hasItBeenCastedYet = false;
  public bool animateTextureOffsetY = false;
  public bool animateTextureOffsetX = false;
  public string elementalAlignment = "";
  public Vector3 scale = new Vector3(1,1,1);
  public bool dontDestroyOnImpactWithEnemies = false;

  //bools to keep track on what behavior we want
  //used for beam spell
  public bool isBeam = false;

  //Projectile spells
  public bool isProjectile = false;

  //poison damage
  public bool residualDamage = false;

  //have to throw the object rather then cast it normally
  public bool isThrowable = false;

  //this spell explodes
	public bool explosionDamage = false;

  //paralyzes the enemies
	public bool paralyzer = false;

  public bool gravity = false;






	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<MasterGameManager>();

	}

	// Update is called once per frame
	void Update () {
		if (livingTime < 0)
		{
			Destroy(gameObject);
		}
		if (hasItBeenCastedYet)
		{
			livingTime -= livingTimeDecay;
      if (animateTextureOffsetY)
      {
        Material spellMaterial = GetComponent<Renderer>().material;
        float speed = 0.1f;
        spellMaterial.mainTextureOffset = new Vector2(spellMaterial.mainTextureOffset.x, spellMaterial.mainTextureOffset.y - speed);

      }
      if (animateTextureOffsetX)
      {
        Material spellMaterial = GetComponent<Renderer>().material;
        float speed = 0.05f;
        spellMaterial.mainTextureOffset = new Vector2(spellMaterial.mainTextureOffset.x + speed, spellMaterial.mainTextureOffset.y);

      }
		}





	}

	public void DoOnCast()
	{
		hasItBeenCastedYet = true;
    //legacy
		/*if (gameObject.tag == "Persistent")
		{
			transform.localScale = new Vector3(transform.localScale.x, 10, transform.localScale.z);
			transform.localPosition = new Vector3(0, 0, 10);
		}*/

    if (isBeam)
    {
      transform.localScale = new Vector3(transform.localScale.x, 30, transform.localScale.z);
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 30);
      
    }
    if (isThrowable)
    {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        Vector3 handVelocity = gameManager.rightHandTransform.GetComponent<NVRHand>().GetVelocityEstimation();
        print(handVelocity);
        rigidbody.velocity = handVelocity * -10;
        if (rigidbody.velocity.x > 0)
        {
          rigidbody.velocity = new Vector3(0 - rigidbody.velocity.x, rigidbody.velocity.y, rigidbody.velocity.z);
        }

        rigidbody.useGravity = true;

    }
  }

	public void OnTriggerEnter(Collider other)
	{
      //Debug.Log("on trigger enter called " + gameObject.name + explosionDamage + other.gameObject.name);
    //if you are an exploding spell, set off the explosion
		if (explosionDamage && (other.gameObject.tag == "Ground" || other.gameObject.GetComponent<EnemyHealth>() != null))
		{
      //Debug.Log("passed of statement");
			foreach (Transform child in gameObject.transform) {

        //Debug.Log("child of " + gameObject.name + " is " + child.gameObject.name);

				if (child.GetComponent<ExplosionEffect>() != null)
				{
          //Debug.Log("before explosion called");
					child.GetComponent<ExplosionEffect>().Explode();
          //Debug.Log("explosion called");
				}
			}
		}

    //if you are a paralyzing spell, paralyze the enemies
		if (other.gameObject.GetComponent<EnemyHealth>() != null && paralyzer)
		{
			other.gameObject.GetComponent<EnemyMovement>().nav.speed = 0;
		}

    //handle colliding with other things based on the type of spell it is
		if (other.gameObject.tag == "Ground" && isProjectile)
		{
      //Debug.Log("spell died from Ground object: " + gameObject.name + explosionDamage);
			Destroy(gameObject);
      //Debug.Log("after spell died");
		}
		if (other.gameObject.GetComponent<EnemyHealth>() != null && !dontDestroyOnImpactWithEnemies)
		{
      //Debug.Log("spell died");
			Destroy(gameObject);
      //Debug.Log("after spell died 2");

		}
	}


}
