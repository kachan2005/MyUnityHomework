using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDisplay : MonoBehaviour {

    public Vector3 localPosition;
    public bool parentWith;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    
            SetMenu();
        
	}

    void SetMenu()
    {
        GameObject g = GameObject.Find("controller_left");
        if (g.transform.childCount <= 0) return;

        if(parentWith)
        {
            if (transform.parent == null) {
                transform.parent = g.transform;
                transform.localPosition = localPosition;
                transform.localRotation = new Quaternion();
            }
        }
        else {
            transform.parent = null;
            transform.position = g.transform.position + localPosition;
            transform.rotation = g.transform.rotation;
        }
    }
}
