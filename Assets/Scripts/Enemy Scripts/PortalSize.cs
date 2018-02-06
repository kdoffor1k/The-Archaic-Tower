using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSize : MonoBehaviour {
    private float X, Y, Z;
    Transform player;

    // Use this for initialization
    void Start () {
        float X = GetComponentInParent<EnemyManager>().enemy.transform.localScale.x;
        
        float Y = GetComponentInParent<EnemyManager>().enemy.transform.localScale.y;
        float Z = GetComponentInParent<EnemyManager>().enemy.transform.localScale.z;
        X = X * 2;
        Y = Y * 3;
        transform.localScale = new Vector3(X,Y, .1f);
        transform.localPosition = new Vector2(0, Y / 2);
        player = GameObject.FindGameObjectWithTag("Destination").transform;
        Vector3 targetPosition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
        transform.LookAt(targetPosition);

       

    }
	
    
	
}
