using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PortalSpawning : MonoBehaviour {
    public float delay = 3f;
    public List<GameObject> portals;
    public float limit = 3f; //Change this dynamically to increase the number of portals available
    public List<Vector3> spawnPoints;   // An array of the spawn points this enemy can spawn from.
    public float count = 0f;
    MasterGameManager gameController;
    int indexLevelEnemyList;


    // Use this for initialization
    void Start () {
        gameController = GameObject.FindWithTag("GameController").GetComponent<MasterGameManager>();
        for (int i = 0; i <= 200; i++)
        {
            float angle = UnityEngine.Random.Range(90, 270);
            float radians = angle * Mathf.Deg2Rad;
            float z = 60 * Mathf.Sin(radians);
            float x = 60 * Mathf.Cos(radians);
            Vector3 temp = new Vector3(x, 0, z);
            spawnPoints.Add(temp);
            
        }

        InvokeRepeating("Spawn", delay, delay);
    }
	
	

	void Spawn() {
        if (gameController.nextWave == false)
        {
            return;
        }
        Debug.Log("Entered Spawn in PortalSpawning class.");
        int[] currentLevelEnemyList = new int[gameController.levelPopulation[gameController.currentLevel].Length];
        Array.Copy(gameController.levelPopulation[gameController.currentLevel], currentLevelEnemyList, 
            gameController.levelPopulation[gameController.currentLevel].Length);
        if (count >= limit)
        {
            return;
        }

        
        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Count);

        int portalIndex = UnityEngine.Random.Range(0, currentLevelEnemyList.Length);
        Debug.Log(portalIndex);
        while (currentLevelEnemyList[portalIndex] == 0) {
            portalIndex = UnityEngine.Random.Range(0, currentLevelEnemyList.Length);
        }
        GameObject portal = portals[portalIndex];

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        portal = Instantiate(portal, spawnPoints[spawnPointIndex], Quaternion.identity);
        portal.transform.LookAt(GameObject.FindGameObjectWithTag("Destination").transform);
        count = count + 1;
        spawnPoints.RemoveAt(spawnPointIndex);
        
        currentLevelEnemyList[portalIndex]--;

    }


}
