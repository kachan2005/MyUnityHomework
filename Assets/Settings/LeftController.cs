using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftController : MonoBehaviour {

    public OVRInput.Controller controller;

    private int selectionMode = 0;

    // Use this for initialization
    void Start()
    {
        Debug.LogFormat("Controller Info: {0}", controller.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
        transform.localRotation = OVRInput.GetLocalControllerRotation(controller);
    }
}
