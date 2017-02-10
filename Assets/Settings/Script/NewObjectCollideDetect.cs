using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectCollideDetect : MonoBehaviour {

    float time = 0.0f;
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if( time > 0.1f)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = gameObject.tag == "whiteboard";
            Collider[] colliders = gameObject.GetComponents<Collider>();
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].isTrigger = false;
            }
            Destroy(gameObject.GetComponent<NewObjectCollideDetect>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("{0} collide with {1}, {0} is destroyed", gameObject.name, other.gameObject.name);
        if(other.gameObject.name != "Plane" && other.gameObject.name != "NavigationBall" &&
           !(other.gameObject.tag == "room" && gameObject.tag == "whiteboard") )
        {
            Destroy(gameObject);
        }
    }
}
