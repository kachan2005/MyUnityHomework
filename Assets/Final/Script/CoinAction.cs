using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class CoinAction : MonoBehaviour {
    

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnCollisionEnter(Collision collision)
    {

        Debug.LogFormat("{0} collide with object {1}", gameObject.name, collision.gameObject.name);


        if (isAircraft(collision.gameObject.transform))
        {
            GameObject.Find("Aircraft").GetComponent<aircraft_collide>().collide_coin(gameObject);
            Debug.LogFormat("{0} has collide with aircraft", gameObject.name);
            
            Destroy(gameObject);
            return;
        }
        
    }

    private bool isAircraft(Transform t)
    {
        while (t != null)
        {
            if (t.name == "Aircraft")
            {
                return true;;
            }
            t = t.parent;
        }
        return false;
    }
}
