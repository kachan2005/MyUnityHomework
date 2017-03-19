using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aircraft_collide : MonoBehaviour {

    public bool isCollided = false;
    public string collidedObject;
    public float animation_time;

    public float time;

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

    public void handle_Collide(GameObject g)
    {
        isCollided = true;
        collidedObject = g.name;

        GameObject lights = GameObject.Find("Lights");
        for (int i = 0; i < lights.transform.childCount; i++)
        {
            lights.transform.GetChild(i).GetComponent<light_collided>().isCollided = true;
        }
    }


}
