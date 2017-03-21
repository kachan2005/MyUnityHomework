using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTiles : MonoBehaviour {

    GameObject wall_left;
    GameObject wall_right;
    GameObject wall_back;
    GameObject wall_front;
    GameObject wall_top;
    GameObject wall_ground;

    private float time = 0;

    // Use this for initialization
    void Start () {

        wall_left = GameObject.Find("plane_wall_left");
        wall_right = GameObject.Find("plane_wall_right");
        wall_back = GameObject.Find("plane_wall_back");
        wall_front = GameObject.Find("plane_wall_front");
        wall_top = GameObject.Find("plane_top");
        wall_ground = GameObject.Find("plane_ground");
    }
	
	// Update is called once per frame
	void Update () {
		if( GameObject.Find("Menu").GetComponent<PauseSystem>().systemPause == false) {
            time += Time.deltaTime;
            GetComponent<AudioSource>().mute = false;
            if(time > 4) {
                GetComponent<AudioSource>().Play();
                time = 0;
            }

        }
        else {
            GetComponent<AudioSource>().mute = true;
        }
	}

    public void switchTiles(Material m) {

        for (int i = 0; i < wall_left.transform.childCount; i++)
        {
            if (wall_left.transform.GetChild(i).name != "glass")
                wall_left.transform.GetChild(i).GetComponent<Renderer>().material = m;
        }
        for (int i = 0; i < wall_right.transform.childCount; i++)
        {
            if (wall_right.transform.GetChild(i).name != "glass")
                wall_right.transform.GetChild(i).GetComponent<Renderer>().material = m;
        }
        for (int i = 0; i < wall_back.transform.childCount; i++)
        {
            if (wall_back.transform.GetChild(i).name != "glass")
                wall_back.transform.GetChild(i).GetComponent<Renderer>().material = m;
        }
        for (int i = 0; i < wall_front.transform.childCount; i++)
        {
            if (wall_front.transform.GetChild(i).name != "glass")
                wall_front.transform.GetChild(i).GetComponent<Renderer>().material = m;
        }
        wall_top.GetComponent<Renderer>().material = m;
        wall_ground.GetComponent<Renderer>().material = m;

    }
}
