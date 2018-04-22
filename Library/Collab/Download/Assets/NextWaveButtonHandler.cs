using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWaveButtonHandler : MonoBehaviour {

    public MasterGameManager masterGameManager;
    public Canvas nextWaveCanvas;


    // Use this for initialization
    void Start () {
        masterGameManager = GameObject.FindWithTag("GameController").GetComponent<MasterGameManager>();
    }

    public void nextWaveButtonClicked()
    {
        Debug.Log("Handler for next wave button click!");

        masterGameManager.nextWave = true;
        masterGameManager.nextWaveCanvas.enabled = false;
        masterGameManager.leaveWizardLabButton.interactable = false;
    }

}
