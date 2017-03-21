using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manipulation : MonoBehaviour {

    public GameObject modifiedObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RotateY(float value)
    {
        if (modifiedObject == null) return;

        Vector3 angle = new Vector3(0, value, 0);
        modifiedObject.transform.localRotation = Quaternion.Euler(angle);
    }

    public void updateLightColor(float value, int index) {

        Color c = modifiedObject.GetComponent<Light>().color;

        switch(index)
        {
            case 1:
                c.r = value;
                break;
            case 2:
                c.g = value;
                break;
            case 3:
                c.b = value;
                break;
            case 4:
                modifiedObject.GetComponent<Light>().intensity = value;
                break;
        }

        modifiedObject.GetComponent<Light>().color = c;
    }
}
