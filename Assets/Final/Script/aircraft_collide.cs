using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class aircraft_collide : MonoBehaviour {

    public bool isCollided = false;
    public string collidedObject;
    public float animation_time;

    public float time;

    public List<GameObject> lights;

    public AudioClip rockCrush;
    public AudioClip coinDrop;

    private system_menu system_menu;
    private PauseSystem pasuse_system;

	// Use this for initialization
	void Start () {
        system_menu = GameObject.Find("System_Menu").GetComponent<system_menu>();
        pasuse_system = GameObject.Find("Menu").GetComponent<PauseSystem>();  


        //GameObject lights = GameObject.Find("Lights");
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].GetComponent<light_collided>().animation_time = animation_time;
        }
        
    }
	
	// Update is called once per frame
	void Update () {

        //if (isCollided)
        //{
        //    time += Time.deltaTime;
        //    if (time < animation_time)
        //    {
        //    }
        //    else
        //    {
        //        isCollided = false;
        //        time = 0;
        //    }
        //}
        

        if (pasuse_system.systemPause) {
            GetComponent<AutoMoveAndRotate>().enabled = false; 
            GetComponent<AudioSource>().mute = true;
        }
        else
        {

            time += Time.deltaTime;
            GetComponent<AudioSource>().mute = false;
            if (time > 5)
            {
                GetComponent<AudioSource>().Play();
                time = 0;
            }
            GetComponent<AutoMoveAndRotate>().enabled = true;
        }
    }

    public void collide_rock(GameObject g)
    {
        //Debug.LogFormat("Aircraft collide with rock {0}", g.name);
        isCollided = true;

        AudioSource.PlayClipAtPoint(rockCrush, g.transform.position);
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].GetComponent<light_collided>().isCollided = true;
        }

        if (collidedObject != g.name)
            system_menu.hitRock();

        collidedObject = g.name;
    }


    public void collide_coin(GameObject g)
    {
        isCollided = true;

        AudioSource.PlayClipAtPoint(coinDrop, g.transform.position);
        if (collidedObject != g.name)
            system_menu.getCoin();
        
        collidedObject = g.name;
    }


}
