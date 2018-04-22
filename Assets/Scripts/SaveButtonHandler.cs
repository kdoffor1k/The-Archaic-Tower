using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButtonHandler : MonoBehaviour {
    public AssembledSpellQuickBar assembledSpellQuickBar;
    public GameObject craftedSlot;
    SpellEngine spellEngine;


	// Use this for initialization
	public void saveAssembledSpell () {
        List<string> newSpell = new List<string>();
        foreach (Transform child in craftedSlot.transform)
        {
            if (child.gameObject.GetComponent<ButtonHandeling>() != null)
            {
                newSpell.Add(child.gameObject.GetComponent<ButtonHandeling>().stringKey);
            }
            else if(child.gameObject.GetComponent<ModButtonHandeling>() != null)
            {
                newSpell.Add(child.gameObject.GetComponent<ModButtonHandeling>().stringKey);
            }

          Destroy(child.gameObject);
        }
        List<string> sortedSpell = new List<string>();

        //find the spell core:
        foreach (string someComponent in newSpell) {
          if (spellEngine.spellCoreNames.Contains(someComponent))
          {
            sortedSpell.Add(someComponent);
            newSpell.Remove(someComponent);
            break;
          }
        }

        //Add all the rest
        foreach (string someComponent in newSpell) {
          sortedSpell.Add(someComponent);
        }

        assembledSpellQuickBar.AddNewSpell(sortedSpell);


    }

	// Update is called once per frame
	void Start () {

        assembledSpellQuickBar = GameObject.FindWithTag("GameController").GetComponent<AssembledSpellQuickBar>();
        spellEngine = GameObject.FindWithTag("GameController").GetComponent<SpellEngine>();
}
}
