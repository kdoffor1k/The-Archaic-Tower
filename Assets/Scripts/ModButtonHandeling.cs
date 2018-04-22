using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]

public class ModButtonHandeling : MonoBehaviour {

    //public Button spellButton;
   // public Event GetEvent;
    //public GameObject CraftedSpell;
    //public GameObject toCopy;
    public GameObject craftedSlot;
    public GameObject newClone;
    public string stringKey = "";
    public GameObject arrow;

    //  public GameObject activePrefab;

    public bool debugTest = false;


    void Start()
    {
        /*matster game manger. spell list .contains stringKey
        if not
        gameObject.SetActive( false);*/

        gameObject.SetActive(false);
        if (GameObject.FindWithTag("GameController").GetComponent<SpellEngine>().spellComponentNames.Contains(stringKey)) {
            gameObject.SetActive(true);
        }



    }


    public void CreateClone()
    {
        Debug.Log("clicked");
        if (newClone != null)
        {
            Destroy(newClone);
        }
        else
        {
          newClone = GameObject.Instantiate(gameObject, gameObject.transform.parent) as GameObject;
          newClone.transform.localPosition = new Vector3(0,0,0);
          newClone.GetComponent<ModButtonHandeling>().newClone = newClone;

          newClone.transform.parent = craftedSlot.transform;
          newClone = null;
        }

        //newClone.transform.position = craftedSlot.transform.position;


    }


    // Use this for initialization
    //void Start () {
    // Button btn = button.GetComponent<Button>();
    //  btn.onClick.AddListener(CreateClone);

    // }

    private void Update()
    {
        if (debugTest== true)
        {
            debugTest = false;

              CreateClone();

        }
    }


}
