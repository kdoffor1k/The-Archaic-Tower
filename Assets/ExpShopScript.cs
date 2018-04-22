using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExpShopScript : MonoBehaviour {
    public SpellEngine spellEngine;
    public GameObject spellCrafter;
    //public Dictionary<string, SpellComponent> spellComponentName2UnityComponent = new Dictionary<string, SpellComponent>();

    //public bool coreTest = false;
    //public bool elementalAndModiferTest = false;

    // Use this for initialization
    void Start () {
        spellEngine = GameObject.FindWithTag("GameController").GetComponent<SpellEngine>();

        /*spellComponentName2UnityComponent.Add("FireV1", new FireElement());
        spellComponentName2UnityComponent.Add("WaterV1", new WaterElement());
        spellComponentName2UnityComponent.Add("NatureV1", new NatureElement());

        spellComponentName2UnityComponent.Add("DamageV1", new DamageModifier());
        spellComponentName2UnityComponent.Add("SizeV1", new SizeModifier());
        spellComponentName2UnityComponent.Add("SpeedV1", new SpeedModifier());
        spellComponentName2UnityComponent.Add("GravityV1", new GravityModifier());

        spellComponentName2UnityComponent.Add("MultiplierV1", new GravityModifier());
        spellComponentName2UnityComponent.Add("Add1V1", new GravityModifier());
        spellComponentName2UnityComponent.Add("PoisonV1", new GravityModifier());
        spellComponentName2UnityComponent.Add("ExplosionV1", new GravityModifier());
        spellComponentName2UnityComponent.Add("ParalyzeV1", new GravityModifier());
        spellComponentName2UnityComponent.Add("SlowV1", new GravityModifier());*/




    }

    public void LearnNewCore(string coreString)
    {
        //string[] stringArray = CSV.Split(',');
        //print(stringArray[0]);
        //print(stringArray[1]);
        //spellEngine.spellCoreName2PreFab.Add(stringArray[0], (GameObject)Resources.Load(stringArray[1], typeof(GameObject)));
        //TODO: have the string be just the name of the spell core and add it to spellEngine.spellCoreNames
        //spellEngine.spellCoreNames.Add(coreString);

    }

    public void LearnElementsAndModifiers(string componentString)
    {
        //TODO: have the key be added to spellEngine.spellComponentNames
        //spellEngine.spellComponentName2UnityComponent.Add(key, spellComponentName2UnityComponent[key]);
        spellEngine.spellComponentNames.Add(componentString);
    }


    public void MakeButtonNotInteractable (Button thisButton)
    {
      thisButton.interactable = false;
    }

    public void EnableButton (GameObject spellCrafterButton)
    {
      spellCrafterButton.SetActive(true);
    }



	// Update is called once per frame
	void Update () {
        /*if (coreTest)
        {
            coreTest = false;
            learnNewCore("ArrowV1,ArrowPrefab");
        }*/
	}
}
