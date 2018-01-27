using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;
using UnityEngine.UI;

public class MasterGameManager : MonoBehaviour {

	[HideInInspector]
	public GestureGameManager gestureGameManager;

	[HideInInspector]
	public NVRPlayer nvrPlayer;

	[HideInInspector]
	public SpellEngine spellEngine;

	[HideInInspector]
	public Transform rightHandTransform;

	[HideInInspector]
	public Transform LeftHandTransform;

	[HideInInspector]
	public AssembledSpellQuickBar assembledSpellQuickBar;

	public bool useQuickBarMechanics = false;

    public Text goldText;
    public int gold;

    public Text expText;
    public int exp;


    void Awake ()
	{
		nvrPlayer =  GameObject.FindWithTag("Player").GetComponent<NVRPlayer>();

		gestureGameManager = GetComponent<GestureGameManager>();
		gestureGameManager.nvrPlayer = nvrPlayer;
		gestureGameManager.masterGameManager = this;
		assembledSpellQuickBar = GetComponent<AssembledSpellQuickBar>();

		spellEngine = GetComponent<SpellEngine>();
		spellEngine.masterGameManager = this;


	}

	// Use this for initialization
	void Start () {
        gold = 100;
        goldText = GameObject.FindWithTag("canv").transform.GetChild(0).GetComponent<Text>();
        expText = GameObject.FindWithTag("canv").transform.GetChild(1).GetComponent<Text>();
        exp = 0;

    }

	// Update is called once per frame
	void Update () {

		//since the controllers could be turned off and on, make sure we still got hands
		if (rightHandTransform == null)
		{
			rightHandTransform = nvrPlayer.transform.Find("RightHand");
		}

		if (LeftHandTransform == null)
		{
			LeftHandTransform = nvrPlayer.transform.Find("LeftHand");
		}
        goldText.text = "Gold: " + gold;
        expText.text = "Exp: " + exp;
    }

    public void addGold(int newGold)
    {
        gold += newGold;
    }


    public void addExp(int newExp)
    {
        exp += newExp;
    }
}
