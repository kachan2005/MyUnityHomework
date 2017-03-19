using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBall2 : MonoBehaviour {
    public float rotateSpeed;
    public float maxAngle;
    public Color activeColor;
    private Color originalColor;
    


    GameObject aircraft;


    private bool isTrigger;
    private bool isFirstActivate;
    private bool isActivate;
    private Vector3 lastPosition;
    private Vector3 currentPosition;
    // Use this for initialization
    void Start ()
    {
        originalColor = GetComponent<Renderer>().material.color;
        lastPosition = currentPosition = new Vector3();
        aircraft = GameObject.Find("Aircraft");
    }
	
	// Update is called once per frame
	void Update () {
		if(isTrigger) {
            //Debug.LogFormat("{0} is been triggered", gameObject.name);
            if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            {
                //Debug.LogFormat("Before {0} is activate, isFirstActivate = {1}, isActivate = {2}", gameObject.name, isFirstActivate, isActivate);
                isFirstActivate = isActivate == false;
                isActivate = true;
                //Debug.LogFormat("After {0} is activate, isFirstActivate = {1}, isActivate = {2}", gameObject.name, isFirstActivate, isActivate);
            }
            else
                isActivate = isFirstActivate = false;
        }
        else
            isActivate = isFirstActivate = false;
    }

    void rotation() {
        Vector3 a = lastPosition;
        Vector3 b = currentPosition;

        float angle = Mathf.Acos((Vector3.Dot(a, b)) / (a.magnitude * b.magnitude)) * rotateSpeed;
        Vector3 rotateAxis = Vector3.Cross(a, b);
        transform.RotateAround(transform.position, rotateAxis, Mathf.Min(angle, maxAngle));
        aircraft.transform.RotateAround(aircraft.transform.position, rotateAxis, Mathf.Min(angle, maxAngle));

        //Debug.LogFormat("{0}: angle {1}", Time.time, angle);

        lastPosition = currentPosition;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Laser")
        {
            //Debug.LogFormat("{0} is trigger by {1}", gameObject.name, other.gameObject.name);
            currentPosition = other.transform.position - transform.position;
            isTrigger = true;
            if(isFirstActivate) {
                lastPosition = currentPosition;
                return;
            }
            else if(isActivate) {
                rotation();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isTrigger = false;
    }
}
