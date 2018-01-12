using System.Collections.Generic;
using UnityEngine;

using AirSig;
using NewtonVR;



public class GestureGameManager : MonoBehaviour {

    // Callback for receiving signature/gesture progression or identification results
    AirSigManager.OnDeveloperDefinedMatch developerDefined;
    AirSigManager.OnDeveloperDefinedMatch developerDefinedCore;
    AirSigManager.OnDeveloperDefinedMatch developerDefinedElemental;
    AirSigManager.OnDeveloperDefinedMatch developerDefinedModifier;

    [HideInInspector]
		public AirSigManager airsigManager;

    [HideInInspector]
		public SteamVR_TrackedObject rightHandControl;

    [HideInInspector]
		public NVRPlayer nvrPlayer;

    public ParticleSystem track;

    [HideInInspector]
		public SpellEngine spellEngine;

    [HideInInspector]
    public MasterGameManager masterGameManager;



    // Handling developer defined gesture match callback - This is invoked when the Mode is set to Mode.DeveloperDefined and a gesture is recorded.
    // gestureId - a serial number
    // gesture - gesture matched or null if no match. Only guesture in SetDeveloperDefinedTarget range will be verified against
    // score - the confidence level of this identification. Above 1 is generally considered a match
    void HandleOnDeveloperDefinedMatch(long gestureId, string gesture, float score) {
        Debug.Log("gestureId: " + gestureId + ", gesture: " + gesture + ", score: "  + score);
				spellEngine.spellPartDetected(gesture, score, "");
				//textToUpdate = string.Format("<color=cyan>Gesture Match: {0} Score: {1}</color>", gesture.Trim(), score);
    }

    void HandleOnDeveloperDefinedMatchCore(long gestureId, string gesture, float score) {
        Debug.Log("gestureId: " + gestureId + ", gesture: " + gesture + ", score: "  + score + ", CORE");
				spellEngine.spellPartDetected(gesture, score, "CORE");
				//textToUpdate = string.Format("<color=cyan>Gesture Match: {0} Score: {1}</color>", gesture.Trim(), score);
    }

    void HandleOnDeveloperDefinedMatchElemental(long gestureId, string gesture, float score) {
        Debug.Log("gestureId: " + gestureId + ", gesture: " + gesture + ", score: "  + score + ", ELEMNTAL");
				spellEngine.spellPartDetected(gesture, score, "ELEMNTAL");
				//textToUpdate = string.Format("<color=cyan>Gesture Match: {0} Score: {1}</color>", gesture.Trim(), score);
    }

    void HandleOnDeveloperDefinedMatchModifier(long gestureId, string gesture, float score) {
        Debug.Log("gestureId: " + gestureId + ", gesture: " + gesture + ", score: "  + score + ", MODIFIER");
				spellEngine.spellPartDetected(gesture, score, "MODIFIER");
				//textToUpdate = string.Format("<color=cyan>Gesture Match: {0} Score: {1}</color>", gesture.Trim(), score);
    }




    // Use this for initialization
    void Awake() {
        Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);

				spellEngine = gameObject.GetComponent<SpellEngine>();
        airsigManager = gameObject.GetComponent<AirSigManager>();
        // Update the display text
        //textMode.text = string.Format("Mode: {0}", AirSigManager.Mode.DeveloperDefined.ToString());
        //textResult.text = defaultResultText = "Pressing trigger and write symbol in the air\nReleasing trigger when finish";
        //textResult.alignment = TextAnchor.UpperCenter;
        //instruction.SetActive(false);
        //ToggleGestureImage("All");

        // Configure AirSig by specifying target
        developerDefined = new AirSigManager.OnDeveloperDefinedMatch(HandleOnDeveloperDefinedMatch);
        airsigManager.onDeveloperDefinedMatch += developerDefined;
        airsigManager.SetMode(AirSigManager.Mode.DeveloperDefined);
        airsigManager.SetDeveloperDefinedTarget(spellEngine.getCompleteSpellPartsNameList());
        //airsigManager.SetClassifier("GestureProfile1", "");
        airsigManager.SetClassifier("SpellCoresV1", "");

        /*developerDefinedCore = new AirSigManager.OnDeveloperDefinedMatch(HandleOnDeveloperDefinedMatchCore);
        airsigManager.onDeveloperDefinedMatch += developerDefinedCore;
        airsigManager.SetMode(AirSigManager.Mode.DeveloperDefined);
        airsigManager.SetDeveloperDefinedTarget(spellEngine.getCompleteSpellPartsNameList());
        airsigManager.SetClassifier("GestureProfile1", "");*/




        //checkDbExist();

        airsigManager.SetTriggerStartKeys(
            AirSigManager.Controller.RIGHT_HAND,
            SteamVR_Controller.ButtonMask.Trigger,
            AirSigManager.PressOrTouch.PRESS);


        airsigManager.SetTriggerStartKeys(
            AirSigManager.Controller.LEFT_HAND,
            SteamVR_Controller.ButtonMask.Touchpad,
            AirSigManager.PressOrTouch.PRESS);

    }

		void Start()
		{
			//rightHandControl = nvrPlayer.transform.Find("RightHand").GetComponent<SteamVR_TrackedObject>();
		}


    void OnDestroy() {
        // Unregistering callback
        airsigManager.onDeveloperDefinedMatch -= developerDefined;
    }

    void Update() {
			if (rightHandControl == null)
			{
				if (masterGameManager.rightHandTransform != null)
				{
					rightHandControl = masterGameManager.rightHandTransform.GetComponent<SteamVR_TrackedObject>();
					//Debug.Log("SUCCSESS ============================================");
				}
			}
			else
			{
				UpdateUIandHandleControl();
				//Debug.Log("ELSE");
			}

    }


    protected void UpdateUIandHandleControl() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            Application.Quit();
        }
        /*if (null != textToUpdate) {
            uiFeedback = setResultTextForSeconds(textToUpdate, 1.5f, defaultResultText);
            StartCoroutine(uiFeedback);
            textToUpdate = null;
        }*/

        if (-1 != (int)rightHandControl.index) {
            var device = SteamVR_Controller.Input((int)rightHandControl.index);
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
                track.Clear();
                track.Play();
            } else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
                track.Stop();
            } else if (!device.GetPress(SteamVR_Controller.ButtonMask.Trigger)) {
							track.Stop();
						}
        }

        /*if (nextUiAction != null) {
            nextUiAction();
            nextUiAction = null;
        }*/
    }
}
