using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAttach : MonoBehaviour {

    GameObject room;

    public bool attachToWall = false;

    private Vector3 lastPosition;
    private Quaternion lastRotation;

	// Use this for initialization
	void Start () {
        room = GameObject.Find("room");

	}
	
	// Update is called once per frame
	void Update () {
        float minDistance = 10000.0f;
        RaycastHit minRayhit;
        Vector3 direction = transform.right;
        Vector3 forward = transform.up;
        Vector3 minPoint = transform.position;
        if (!attachToWall)
        {
            Debug.LogFormat("Z Direction: ({0}, {1}, {2})", transform.forward.x, transform.forward.y, transform.forward.z);
            Debug.LogFormat("Y Direction: ({0}, {1}, {2})", transform.up.x, transform.up.y, transform.up.z);
            Debug.LogFormat("X Direction: ({0}, {1}, {2})", transform.right.x, transform.right.y, transform.right.z);
            for (int i = 0; i < 36; i++)
            {
                Ray ray = new Ray(transform.position, direction);
                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit, 3000.0f))
                {
                    //rayHit.collider.gameObject;
                    //Debug.LogFormat("{1}, Hit Object: ({0}) Tag: {2}", rayHit.collider.gameObject.name, i, rayHit.collider.gameObject.tag);
                    if (rayHit.collider.gameObject.tag == "room")
                    {
                        //Debug.LogFormat("{1}, Hit Object: ({0})", rayHit.collider.gameObject.transform.parent.parent.gameObject.name, i);
                        if (minDistance > rayHit.distance)
                        {
                            minDistance = rayHit.distance;
                            minRayhit = rayHit;
                            minPoint = rayHit.point;
                            forward = minRayhit.normal;
                        }
                        //Debug.LogFormat("{1}, Has room tag, minDistance = {0}, direction = ({2}, {3}, {4}) ", minDistance, i, forward.x, forward.y, forward.z);
                    }
                }
                else
                {
                    //Debug.LogFormat("{0}, Nothing get Hitting",  i);
                }
                //Debug.LogFormat("Current Direction: ({0}, {1}, {2})", direction.x, direction.y, direction.z);
                direction = Quaternion.AngleAxis(10, transform.forward) * direction;
            }
            transform.up = forward;
            Vector3 position = transform.position;

            //determine normal is face in or face out
            Transform playerPosition = GameObject.Find("OVRPlayerController").transform;
            float distance = Vector3.Dot( (playerPosition.position - transform.position), forward);
            if( distance > 0) // normal face me, not good
            {
                forward *= -1;
                Vector3 r = transform.rotation.eulerAngles;
                r.y += 180;
                transform.rotation = Quaternion.Euler(r);
            }

            //Debug.LogFormat("Has room tag, minDistance = {0}, direction = ({1}, {2}, {3}) ", minDistance, forward.x, forward.y, forward.z);
            //Debug.LogFormat("Before position is ({0}, {1}, {2})", position.x, position.y, position.z);
            //Debug.LogFormat("forward Direction is ({0}, {1}, {2})", forward.x, forward.y, forward.z);
            //Debug.LogFormat("distance is ({0})", minDistance);
            position = position + forward * (minDistance - 0.2f);

            //Debug.LogFormat("After position is ({0}, {1}, {2})", position.x, position.y, position.z);
            transform.position = position;
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.x = -90f;
            transform.rotation = Quaternion.Euler(rotation);

            attachToWall = true;
        }
	}

    private void OnTriggerStay(Collider other)
    {
        //if(other.gameObject.layer != LayerMask.NameToLayer("grabble") && other.gameObject.name != "NavigationBall")
        //{
        //    Debug.LogFormat("Whiteboard trigger with {0}", other.gameObject.name);
        //}
        if(other.gameObject.tag == "room")
        {
            //gameObject.GetComponent<Collider>().isTrigger = false;
            //gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Debug.LogFormat("Whiteboard trigger with {0}", other.gameObject.name);
            //transform.position = transform.position + transform.up * -1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "room")
        {
            //gameObject.GetComponent<Collider>().isTrigger = true;
            //gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //attachToWall = false;
        }
    }



}
