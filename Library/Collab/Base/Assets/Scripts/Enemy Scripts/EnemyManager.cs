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
    PortalSize getElement;
    public List<Material> materials;
    public GameObject Drone;


    private List<GameObject> enemies = new List<GameObject>();


    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", delay, delay);

        getElement = GetComponentInChildren<PortalSize>();


    }


    void Spawn()
    {
        // enemies.RemoveAll(deadEnemy => deadEnemy == null);

        if (enemies.Count >= limit)
        {
            Destroy(transform.GetChild(0).gameObject);
            Destroy(gameObject);
            return;
        }



        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Vector3 pos = new Vector3(0f, 0f, 0f);
        if (enemy == Drone)
        {
            pos = new Vector3(0f, 10.1f, 0f);
        }
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        GameObject obj = Instantiate(enemy, spawnPoints[spawnPointIndex].position + pos, spawnPoints[spawnPointIndex].rotation);

        pickElement(obj);

        enemies.Add(obj);


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
