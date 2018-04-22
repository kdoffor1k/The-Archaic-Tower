using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NewtonVR;

public class EnemyHealth : MonoBehaviour {

    public float startingHealth = 100f;
    public float currentHealth;
    public int goldGained;
    public int expGained;
    public float residualDamage;
    private float damageMultiplier;
    private int rewardMultiplier;
    public MasterGameManager gameManager;
    private Dictionary<string, string> ElementToWeaknesss = new Dictionary<string, string>();
    private Dictionary<string, string> ElementToResistance = new Dictionary<string, string>();
    public NVRHand myHand;
    private int VibrateFrames = 0;


    void setElementalAlignments(Collider col)
    {
        string enemyElement = gameObject.GetComponent<Renderer>().material.name;
        print("Enemy element: " + enemyElement);
        string spellElement = col.GetComponent<SpellCore>().elementalAlignment;
        print("Spell element: " + spellElement);
        enemyElement = enemyElement.Replace(" (Instance)", "");
        string enemyWeakness = ElementToWeaknesss[enemyElement];
        string enemyResistant = ElementToResistance[enemyElement];

        if (spellElement.Equals(enemyWeakness))
        {
            damageMultiplier = 1.5f;
            rewardMultiplier = 2;

        }
        else if (spellElement.Equals(enemyResistant))
        {
            damageMultiplier = 0.5f;
            rewardMultiplier = 1;
        } else
        {
            damageMultiplier = 1;
            rewardMultiplier = 1;
        }
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.GetComponent<SpellCore>() == true)
        {
            setElementalAlignments(col);
            if (col.GetComponent<SpellCore>().isBeam == false)
            {
              myHand.InputDevice.TriggerHapticPulse(2000);
              currentHealth = currentHealth - (100 * col.GetComponent<SpellCore>().damageMulti * damageMultiplier);
              VibrateFrames = 20;

            }




            if (col.GetComponent<SpellCore>().residualDamage == true)
            {
                col.GetComponent<SpellCore>().residualDamage = false;
                residualDamage = residualDamage + 50;

            }
            //DestroyObject(col);

        }

    }

    void OnTriggerStay(Collider col)
    {

        if (col.GetComponent<SpellCore>() != null && col.GetComponent<SpellCore>().isBeam)
        {
            //myHand.LongHapticPulse(1);
            myHand.InputDevice.TriggerHapticPulse(2000);
            VibrateFrames = 20;
            currentHealth = currentHealth - (10 * col.GetComponent<SpellCore>().damageMulti);
        }


    }
    // Use this for initialization
    void Start() {
        if (GameObject.FindWithTag("dominantHand") != null)
        {
          myHand = GameObject.FindWithTag("dominantHand").GetComponent<NVRHand>();
          if (myHand == null)
          {
              Debug.Log("Could not find the dominant hand object");
          }
        }


        //myHand.LongHapticPulse(1);
        //StartCoroutine(LongVibration(1, 4000));
        //Debug.Log("AAAAAAAAAAAAAAAAHHHHH");
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
        ElementToWeaknesss.Add("Fire", "Water");
        ElementToWeaknesss.Add("Water", "Nature");
        ElementToWeaknesss.Add("Nature", "Fire");

        ElementToResistance.Add("Water", "Fire");
        ElementToResistance.Add("Fire", "Nature");
        ElementToResistance.Add("Nature", "Water");
        damageMultiplier = 1;
        rewardMultiplier = 1;

    }

    // Update is called once per frame
    void Update() {
        if (VibrateFrames > 0)
        {
          VibrateFrames -= 1;
          myHand.InputDevice.TriggerHapticPulse(2000);
        }

        if (residualDamage > 0)
        {
            currentHealth = currentHealth - 1;
            residualDamage = residualDamage - 1;
        }
        if (currentHealth <= 0)

        {
            gameManager.addGold(goldGained * rewardMultiplier);
            gameManager.addExp(expGained * rewardMultiplier);

            //add kill effect
            GameObject newPart = Instantiate(Resources.Load("DeathEffect") as GameObject);
            newPart.transform.position = gameObject.transform.position;
            newPart.transform.position += Vector3.up;
            GameObject clone = gameObject;
            Destroy(gameObject);

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Practice Arena"))
            {
                
                GameObject obj = Instantiate(clone);
               
            }
        }

    }




}
