using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SaveDataWriterReader : MonoBehaviour {

	public MasterGameManager masterGameManager;
	public string textFileName = "SaveData";

	public bool debugger = false;

	public TextAsset textFile;

	private StreamWriter writer;

	private int towerMaxHealthReference;

	/*

		FORMAT IS AS FOLLOWS:

		SPELL CORES
		ELEMENTALS AND MODIFIERS|
		assembledSpellQuickBar (one list per line)|
		GOLD,EXP, TOWER MAX HEAL

	*/

	// Use this for initialization
	void Awake() {
		Resources.UnloadUnusedAssets();
		masterGameManager = GameObject.FindWithTag("GameController").GetComponent<MasterGameManager>();
		textFile = (TextAsset)Resources.Load(textFileName);
		ReadAllData();
	}

	void Start () {
		//Debug.Log(String.Join(",", masterGameManager.spellEngine.spellCoreNames.ToArray()) + "\n");
		//Debug.Log(String.Join(",", masterGameManager.spellEngine.spellComponentNames.ToArray()) + "\n");

	}

	// Update is called once per frame
	void Update () {
		if (debugger)
		{
			debugger = false;
			//writeSpellComponentData();
			masterGameManager.spellEngine.spellCoreNames = new List<string>();
			masterGameManager.spellEngine.spellComponentNames = new List<string>();
			Debug.Log("before");
			Debug.Log(String.Join(",", masterGameManager.spellEngine.spellCoreNames.ToArray()) + "\n");
			Debug.Log(String.Join(",", masterGameManager.spellEngine.spellComponentNames.ToArray()) + "\n");
			readSpellComponentData();
			Debug.Log("after");
			Debug.Log(String.Join(",", masterGameManager.spellEngine.spellCoreNames.ToArray()) + "\n");
			Debug.Log(String.Join(",", masterGameManager.spellEngine.spellComponentNames.ToArray()) + "\n");
		}
	}


	/*public void writeSpellComponentData()
	{

	  writer = new StreamWriter("Assets/Resources/" + textFileName + ".txt");
		string stringToWrite = String.Join(",", masterGameManager.spellEngine.spellCoreNames.ToArray()) + "\n" + String.Join(",", masterGameManager.spellEngine.spellComponentNames.ToArray()) + "|";
		Debug.Log(stringToWrite);
		writer.WriteLine(stringToWrite);
		//Debug.Log("done");
		writer.Close();
	}*/

	public void readSpellComponentData()
	{
		string[] data = textFile.text.Split(new string[] { "|" }, StringSplitOptions.None);
		string[] spellComponents = data[0].Split(new string[] { "\n" }, StringSplitOptions.None);
		string[] spellCores = spellComponents[0].Split(',');
		string[] spellElementsAndModifiers = spellComponents[1].Split(',');

		//Debug.Log(String.Join(" = ", spellCores));
		//Debug.Log(String.Join(" = ", spellElementsAndModifiers));
		//Debug.Log("done");

		masterGameManager.spellEngine.spellCoreNames = new List<string> (spellCores);
		masterGameManager.spellEngine.spellComponentNames = new List<string> (spellElementsAndModifiers);
	}

	public void ReadAllData()
	{
		Debug.Log(textFile.text);
		string[] dataChunks = textFile.text.Split(new string[] { "|" }, StringSplitOptions.None);

		string[] spellComponents = dataChunks[0].Split(new string[] { "\n" }, StringSplitOptions.None);
		string[] spellCores = spellComponents[0].Split(',');
		string[] spellElementsAndModifiers = spellComponents[1].Split(',');
		//masterGameManager.spellEngine.spellCoreNames = new List<string> (spellCores);
		gameObject.GetComponent<SpellEngine>().spellCoreNames = new List<string> (spellCores);

		//masterGameManager.spellEngine.spellComponentNames = new List<string> (spellElementsAndModifiers);
		gameObject.GetComponent<SpellEngine>().spellComponentNames = new List<string> (spellElementsAndModifiers);


		string[] assembledSpells = dataChunks[1].Split(new string[] { "\n" }, StringSplitOptions.None);
		Debug.Log("reading Data");
		List<List<string>> newAssembeledSpellList = new List<List<string>>();
		bool isFirst = true;
		foreach (string someSpell in assembledSpells) {
			Debug.Log(someSpell);
			if (isFirst)
			{
				isFirst = false;
				continue;
			}
			string[] brokenDownSpellParts = someSpell.Split(',');

			List<string> tempList = new List<string>(brokenDownSpellParts);
			newAssembeledSpellList.Add(tempList);
		}
		//masterGameManager.assembledSpellQuickBar.quickBarSpells = newAssembeledSpellList;
		gameObject.GetComponent<AssembledSpellQuickBar>().quickBarSpells = newAssembeledSpellList;
		//Debug.Log(String.Join(" = ", spellCores));
		//Debug.Log(String.Join(" = ", spellElementsAndModifiers));
		//Debug.Log("done");


		//read gold, exp, and tower health

		string[] otherData = dataChunks[2].Split(',');
		masterGameManager.gold = Int32.Parse(otherData[0]);
		masterGameManager.exp = Int32.Parse(otherData[1]);
		masterGameManager.towerMaximumHealth = Int32.Parse(otherData[2]);
		masterGameManager.goldRequiredToUpgrade = Int32.Parse(otherData[3]);

        //Read current level data
        masterGameManager.currentLevel = Int32.Parse(dataChunks[3]);

		/*if (SceneManager.GetActiveScene().name == "MainScene")
    {
			GameObject.FindWithTag("tower").GetComponent<HumanWatchTower>().maximumHealth = Int32.Parse(otherData[2]);
		}*/



	}



	void OnDestroy()
	{
		writer = new StreamWriter("Assets/Resources/" + textFileName + ".txt");
		string stringToWrite = String.Join(",", masterGameManager.spellEngine.spellCoreNames.ToArray()) + "\n" + String.Join(",", masterGameManager.spellEngine.spellComponentNames.ToArray()) + "|";
		Debug.Log("on destroy");
		writer.WriteLine(stringToWrite);
		writer.WriteLine(masterGameManager.assembledSpellQuickBar.GetStringSpellList() + "|");

		//if (SceneManager.GetActiveScene().name == "MainScene")
    //{
		writer.WriteLine(masterGameManager.gold +","+ masterGameManager.exp +","+ masterGameManager.towerMaximumHealth +","+ masterGameManager.goldRequiredToUpgrade +"|");
        writer.WriteLine(masterGameManager.currentLevel + "|");
		//}
		//else
		//{
			//writer.WriteLine(masterGameManager.gold +","+ masterGameManager.exp +","+ towerMaxHealthReference);
		//}

		writer.WriteLine("");
		//Debug.Log("done");
		writer.Close();
		Resources.UnloadAsset(textFile);
		Resources.UnloadUnusedAssets();
		AssetDatabase.Refresh();


	}


}
