using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class AssembledSpellQuickBar : MonoBehaviour {

  //each element in quickBarSpells is a list of the component names of the assembled spell
  public List<List<string>> quickBarSpells;

  public Dictionary<string, int> tempNameToListIndexForQuickBar;

  public GameObject canvas;

  public GameObject theSpellQuickbar;

  public List<GameObject> uISlots;

  void Awake()
  {

  }


  void Start ()
  {
    //AddNewSpell(new List<string>(new string[1] {"ArrowV1"} ));
    //GameObject cameraGameObject = Camera.main.gameObject;
    //GameObject UIGameObject = cameraGameObject.GetComponentInChildren<GridLayoutGroup>().gameObject;
    Debug.Log("LETS GO" + GetStringSpellList());
    if (quickBarSpells == null)
    {
      quickBarSpells = new List<List<string>>();
      quickBarSpells.Add(new List<string>(new string[] { "ArrowV1"}));
      quickBarSpells.Add(new List<string>(new string[] { "MagicBallV1", "FireV1", "ExplosionV1"}));
      quickBarSpells.Add(new List<string>(new string[] { "ArrowV1", "FireV1", "ExplosionV1"}));
      quickBarSpells.Add(new List<string>(new string[] { "ArrowV1", "FireV1", "ExplosionV1"}));
    }


    //quickBarSpells.Add(new List<string>(new string[] { "ArrowV1", "FireV1", "SizeV1", "SizeV1", "SizeV1", "SizeV1", "MultiplierV1"}));
    //quickBarSpells.Add(new List<string>(new string[] { "ShieldV1" }));
    //quickBarSpells.Add(new List<string>(new string[] { "MeteorShowerV1", "WaterV1" }));

    tempNameToListIndexForQuickBar = new Dictionary<string, int>();
    tempNameToListIndexForQuickBar.Add("Dot", 0);
    tempNameToListIndexForQuickBar.Add("Line", 1);
    tempNameToListIndexForQuickBar.Add("Circle", 2);
    //tempNameToListIndexForQuickBar.Add("Square", 3);
    tempNameToListIndexForQuickBar.Add("Triangle", 3);


    uISlots = new List<GameObject>();
    GameObject CurrentNewestChild = theSpellQuickbar.transform.GetChild(0).gameObject;
    foreach (KeyValuePair<string, int> entry in tempNameToListIndexForQuickBar) {
      if (quickBarSpells.Count == entry.Value)
      {
        quickBarSpells.Add(new List<string>());
      }

      GameObject newerChild = GameObject.Instantiate(CurrentNewestChild, CurrentNewestChild.transform.parent) as GameObject;
      newerChild.transform.localPosition = new Vector3(0,0,0);

      List<string> someList = quickBarSpells[entry.Value];
      CurrentNewestChild.transform.GetChild(0).GetComponent<Text>().text = String.Join("\n",someList.ToArray());
      CurrentNewestChild.GetComponent<Button>().onClick.AddListener(delegate { RemoveSpell(entry.Value); });
      uISlots.Add(CurrentNewestChild);

      CurrentNewestChild = newerChild;

    }
    Destroy(CurrentNewestChild);
    //AddNewSpell(quickBarSpells[0]);
  }


  public void AddNewSpell(List<string> newSpell)
  {
    bool beenPlaced = false;
    foreach (KeyValuePair<string, int> entry in tempNameToListIndexForQuickBar) {
      if (entry.Value == 0)
      {
        continue;
      }
      if (quickBarSpells[entry.Value].Count == 0)
      {
        beenPlaced = true;
        quickBarSpells[entry.Value] = newSpell;
        uISlots[entry.Value].transform.GetChild(0).GetComponent<Text>().text = String.Join("\n",newSpell.ToArray());
        break;
      }
    }
  }


  void Update()
  {
    canvas.transform.position = Camera.main.gameObject.transform.position;
    Vector3 canvasPosition = canvas.transform.position;
    canvasPosition.y = canvasPosition.y - 0.5f;
    if (SceneManager.GetActiveScene().name == "MainScene")
    {
      if (canvasPosition.y < 11.85f)
      {
        canvasPosition.y = 11.85f;

      }
      if (canvasPosition.x < -0.6442835f)
      {
        canvasPosition.x = -0.6442835f;
      }
    }
    if (SceneManager.GetActiveScene().name == "Wizard's Lab")
    {
      canvasPosition.y = canvasPosition.y - 1f;
      if (canvasPosition.y < 0.82f)
      {
        canvasPosition.y = 0.82f;
      }
    }
    canvas.transform.position = canvasPosition;
    Vector3 canvasRotation = canvas.transform.eulerAngles;
    canvasRotation.y = Camera.main.gameObject.transform.eulerAngles.y;
    canvas.transform.eulerAngles = canvasRotation;
  }

  public void RemoveSpell (int index)
  {
    if (index != 0 && SceneManager.GetActiveScene().name == "Wizard's Lab")
    {
      quickBarSpells[index] = new List<string>();
      uISlots[index].transform.GetChild(0).GetComponent<Text>().text = String.Join("\n",quickBarSpells[index].ToArray());
    }

  }


  public string GetStringSpellList()
  {
    string output = "";

    foreach (List<string> someList in quickBarSpells) {
      foreach (string someString in someList) {
        output += someString + ",";
      }
      output = output.TrimEnd(output[output.Length - 1]);
      output += "\n";
    }
      output = output.TrimEnd(output[output.Length - 1]);
      //output = output.TrimEnd(output[output.Length - 1]);
    return output;
  }



}
