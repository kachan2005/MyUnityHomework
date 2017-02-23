using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trial_Arrow : MonoBehaviour {

    private float time = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        transform.parent.RotateAround(transform.parent.position, transform.parent.forward, 30.0f * time);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.parent != null)
        {
            if (other.gameObject.transform.parent.gameObject.name == "CheckPoints") Destroy(transform.parent.gameObject);
            
        }
    }
}
