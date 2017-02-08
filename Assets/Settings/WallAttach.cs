using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAttach : MonoBehaviour {

    GameObject room;

    bool attachToWall = false;

	// Use this for initialization
	void Start () {
        room = GameObject.Find("room");

	}
	
	// Update is called once per frame
	void Update () {
        float minDistance = 10000.0f;
        RaycastHit minRayhit;
        Vector3 direction = transform.right;
        Vector3 minDirection = direction;
        Vector3 forward = transform.up;
        if (!attachToWall)
        {
            for(int i = 0; i < 36; i++)
            {
                Ray ray = new Ray(transform.position, direction);
                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit, 30.0f))
                {
                    //rayHit.collider.gameObject;
                    if (rayHit.collider.gameObject.transform.parent.parent.gameObject.name != "room")
                    {
                        continue;
                    }
                    //Debug.LogFormat("{1}, Hit Object: ({0})", rayHit.collider.gameObject.transform.parent.parent.gameObject.name, i);
                    if ( minDistance > rayHit.distance)
                    {
                        minDistance = rayHit.distance;
                        minRayhit = rayHit;

                        minDirection = direction;
                        forward = minRayhit.normal * -1;
                    }
                }
                direction = Quaternion.AngleAxis(10, transform.forward) * direction;
            }
            transform.up = forward;
            Vector3 position = transform.position;
            //Debug.LogFormat("Before position is ({0}, {1}, {2})", position.x, position.y, position.z);
            //Debug.LogFormat("forward Direction is ({0}, {1}, {2})", forward.x, forward.y, forward.z);
            //Debug.LogFormat("distance is ({0})", minDistance);
            position = position + forward * (minDistance - 0.1f);

            //Debug.LogFormat("After position is ({0}, {1}, {2})", position.x, position.y, position.z);
            transform.position = position;

            attachToWall = true;

            

        }
	}

   
}
