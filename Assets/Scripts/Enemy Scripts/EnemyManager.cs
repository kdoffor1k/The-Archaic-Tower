using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    private static List<GameObject> enemies = new List<GameObject>();

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update () {
        enemies.RemoveAll(deadEnemy => deadEnemy == null);
       
    }

    public static List<GameObject> getEnemies()
    {
        return enemies;
    }
}
