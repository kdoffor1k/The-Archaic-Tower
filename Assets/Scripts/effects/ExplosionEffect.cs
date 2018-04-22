using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour {

    public float radius = 5.0F;
    public float power = 10.0F;

    public float lifeLength = 10f;
    private bool exploded = false;

    public void OnTriggerEnter(Collider other)
    {

        //Vector3 explosionPos = transform.position;
        //Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        if (exploded)
        {
            //foreach (Collider hit in colliders)
            //{


                //Rigidbody rb = hit.GetComponent<Rigidbody>();
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    //rb.AddExplosionForce(power, explosionPos, radius);
                    //EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
                    EnemyHealth enemy = other.GetComponent<EnemyHealth>();
                    if (enemy != null)
                    {
                        enemy.currentHealth = enemy.currentHealth - 50;
                    }
                }


            }
    }


    public void Explode ()
    {
      transform.parent = null;

      transform.localScale = new Vector3(radius,radius,radius);
      //if (gameObject.GetComponent<SpellCore>().hasItBeenCastedYet == true)
        //{
            gameObject.GetComponent<ParticleSystem>().Play();
        //}

      exploded = true;
      gameObject.GetComponent<Collider>().enabled = true;

    }

    void Update ()
    {

      if (exploded)
      {
        if (lifeLength > 0)
        {
          lifeLength -= 1;
        }
        else
        {
        
          Destroy(gameObject);
        }
      }
    }



}
