using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aircraft_collide : MonoBehaviour {

    public bool isCollided = false;
    public string collidedObject;
    public float animation_time;

    public float time;

    public float play_time = 0.0f;
    public float money = 0.0f;

	// Use this for initialization
	void Start () {

        GameObject lights = GameObject.Find("Lights");
        for (int i = 0; i < lights.transform.childCount; i++)
        {
            lights.transform.GetChild(i).GetComponent<light_collided>().animation_time = animation_time;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        play_time += Time.deltaTime;

        if (isCollided)
        {
            time += Time.deltaTime;
            if (time < animation_time)
            {
            }
            else
            {
                isCollided = false;
                time = 0;
            }
            
        }
    }

    public void collide_rock(GameObject g)
    {
        isCollided = true;

        GameObject lights = GameObject.Find("Lights");
        for (int i = 0; i < lights.transform.childCount; i++)
        {
            lights.transform.GetChild(i).GetComponent<light_collided>().isCollided = true;
        }

        if(collidedObject != g.name)
            money -= play_time;

        collidedObject = g.name;
    }


    public void collide_coin(GameObject g)
    {
        isCollided = true;

        if (collidedObject != g.name)
            money += Mathf.Log10( play_time + 1);
        
        collidedObject = g.name;
    }


}
