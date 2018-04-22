using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    //public PlayerHealth playerHealth;
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float delay = 3f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public float limit = 1f;
    PortalSize getElement;
    public List<Material> materials;
    MasterGameManager gameController;


    private List<GameObject> enemies = new List<GameObject>();


    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<MasterGameManager>();
        if (gameController == null)
        {
            Debug.Log("Reference to gameController could not be found in EnemyManager class");
        }

        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", delay, delay);
        getElement = GetComponentInChildren<PortalSize>();


    }


    void Spawn()
    {
        Debug.Log("Entering Spawn in EnemyManager class");
      

        if (enemies.Count >= limit)
        {
            enemies.RemoveAll(deadEnemy => deadEnemy == null);
            Destroy(transform.GetChild(0).gameObject);
            Destroy(gameObject);
            return;
        }



        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        GameObject obj = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        pickElement(obj);

        enemies.Add(obj);

        EnemyManager.getEnemies().Add(obj);


    }


    public void pickElement(GameObject obj)
    {

        Renderer renderer = obj.GetComponent<Renderer>();
        if (getElement.getElement() == 0)
        {
            //Fire
            renderer.material = materials[getElement.getElement()];
            Light light = obj.GetComponent<Light>();
            light.color = Color.red;


        }
        else if (getElement.getElement() == 1)
        {
            //Nature
            renderer.material = materials[getElement.getElement()];
            Light light = obj.GetComponent<Light>();
            light.color = Color.green;

        }
        else
        {
            //Water
            renderer.material = materials[getElement.getElement()];
            Light light = obj.GetComponent<Light>();
            light.color = Color.blue;

        }
    }



}
