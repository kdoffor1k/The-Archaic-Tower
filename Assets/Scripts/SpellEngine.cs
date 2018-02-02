using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEngine : MonoBehaviour {

	[HideInInspector]
	public MasterGameManager masterGameManager;

	//[HideInInspector]
	private List<string> spellComponentNames = new List<string> {"FireV1", "WaterV1", "NatureV1", "DamageV1", "SizeV1", "SpeedV1"};

	//[HideInInspector]
	private List<string> spellCoreNames = new List<string> {"ArrowV1", "ShieldV1", "BeamV1", "LightningV1"};


	private Dictionary<string, SpellComponent> spellComponentName2UnityComponent = new Dictionary<string, SpellComponent>();

	private Dictionary<string, GameObject> spellCoreName2PreFab = new Dictionary<string, GameObject>();

	private Queue<string> spellsQueuedUp;

	public bool hasCoreBeenCraftedYet = false;

	public bool hasElementBeenCraftedYet = false;

	private GameObject craftedSpell;

	public bool tester = false;



	void Awake ()
	{
		spellsQueuedUp = new Queue<string>();

		spellCoreName2PreFab.Add("ArrowV1", (GameObject) Resources.Load("ArrowPrefab", typeof(GameObject)));
		spellCoreName2PreFab.Add("ShieldV1", (GameObject) Resources.Load("ShieldPrefab", typeof(GameObject)));
		spellCoreName2PreFab.Add("BeamV1", (GameObject) Resources.Load("BeamPrefab", typeof(GameObject)));
		spellCoreName2PreFab.Add("LightningV1", (GameObject) Resources.Load("LightningPrefab", typeof(GameObject)));


		spellComponentName2UnityComponent.Add("FireV1", new FireElement());
		spellComponentName2UnityComponent.Add("WaterV1", new WaterElement());
		spellComponentName2UnityComponent.Add("NatureV1", new NatureElement());

		spellComponentName2UnityComponent.Add("DamageV1", new DamageModifier());
		spellComponentName2UnityComponent.Add("SizeV1", new SizeModifier());
		spellComponentName2UnityComponent.Add("SpeedV1", new SpeedModifier());

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

		//testing
		/*if (tester)
		{
			masterGameManager.gestureGameManager.airsigManager.SetClassifier("ElementalComponentsV1", "");
			tester = false;
		}*/



		//Instantiate the next spell in the Queue
		if (spellsQueuedUp.Count > 0)
		{
			string nextSpell = spellsQueuedUp.Dequeue();
			//Debug.Log("1");
			if (isProperSpellPart(nextSpell))
			{
				//Debug.Log("2");
				if (spellCoreNames.Contains(nextSpell))
				{
					craftSpellCore(nextSpell);
					//Debug.Log("3");
				}
				else if (spellComponentNames.Contains(nextSpell))
				{
					craftSpellComponent(nextSpell);
					//Debug.Log("4");
				}


			}
		}


		if (masterGameManager.gestureGameManager.rightHandControl != null)
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

				if (-1 != (int)masterGameManager.gestureGameManager.rightHandControl.index && craftedSpell != null) {
						var device = SteamVR_Controller.Input((int)masterGameManager.gestureGameManager.rightHandControl.index);
						if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {

								//craftedSpell = GameObject.Instantiate(spellCoreName2PreFab[spellName],craftedSpell.transform.position,transform.rotation);\
								if (craftedSpell.tag != "Persistent")
								{
									craftedSpell.transform.SetParent(null);
								}
								if(craftedSpell.tag == "Projectile")
								{
									craftedSpell.GetComponent<Rigidbody>().useGravity = true;
									craftedSpell.GetComponent<Rigidbody>().velocity = craftedSpell.transform.up * 20f * craftedSpell.GetComponent<SpellCore>().speedMulti;
									craftedSpell.GetComponent<Rigidbody>().isKinematic = false;
								}

								craftedSpell.GetComponent<SpellCore>().hasItBeenCastedYet = true;
								craftedSpell.GetComponent<SpellCore>().doOnCast();
								craftedSpell = null;
								hasCoreBeenCraftedYet = false;
								hasElementBeenCraftedYet = false;
								masterGameManager.gestureGameManager.airsigManager.SetClassifier("SpellCoresV1", "");


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
		masterGameManager.gestureGameManager.airsigManager.SetClassifier("ElementalComponentsV1", "");
		craftedSpell = GameObject.Instantiate(spellCoreName2PreFab[spellName], masterGameManager.nvrPlayer.RightHand.transform);
		//FireElement.applyEffect(craftedSpell);
		//craftSpellComponent("FireV1");
	}

	public void craftSpellComponent (string spellName)
	{
		spellComponentName2UnityComponent[spellName].applyAffect(craftedSpell);
		if (spellComponentName2UnityComponent[spellName] is ElementalComponent)
		{
			hasElementBeenCraftedYet = true;
			masterGameManager.gestureGameManager.airsigManager.SetClassifier("SpellModifiersV1", "");
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
			return spellCoreNames.Contains(spellName) ? true : false;
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
			Debug.Log(index);
			foreach (string someSpellComponent in masterGameManager.assembledSpellQuickBar.quickBarSpells[index]) {
				Debug.Log(someSpellComponent);
				spellsQueuedUp.Enqueue(someSpellComponent);
			}
		}
		else if (getCompleteSpellPartsNameList().Contains(name) && confidence > 0.9f)
		{
			//GameObject.Instantiate(spellCoreName2PreFab["ArrowV1"]);
			spellsQueuedUp.Enqueue(name);
		}
	}

}
