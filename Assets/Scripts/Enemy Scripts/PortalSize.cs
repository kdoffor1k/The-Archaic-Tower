using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSize : MonoBehaviour {
    private float X, Y, Z;
    Transform player;
    public List<Material> materials;
    private int element;

    // Use this for initialization
    void Start () {

        pickElement();
        
        player = GameObject.FindGameObjectWithTag("Destination").transform;
        float X = GetComponentInParent<EnemyManager>().enemy.transform.localScale.x;
        float Y = GetComponentInParent<EnemyManager>().enemy.transform.localScale.y;
        float Z = GetComponentInParent<EnemyManager>().enemy.transform.localScale.z;
        float X2 = X;
        // calculates the x and y scale for portal based on emeny size
        X = X * 2;
        Y = Y * 3;
        //creates portal
        transform.localScale = new Vector3(X, Y, .1f);
        //adjusts portal postioning based on its personal size
        transform.localPosition = new Vector2(0, Y / 2);
        
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);
       
        //pushes the portal X2/2 units away from portal to give appearance of moving through portal
        transform.position = Vector3.MoveTowards(transform.position, player.position, X2/2 + .1f);

    }
	
    public void pickElement()
    {
        element = Random.Range(0, 3);
        Renderer renderer = GetComponent<Renderer>();
        if (element == 0)
        {
            renderer.material = materials[element];
        }
        else if (element == 1)
        {
            renderer.material = materials[element];
        }
        else
        {
            renderer.material = materials[element];
        }
    }

    public int getElement()
    {
        return element;
    }

	
}
