using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class StopBall : MonoBehaviour {

    [SerializeField]
    private PinchDetector _pinchDetectorL;
    public PinchDetector PinchDetectorL
    {
        get
        {
            return _pinchDetectorL;
        }
        set
        {
            _pinchDetectorL = value;
        }
    }

    public float distanceLimit;
    public Color stopColor;
    private Color originalColor;

    private GameObject aircraft;
    private bool trigger;


    // Use this for initialization
    void Start () {

        aircraft = GameObject.Find("Aircraft");
        originalColor = GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {

        float distance = 100.0f;
        if (_pinchDetectorL != null)
        {
            Vector3 direction = _pinchDetectorL.Position - transform.position;
            distance = direction.magnitude;
            //Debug.LogFormat("{0}: distance {1}", Time.time, distance);
        }


        if (_pinchDetectorL != null && _pinchDetectorL.IsActive && distance < distanceLimit && !trigger)
        {
            aircraft.GetComponent<flying>().isStop = !aircraft.GetComponent<flying>().isStop;
            trigger = true;
        }

        if( distance > distanceLimit)
        {
            trigger = false;
        }

        if(aircraft.GetComponent<flying>().isStop)
            GetComponent<Renderer>().material.color = stopColor;
        else
            GetComponent<Renderer>().material.color = originalColor;
    }
}
