using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_point : MonoBehaviour {

    public Color checked_color;
    public Color target_color;
    public bool isChecked = false;


	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setTarget()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_TintColor", target_color);
    }

    public void setChecked()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_TintColor", checked_color);
        isChecked = true;
    }
}
