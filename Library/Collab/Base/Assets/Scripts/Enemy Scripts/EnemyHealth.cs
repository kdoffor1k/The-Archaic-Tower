using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float startingHealth = 100f;
    public float currentHealth;
    public int goldGained;
    public int expGained;
    public MasterGameManager gameManager;


    void OnTriggerEnter(Collider col)
    {

        if (col.GetComponent<SpellCore>() == true)
        {
            if (col.tag == "Projectile")
            {
                currentHealth = currentHealth - (100 * col.GetComponent<SpellCore>().damageMulti);
                DestroyObject(col);
            }
            if (col.tag == "Lightning")
            {
                currentHealth = currentHealth - (100 * col.GetComponent<SpellCore>().damageMulti);
                col.GetComponent<SpellCore>().damageMulti = 0;
            }
        }

    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Persistent" && col.GetComponent<SpellCore>() == true)
        {
            currentHealth = currentHealth - (10 * col.GetComponent<SpellCore>().damageMulti);
        }


    }
    // Use this for initialization
    void Start() {
        currentHealth = startingHealth;
        goldGained = 100;
        expGained = 10;
        GameObject gameManagerObject = GameObject.FindWithTag("GameController");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<MasterGameManager>();
        }
        else
        {
            Debug.Log("Cannot find gameManager.");
        }

    }

    // Update is called once per frame
    void Update() {

        if (currentHealth <= 0)
        {
            gameManager.addGold(goldGained);
            gameManager.addExp(expGained);
            Destroy(gameObject);
        }

    }


    /*void DecreaseHealth(Collider col)
    {
        // currenthealth = currenthealth - col.damage;
        currentHealth = currentHealth - 50;
    }
    */

}
