using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //public PlayerHealth playerHealth;      
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float delay = 3f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public float limit = 3f;
   


    private List<GameObject> enemies = new List<GameObject>();


    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", delay, delay);
        
    }


    void Spawn()
    {
        enemies.RemoveAll(deadEnemy => deadEnemy == null);

        if (enemies.Count >= limit)
        {
            Destroy(transform.GetChild(0).gameObject);
            Destroy(gameObject);
            return;
        }
           
            

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        GameObject obj = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        enemies.Add(obj);
        

    }

 


}
