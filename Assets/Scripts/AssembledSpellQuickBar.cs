using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembledSpellQuickBar : MonoBehaviour {

  //each element in quickBarSpells is a list of the component names of the assembled spell
  public List<List<string>> quickBarSpells;

  public Dictionary<string, int> tempNameToListIndexForQuickBar;

  void Awake()
  {
    quickBarSpells = new List<List<string>>();
    quickBarSpells.Add(new List<string>(new string[] { "ArrowV1", "FireV1" }));
    quickBarSpells.Add(new List<string>(new string[] { "ArrowV1", "FireV1", "SizeV1", "SizeV1", "SizeV1", "SizeV1" }));
    //quickBarSpells.Add(new List<string>(new string[] { "ShieldV1" }));
    quickBarSpells.Add(new List<string>(new string[] { "LightningV1", "WaterV1" }));

    tempNameToListIndexForQuickBar = new Dictionary<string, int>();
    tempNameToListIndexForQuickBar.Add("ArrowV1", 0);
    tempNameToListIndexForQuickBar.Add("BeamV1", 1);
    tempNameToListIndexForQuickBar.Add("ShieldV1", 2);
  }

  void Update()
  {

  }

}
