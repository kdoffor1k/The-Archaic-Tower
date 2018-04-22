using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SpellEngine : MonoBehaviour {


	public MasterGameManager masterGameManager;

	//[HideInInspector]
	public List<string> spellComponentNames = new List<string>() /*{"FireV1", "WaterV1", "NatureV1", "DamageV1", "SizeV1", "SpeedV1", "GravityV1", "MultiplierV1","Add1V1","PoisonV1","ExplosionV1","ParalyzeV1","SlowV1"}*/;

	//[HideInInspector]
	public List<string> spellCoreNames = new List<string>() /*{"ArrowV1", "ShieldV1", "BeamV1", "LightningV1", "WallV1" ,"MeteorShowerV1","MagicBallV1","ShockWaveV1","SummonGolemsV1"}*/;


	private Dictionary<string, SpellComponent> spellComponentName2UnityComponent = new Dictionary<string, SpellComponent>();

	private Dictionary<string, GameObject> spellCoreName2PreFab = new Dictionary<string, GameObject>();

	private Queue<string> spellsQueuedUp;

	public bool hasCoreBeenCraftedYet = false;

	public bool hasElementBeenCraftedYet = false;

	public List<GameObject> craftedSpells = new List<GameObject>();

	public bool tester = false;




	void Awake ()
	{
		spellsQueuedUp = new Queue<string>();

		masterGameManager = GameObject.FindWithTag("GameController").GetComponent<MasterGameManager>();

		spellCoreName2PreFab.Add("ArrowV1", (GameObject) Resources.Load("ArrowPrefab", typeof(GameObject)));
		spellCoreName2PreFab.Add("ShieldV1", (GameObject) Resources.Load("ShieldPrefab", typeof(GameObject)));
		spellCoreName2PreFab.Add("BeamV1", (GameObject) Resources.Load("BeamPrefab", typeof(GameObject)));
		spellCoreName2PreFab.Add("LightningV1", (GameObject) Resources.Load("LightningPrefab", typeof(GameObject)));

    spellCoreName2PreFab.Add("WallV1", (GameObject)Resources.Load("WallSpellPrefab", typeof(GameObject)));
    spellCoreName2PreFab.Add("MeteorShowerV1", (GameObject)Resources.Load("MeteorShowerPrefab", typeof(GameObject)));
    spellCoreName2PreFab.Add("MagicBallV1", (GameObject)Resources.Load("MagicBallPrefab", typeof(GameObject)));
    spellCoreName2PreFab.Add("ShockWaveV1", (GameObject)Resources.Load("ShockWavePrefab", typeof(GameObject)));
    spellCoreName2PreFab.Add("SummonGolemsV1", (GameObject)Resources.Load("SummonGolemsPrefab", typeof(GameObject)));


    spellComponentName2UnityComponent.Add("FireV1", new FireElement());
		spellComponentName2UnityComponent.Add("WaterV1", new WaterElement());
		spellComponentName2UnityComponent.Add("NatureV1", new NatureElement());

		spellComponentName2UnityComponent.Add("DamageV1", new DamageModifier());
		spellComponentName2UnityComponent.Add("SizeV1", new SizeModifier());
		spellComponentName2UnityComponent.Add("SpeedV1", new SpeedModifier());
		spellComponentName2UnityComponent.Add("GravityV1", new GravityModifier());

    spellComponentName2UnityComponent.Add("MultiplierV1", new SpellMultiplier());
    spellComponentName2UnityComponent.Add("Add1V1", new GravityModifier());
    spellComponentName2UnityComponent.Add("PoisonV1", new PoisonModifier());
    spellComponentName2UnityComponent.Add("ExplosionV1", new ExplosionModifier());
    spellComponentName2UnityComponent.Add("ParalyzeV1", new ParalizerModifier());
    spellComponentName2UnityComponent.Add("SlowV1", new GravityModifier());

		//craftedSpells = new List<GameObject>();

    }

	// Use this for initialization
	void Start ()
	{
		foreach (string someString in getCompleteSpellPartsNameList())
		{
			//Debug.Log(someString);
		}
		//GameObject.Instantiate(Resources.Load("ArrowPrefab", typeof(GameObject)));
		//GameObject.Instantiate(spellCoreName2PreFab["ArrowV1"]);
	}

	// Update is called once per frame
	void Update ()
	{
		//Debug.Log(String.Join(",",spellCoreNames.ToArray()));
		//testing
		/*if (tester)
		{
			masterGameManager.gestureGameManager.airsigManager.SetClassifier("ElementalComponentsV1", "");
			tester = false;
		}*/



		//Instantiate the next spell in the Queue
		if (spellsQueuedUp.Count > 0)
		{
			string nextSpellComponent = spellsQueuedUp.Dequeue();
			//Debug.Log("1");
			Debug.Log("just Dequeued " + nextSpellComponent);
			if (isProperSpellPart(nextSpellComponent))
			{
				//Debug.Log("2");
				Debug.Log("proper spell part " + nextSpellComponent);
				if (spellCoreNames.Contains(nextSpellComponent))
				{
					Debug.Log("spellcoresNames contains " + nextSpellComponent);
                    //TODO: update the histogram Dictionary in master mage manger
                    int oldCount = masterGameManager.userSpellHistory[nextSpellComponent];
                    masterGameManager.userSpellHistory[nextSpellComponent] = ++oldCount;
										craftSpellCore(nextSpellComponent);
                    //TODO: Update most used spell component
                    //masterGameManager.mostUsedSpellComponent = masterGameManager.userSpellHistory.FirstOrDefault(x => x.Value == masterGameManager.userSpellHistory.Values.Max()).Key;
                    //Debug.Log("3");
                }
				else if (spellComponentNames.Contains(nextSpellComponent))
				{
                    //TODO: update the histogram Dictionary in master mage
                    int oldCount = masterGameManager.userSpellHistory[nextSpellComponent];
                    masterGameManager.userSpellHistory[nextSpellComponent] = ++oldCount;
                    craftSpellComponent(nextSpellComponent);
                    //TODO:Update most used spell component
                    masterGameManager.mostUsedSpellComponent = masterGameManager.userSpellHistory.FirstOrDefault(x => x.Value == masterGameManager.userSpellHistory.Values.Max()).Key;

                    //Debug.Log("4");
                }


            }
		}


		if (masterGameManager.gestureGameManager != null && masterGameManager.gestureGameManager.rightHandControl != null)
		{
			attemptToLaunchSpell();
		}

	}

//masterGameManager.gestureGameManager.rightHandControl


		protected void attemptToLaunchSpell() {

				/*if (null != textToUpdate) {
						uiFeedback = setResultTextForSeconds(textToUpdate, 1.5f, defaultResultText);
						StartCoroutine(uiFeedback);
						textToUpdate = null;
				}*/

				if (-1 != (int)masterGameManager.gestureGameManager.rightHandControl.index && craftedSpells.Count > 0 /*craftedSpells.First() != null*/) {
						var device = SteamVR_Controller.Input((int)masterGameManager.gestureGameManager.rightHandControl.index);
						if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
							foreach (GameObject spell in craftedSpells) {
								//craftedSpell = GameObject.Instantiate(spellCoreName2PreFab[spellName],craftedSpell.transform.position,transform.rotation);\
								if (spell.tag != "Persistent") {
									spell.transform.SetParent (null);
								}
								if (spell.tag == "Projectile") {
									spell.GetComponent<Rigidbody> ().useGravity = true;
									spell.GetComponent<Rigidbody> ().velocity = spell.transform.up * 40f * spell.GetComponent<SpellCore> ().speedMulti;
									spell.GetComponent<Rigidbody> ().isKinematic = false;
								}


								//craftedSpell.GetComponent<SpellCore>().doOnCast();
								spell.SendMessage ("DoOnCast");
								//spell = null;
								//hasCoreBeenCraftedYet = false;
								//hasElementBeenCraftedYet = false;


							}
							craftedSpells = new List<GameObject>();
							hasCoreBeenCraftedYet = false;
							hasElementBeenCraftedYet = false;
							//masterGameManager.gestureGameManager.airsigManager.SetClassifier ("SpellCoresV1", "");

						} else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
								//track.Stop();
						} else if (!device.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
							//track.Stop();
						}
				}
			}



	public void craftSpellCore (string spellName)
	{
		hasCoreBeenCraftedYet = true;
		//masterGameManager.gestureGameManager.airsigManager.SetClassifier("ElementalComponentsV1", "");
		craftedSpells.Add(GameObject.Instantiate(spellCoreName2PreFab[spellName], masterGameManager.nvrPlayer.RightHand.transform));
		//FireElement.applyEffect(craftedSpell);
		//craftSpellComponent("FireV1");
	}

	public void craftSpellComponent (string spellName)
	{

		spellComponentName2UnityComponent[spellName].applyAffect(craftedSpells.First());
		if (spellComponentName2UnityComponent[spellName] is ElementalComponent)
		{
			hasElementBeenCraftedYet = true;
			//masterGameManager.gestureGameManager.airsigManager.SetClassifier("SpellModifiersV1", "");
		}


	}

	public bool isProperSpellPart (string spellName)
	{
		if (hasCoreBeenCraftedYet)
		{
			if (spellComponentNames.Contains(spellName))
			{
				if (spellComponentName2UnityComponent[spellName] is ElementalComponent)
				{
					return hasElementBeenCraftedYet ? false : true;
				}
				else
				{
					return true;
				}
			}
			else
			{
				return false;
			}
		}
		else
		{
			Debug.Log("Core has not been crafted yet " + spellName + ".");
			if (spellCoreNames.Contains(spellName) || spellName.Equals("ArrowV1")){
				return true;
			}

			return false;
		}

	}



	public List<string> getCompleteSpellPartsNameList ()
	{
		List<string> completedList = new List<string>(spellComponentNames.Count + spellCoreNames.Count);
		completedList.AddRange(spellComponentNames);
		completedList.AddRange(spellCoreNames);

		return completedList;
	}


	public void spellPartDetected (string name, float confidence, string source)
	{
		if (masterGameManager.useQuickBarMechanics == true)
		{
			Debug.Log(name);
			int index = masterGameManager.assembledSpellQuickBar.tempNameToListIndexForQuickBar[name];

			Debug.Log("LOOK HEREEEEEEEEEEEEEEEEEEEEEEE " + index);
			foreach (string someSpellComponent in masterGameManager.assembledSpellQuickBar.quickBarSpells[index]) {
				Debug.Log("THIS IS WHAT WE GOT " + someSpellComponent + "||||");
				spellsQueuedUp.Enqueue(someSpellComponent);
			}
			Debug.Log("spell queued up");
		}
		else if (getCompleteSpellPartsNameList().Contains(name) && confidence > 0.9f)
		{
			//GameObject.Instantiate(spellCoreName2PreFab["ArrowV1"]);
			spellsQueuedUp.Enqueue(name);
		}
	}

}
