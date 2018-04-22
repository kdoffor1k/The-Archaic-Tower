using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    public int damageCaused;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

  private void OnTriggerStay(Collider other)
  {
      /*print("Collision detected");
      int damage = 0;
      if (other.GetComponent<EnemyDamage>() != null)
      {
          //print("Got Enemy Health Component");
          damage = other.GetComponent<EnemyDamage>().damageCaused;
      }
      if (currentTimeInSeconds % 5 == 0)
      {
          currentHealth -= damage;
          print(currentHealth);

      }*/
  }
}
