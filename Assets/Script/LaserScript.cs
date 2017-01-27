using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {

    //debug info
    float time;

    //change laser color
    Renderer rend;

    //collide detection
    CapsuleCollider capCollider;
    private GameObject currentTriggerObject;

    //Time measurment
    float currentTime;
    private const float DWELLING_TIME = 2.0f;

    bool shot = false;
    int mode;

	// Use this for initialization
	void Start () {

        rend = GetComponent<Renderer>();
        capCollider = GetComponent<CapsuleCollider>();
        mode = 0;
        resetLaserColor();
    }
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;

        if (currentTriggerObject != null && currentTriggerObject.name != "Plane" && mode != 2)
        {
            updateLaserColor();
            Debug.LogFormat("Current Trigger Object: {0}", currentTriggerObject.name);
        }

        if (!capCollider.isTrigger)
        {
            if( shot)
            {
                shot = false;
            }
            else
            {
                capCollider.isTrigger = true;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if( currentTriggerObject != other.gameObject )
        {
            resetLaserColor();
            currentTriggerObject = other.gameObject;
        }

        Debug.LogFormat("Time {0} : enter on Object {1}", currentTime, other.gameObject.name);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.LogFormat("Time {0} : stay on Object {1}", currentTime, other.gameObject.name);

        if (other.gameObject.name == "Brick(Clone)")
        {
            if (currentTime > DWELLING_TIME)
            {
                //do action here base on mode
                if (mode == 0)
                {
                    shot = true;
                    capCollider.isTrigger = false;
                }
            }
        }
        else if( other.gameObject.name == "Button"){
            resetLaserColor();
        }
    }
    
    public void changeMode()
    {
        mode = (mode + 1) % 3;
    }

    public int getMode()
    {
        return mode;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.LogFormat("Time {0} : exit on Object {1}", currentTime, other.gameObject.name);
        currentTriggerObject = null;
        currentTime = 0.0f;
        resetLaserColor();
    }


    void updateLaserColor()
    {
        Color newColor = rend.material.GetColor("_TintColor");
        //time += Time.deltaTime;
        newColor.b += (1.0f / DWELLING_TIME) * Time.deltaTime;
        newColor.r -= (1.0f / DWELLING_TIME) * Time.deltaTime;

        //Debug.LogFormat("{3}: Color = ({0}, {1}, {2})", newColor.r, newColor.b, newColor.g, time);
        if (newColor.r < 0.0f)
        {
            resetLaserColor();
        }
        else
        {
            rend.material.SetColor("_TintColor", newColor);
        }

    }

    void resetLaserColor()
    {
        Color newColor = new Color(1.0f, 0.0f, 0.0f);
        rend.material.SetColor("_TintColor", newColor);
        currentTime = 0.0f;
    }

}
