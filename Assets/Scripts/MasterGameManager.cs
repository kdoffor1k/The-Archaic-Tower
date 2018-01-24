using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

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



	}
}
