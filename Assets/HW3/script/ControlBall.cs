﻿using UnityEngine;

namespace Leap.Unity
{

    /// <summary>
    /// Use this component on a Game Object to allow it to be manipulated by a pinch gesture.  The component
    /// allows rotation, translation, and scale of the object (RTS).
    /// </summary>
    public class ControlBall : MonoBehaviour
    {


        public float distanceLimit;
        public float rotateSpeed;
        public float maxAngle;
        
        [SerializeField]
        private PinchDetector _pinchDetectorR;
        public PinchDetector PinchDetectorR
        {
            get
            {
                return _pinchDetectorR;
            }
            set
            {
                _pinchDetectorR = value;
            }
        }

        GameObject aircraft;
        
        private Vector3 lastPosition;
        private Vector3 currentPosition;


        void Start()
        {
            lastPosition = currentPosition = new Vector3();
            aircraft = GameObject.Find("Aircraft");
        }

        void Update()
        {
            //transform.position = aircraft.transform.position +
            //    aircraft.transform.up * 0.3f +
            //    aircraft.transform.forward * 0.15f;
            float distance = 100.0f;

            if (_pinchDetectorR != null)
            {
                currentPosition = _pinchDetectorR.Position - transform.position;
                distance = currentPosition.magnitude;
                //Debug.LogFormat("{0}: distance {1}", Time.time, distance);
            }
            
            if (_pinchDetectorR != null && _pinchDetectorR.IsActive && distance < distanceLimit)
            {
                transformSingleAnchor();
            }
            
            //aircraft.transform.rotation = transform.localRotation;


        }

        private void transformSingleAnchor()
        {

            Vector3 currentPosition = _pinchDetectorR.Position - transform.position;


            Vector3 a = lastPosition;
            Vector3 b = currentPosition;

            float angle = Mathf.Acos((Vector3.Dot(a, b)) / (a.magnitude * b.magnitude)) * rotateSpeed;
            Vector3 rotateAxis = Vector3.Cross(a, b);
            transform.RotateAround(transform.position, rotateAxis, Mathf.Min(angle, maxAngle));
            aircraft.transform.RotateAround(aircraft.transform.position, rotateAxis, Mathf.Min(angle, maxAngle));

            Debug.LogFormat("{0}: angle {1}", Time.time, angle);

            lastPosition = currentPosition;
        }

    }
}