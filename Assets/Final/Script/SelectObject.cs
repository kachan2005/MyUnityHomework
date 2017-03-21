using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectObject : MonoBehaviour {
    public bool system_pause;
    public bool isDisplayed;
    
    public bool activative = false;
    public bool trigger = false;

    public GameObject selectedObject;


    public GameObject L_Hand;
    public GameObject R_Hand;
    
    public float ratio;

    public AudioClip teleport;
    public AudioClip selectItem;

    private bool holdObject = false;

    system_menu s;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(s == null) s = GameObject.Find("System_Menu").GetComponent<system_menu>();

        if (isDisplayed)
        {
            Vector3 rPosition = R_Hand.transform.position;
            Vector3 lPosition = L_Hand.transform.position;

            Vector3 direction = lPosition - rPosition;
            float distance = direction.magnitude;
            direction = direction.normalized;

            distance = distance * ratio;

            Vector3 newPosition = lPosition + (distance * direction);
            transform.position = newPosition;

            if (s.mode == 1){//teleport
                newPosition = transform.localPosition;
                newPosition.y = 0.5f;
                transform.localPosition = newPosition;
            }
        }


        if ( OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))  
        {
            if(s.mode == 1 && !activative)
            {
                if (!inBounard()) return;

                Transform t = GameObject.Find("OVRPlayerController").transform;
                //t.position = transform.position;
                Vector3 position = transform.localPosition;
                position.y = 2;
                t.localPosition = position;
                activative = true;


                AudioSource.PlayClipAtPoint(teleport, transform.position);
            }

            if(s.mode == 2) //select a object
                activative = true;

            if(s.mode == 3)
            {
                if(!activative) {
                    holdObject = !holdObject;
                    AudioSource.PlayClipAtPoint(selectItem, selectedObject.transform.position);
                    activative = true;
                }
            }
        }

        if(s.mode == 3 && holdObject) {
            if (!inBounard()) return;
            GameObject.Find("ManipulationMenu").GetComponent<Manipulation>().modifiedObject.transform.position = transform.position;
        }


        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            trigger = false;
            activative = false;
        }

    }

    
    private void OnTriggerStay(Collider other)
    {
        Debug.LogFormat("Select Object: other object name {0}", other.gameObject.name);
        if (activative && !trigger && other.gameObject.tag == "changeable" && GameObject.Find("System_Menu").GetComponent<system_menu>().mode == 2)
        {
            trigger = true;

            selectedObject = other.gameObject;

            GameObject.Find("ManipulationMenu").GetComponent<Manipulation>().modifiedObject = selectedObject;
            GameObject.Find("SelectedTitle").GetComponent<Text>().text = selectedObject.name;
            GameObject.Find("RotateY").GetComponent<Slider>().value = selectedObject.transform.localRotation.y;
            GameObject.Find("RotateY").transform.GetChild(5).GetComponent<Text>().text = "" + selectedObject.transform.localRotation.y;

            bool showLight = selectedObject.GetComponent<Light>() != null;
            GameObject.Find("LightMenu").GetComponent<DisplayMenu>().displayList(showLight);
            if (showLight) {
                Color c = selectedObject.GetComponent<Light>().color;
                GameObject.Find("Light Red").GetComponent<Slider>().value = c.r * 255;
                GameObject.Find("Light Red").transform.GetChild(5).GetComponent<Text>().text = "" + c.r * 255;
                GameObject.Find("Light Green").GetComponent<Slider>().value = c.g * 255;
                GameObject.Find("Light Green").transform.GetChild(5).GetComponent<Text>().text = "" + c.g * 255;
                GameObject.Find("Light Blue").GetComponent<Slider>().value = c.b * 255;
                GameObject.Find("Light Blue").transform.GetChild(5).GetComponent<Text>().text = "" + c.b * 255;
                GameObject.Find("Light Intensity").GetComponent<Slider>().value = selectedObject.GetComponent<Light>().intensity;
                GameObject.Find("Light Intensity").transform.GetChild(5).GetComponent<Text>().text = "" + selectedObject.GetComponent<Light>().intensity;
            }


            AudioSource.PlayClipAtPoint(selectItem, selectedObject.transform.position);
        }
    }
    


    public void display(bool b)
    {
        isDisplayed = b;
        GetComponent<Renderer>().enabled = isDisplayed;
        GetComponent<Light>().enabled = isDisplayed;

        if (b == false) {
            selectedObject = null; 
            GameObject.Find("ManipulationMenu").GetComponent<Manipulation>().modifiedObject = null;
            GameObject.Find("SelectedTitle").GetComponent<Text>().text = "Select A Object";
        }
    }

    public void applySystemPause(bool isPaused)
    {
        system_pause = isPaused;
        if (!system_pause)
        {
            selectedObject = null;
            display(false);
        }
    }

    public bool inBounard()
    {
        Vector3 p = transform.localPosition;
        if (p.x < -9.5 || p.x > 9.5)
            return false;

        if (p.z < -17 || p.z > 17)
            return false;

        if (p.y < 0.5f || p.y > 6.5)
            return false;

        return true;

    }
}
