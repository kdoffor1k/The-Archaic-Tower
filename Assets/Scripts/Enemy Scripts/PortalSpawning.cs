using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PortalSpawning : MonoBehaviour {
    public float delay = 3f;
    public List<GameObject> portals;
    public float limit = 3f;
    public List<Vector3> spawnPoints;   // An array of the spawn points this enemy can spawn from.
    public float count = 0f;



   

    // Use this for initialization
    void Start () {

      
        int i;
        for (i = 0; i <= limit; i++)
        {
            float angle = Random.Range(0, 360);
            float radians = angle * Mathf.Deg2Rad;
            float z = 50 * Mathf.Sin(radians);
            float x = 50 * Mathf.Cos(radians);
            Vector3 temp = new Vector3(x, 0, z);
            spawnPoints.Add(temp);
            
        }

        InvokeRepeating("Spawn", delay, delay);
    }
	
	

	void Spawn() {

        if (count == limit)
        {
            return;
        }

        
        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Count);
        int portalIndex = Random.Range(0, portals.Count);
        print(portalIndex);
        GameObject portal = portals[portalIndex];
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(portal, spawnPoints[spawnPointIndex], Quaternion.identity);
        count = count + 1;
        spawnPoints.RemoveAt(spawnPointIndex);

    }
}
