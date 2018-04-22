using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveEffect : SpellCore
{

   //ublic GameObject[] EffectsOnCollision;
   //rivate ParticleSystem part;
   //rivate List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
   //rivate ParticleSystem ps;
   //ublic bool UseWorldSpacePosition;
   //ublic float DestroyTimeDelay = 5;
  //public float Offset = 0;

    void Start()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        //rotate gameObject so its looking away from the tower

    }


    void OnParticleCollision(GameObject other)
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAHHHHHHHHHH " + other.name);
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.currentHealth = 0;
        }



    }
}
