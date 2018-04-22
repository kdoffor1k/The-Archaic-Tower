using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour {

    public float radius = 5.0F;
    public float power = 10.0F;

    public void OnTriggerEnter(Collider other)
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        if (transform.parent.GetComponent<SpellCore>().hasItBeenCastedYet)
        {
            foreach (Collider hit in colliders)
            {
                

                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius);
                    EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
                    if(enemy != null)
                    {
                    enemy.currentHealth = enemy.currentHealth - 50;
                    }
                    
            }
        }
    }

      

}
