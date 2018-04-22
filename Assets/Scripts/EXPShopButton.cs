using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPShopButton : MonoBehaviour {
	public SpellEngine spellEngine;
	public MasterGameManager gameManager;
	public bool isSpellCore = false;
	public string stringKey = "";
	public int cost = 0;
	public GameObject spellCrafterButton;



	// Use this for initialization
	void Awake () {
		  spellEngine = GameObject.FindWithTag("GameController").GetComponent<SpellEngine>();
			gameManager = GameObject.FindWithTag("GameController").GetComponent<MasterGameManager>();
	}

	public void AttemptToBuySpellComponent ()
	{
		if (gameManager.exp >= cost){
			gameManager.exp -= cost;
			if (isSpellCore)
			{
				spellEngine.spellCoreNames.Add(stringKey);
			}
			else
			{
				spellEngine.spellComponentNames.Add(stringKey);
			}

			spellCrafterButton.SetActive(true);

			gameObject.GetComponent<Button>().interactable = false;
		}
	}
}
