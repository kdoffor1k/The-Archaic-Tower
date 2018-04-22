using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandeling : MonoBehaviour {
    public GameObject activePrefab;
    public GameObject craftedSlot;
    public bool debugTest = false;
    public string stringKey = "";


    private void Start()
    {
        gameObject.SetActive(false);
        if (GameObject.FindWithTag("GameController").GetComponent<SpellEngine>().spellComponentNames.Contains(stringKey) || GameObject.FindWithTag("GameController").GetComponent<SpellEngine>().spellCoreNames.Contains(stringKey))
        {
            gameObject.SetActive(true);
        }
    }


    public void CloneManager()
    {
        Debug.Log("clicked");
        if (activePrefab != null)
        {
            Destroy(activePrefab);
        }
        else
        {
            DeleteAnyExistingClones();


            //activePrefab = Instantiate(gameObject, new Vector3(2.0F, 2.0F, 2.0F) * 2, new Quaternion()) as GameObject;
            //activePrefab = Instantiate(gameObject, gameObject.transform.parent);
            activePrefab = Instantiate(gameObject, new Vector3(0,0,0), gameObject.transform.rotation, gameObject.transform.parent) as GameObject;;
            activePrefab.transform.localPosition = new Vector3(0,0,0); 
            activePrefab.transform.parent = craftedSlot.transform;


            //activePrefab.GetComponent<ButtonHandeling>().activePrefab = activePrefab;




            //activePrefab.transform.parent = craftedSlot.transform;
            //activePrefab.transform.position = craftedSlot.transform.position;

        }
    }



    public void DeleteAnyExistingClones()
    {



        foreach (Transform child in craftedSlot.transform)
        {
            if (gameObject.tag == child.tag)
            {
                if (child != null)
                {
                    Destroy(child.gameObject);
                }
            }

        }
    }


    // Update is called once per frame
    void Update () {

        if (debugTest == true)
        {
            debugTest = false;
            CloneManager();

            }

        }

    }
