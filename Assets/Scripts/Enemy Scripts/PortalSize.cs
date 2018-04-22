using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSize : MonoBehaviour {
    private float X, Y, Z;
    Transform player;
    public List<Material> materials;
    private int element;
    public GameObject Golem;
    public GameObject Rhino;
    public GameObject Drone;
    MasterGameManager gameController;
    int mostFrequentlyUsedSpellType;



    void Start () {
        gameController = GameObject.FindWithTag("GameController").GetComponent<MasterGameManager>();
        if (gameController == null)
        {
            Debug.Log("GameController not found in PortalSize.");
        }
        if (gameController.mostUsedSpellComponent.Equals("Fire")) {
            mostFrequentlyUsedSpellType = 0;
        } else if (gameController.mostUsedSpellComponent.Equals("Nature")){
            mostFrequentlyUsedSpellType = 1;
        } else if (gameController.mostUsedSpellComponent.Equals("Water")){ 
            mostFrequentlyUsedSpellType = 2;
        } else
        {
            mostFrequentlyUsedSpellType = UnityEngine.Random.Range(0, 3); 
        }
        //This is random right now. We need to make it dynamic given the player's histogram
        pickElement();
        
        player = GameObject.FindGameObjectWithTag("Destination").transform;
        float X = GetComponentInParent<EnemySpawning>().enemy.transform.localScale.x;
        float Y = GetComponentInParent<EnemySpawning>().enemy.transform.localScale.y;
        float Z = GetComponentInParent<EnemySpawning>().enemy.transform.localScale.z;
        float X2 = X;
        // calculates the x and y scale for portal based on enemy size
        if (GetComponentInParent<EnemySpawning>().enemy == Golem)
        {
            X = X * 6;
            Y = Y * 6;
        }
        else if (GetComponentInParent<EnemySpawning>().enemy == Rhino)
        {
            X = X / 6;
            Y = Y / 5;
        }
        else if (GetComponentInParent<EnemySpawning>().enemy == Drone)
        {
            
            Y = Y * 2;
        }
        else
        {
            X = X * 2;
            Y = Y * 3;
        }
        
        //creates portal
        transform.localScale = new Vector3(X, Y, .1f);
        //adjusts portal postioning based on its personal size
        transform.localPosition = new Vector2(0, Y / 2);
        
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);

        //pushes the portal X2/2 units away from portal to give appearance of moving through portal
        if (GetComponentInParent<EnemySpawning>().enemy == Golem)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, X2 / 2 + 3f);
        }
        else if (GetComponentInParent<EnemySpawning>().enemy == Rhino)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, X2 / 5 - 5f);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, X2 / 2 + .1f);
        }

    }
	
    public void pickElement()
    {
        element = probabilisticChooseEnemyType();
        Renderer renderer = GetComponent<Renderer>();
        if (element == 0)
        {
            //Fire
            renderer.material = materials[element];
        }
        else if (element == 1)
        {
            //Nature
            renderer.material = materials[element];
        }
        else
        {
            //Water
            renderer.material = materials[element];
        } 
    }

    private int probabilisticChooseEnemyType()
    {
        int guess = UnityEngine.Random.Range(0, 100);
        if (mostFrequentlyUsedSpellType == 0)
        {
            Debug.Log("Most used spell is Fire. Determining enemy type.");
            if (guess > 50)
            {
                return UnityEngine.Random.Range(0, 3);
            } else
            {
                return 2; //return water element type
            }
        } else if (mostFrequentlyUsedSpellType == 1)
        {
            Debug.Log("Most used spell is Nature. Determining enemy type.");
            if (guess > 50)
            {
                return UnityEngine.Random.Range(0, 3);
            }
            else
            {
                return 0; //return fire element type
            }
        } else if (mostFrequentlyUsedSpellType == 2)
        {
            Debug.Log("Most used spell is Water. Determining enemy type.");
            if (guess > 50)
            {
                return UnityEngine.Random.Range(0, 3);
            }
            else
            {
                return 1; //return nature element type
            }
        } else
        {
            return UnityEngine.Random.Range(0, 3);
        }
    }

    public int getElement()
    {
        return element;
    }

	
}
