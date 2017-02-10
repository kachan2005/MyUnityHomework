using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    public float grabRadius;
    public LayerMask grabMask;
    

    private GameObject grabbedObject;
    private bool grabbing = false;

    private Vector3 CHAIR_SCALE = new Vector3(0.65f, 0.65f, 0.65f);

    void GrabObject()
    {
        Debug.LogFormat("GrabObject detect");
        grabbing = true;
        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask);

        if (hits.Length > 0)
        {
            int closestHit = 0;
            for( int i = 0; i < hits.Length; i++){
                if (hits[i].distance > hits[closestHit].distance) closestHit = i;
                Debug.LogFormat("{0} GrabObject name: {1}", i , hits[i].collider.gameObject.name);
            }

            grabbedObject = hits[closestHit].transform.gameObject;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            grabbedObject.transform.position = transform.position;
            grabbedObject.transform.parent = transform;
        }   

    }

    void DropObject()
    {
        grabbing = false;

        if(grabbedObject != null)
        {
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

            fixScale();

            grabbedObject = null;
        }


    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!grabbing && Input.GetAxis("Oculus_GearVR_RIndexTrigger") == 1) GrabObject();
        if (grabbing && Input.GetAxis("Oculus_GearVR_RIndexTrigger") < 1) DropObject();

        //fixScale();
    }

    void fixScale()
    {
        if (grabbedObject != null)
        {

            if (grabbedObject.tag == "chair")
            {
                grabbedObject.transform.localScale = CHAIR_SCALE;
            }
        }
    }
}
