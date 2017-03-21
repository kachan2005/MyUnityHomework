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

        //Vector3 angle = modifiedObject.transform.localRotation.;
        Vector3 angle = new Vector3(0, value, 0);
        modifiedObject.transform.localRotation = Quaternion.Euler(angle);

        //modifiedObject.transform.Rotate(new Vector3(0,1,0), value);
    }
}
