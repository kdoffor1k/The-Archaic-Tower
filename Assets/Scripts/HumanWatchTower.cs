using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanWatchTower : MonoBehaviour
{


    public int currentTimeInSeconds;
    public int currentHealth;
    public int maximumHealth;

    public double upgradeHealthMultiplier;
    public int repairHealthIncrement;
    MasterGameManager masterGameManager;
    public Text towerHealthText;
    // Use this for initialization
    void Start()
    {
        masterGameManager = GameObject.FindWithTag("GameController").GetComponent<MasterGameManager>();
        maximumHealth = masterGameManager.towerMaximumHealth;
        currentHealth = maximumHealth;
        upgradeHealthMultiplier = 1.5;
        repairHealthIncrement = 50;
        currentTimeInSeconds = 0;

        InvokeRepeating("timeIncrement", 0.0f, 1.0f);

    }

    private void timeIncrement()
    {
        currentTimeInSeconds += 1;
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

    // Update is called once per frame
    void Update()
    {
      towerHealthText.text = "Tower Health: " + currentHealth;
    }
}
