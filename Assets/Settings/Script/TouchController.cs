using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    public OVRInput.Controller controller;

    private Vector3 direction;
    private bool shooting;


	// Use this for initialization
	void Start () {
        Debug.LogFormat("Controller Info: {0}", controller.ToString());
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
        transform.localRotation = OVRInput.GetLocalControllerRotation(controller);
    }
}
