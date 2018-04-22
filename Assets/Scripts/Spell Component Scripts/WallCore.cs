using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCore : SpellCore {
    

    public void DoOnCast()
    {
        //print("0 ==================================");
        base.DoOnCast();
        //print("1 ==================================");
        RaycastHit hit;


        //print("2 ==================================");
        Object WallPrefab = Resources.Load("WallPrefab", typeof(GameObject));

        GameObject wall = (GameObject)Object.Instantiate(WallPrefab);
        if (gameObject.GetComponent<GravityBallEffect>() != null)
        {
            new GravityModifier().applyAffect(wall);
        }
        if (Physics.Raycast(transform.position, transform.up, out hit))
        {

            print("YOOOOO");
            if (hit.collider.tag == "Ground")
            {
                wall.transform.position = hit.point;
            }
            else
            {
                GameObject.Destroy(wall);
            }
                //print("3 ==================================");

        }
        else
        {
            print("COME ON");
            GameObject.Destroy(wall);
            //print("4 ==================================");
        }
        //print("5 ==================================");
        GameObject.Destroy(gameObject);
        //print("6 ==================================");
    }
}
