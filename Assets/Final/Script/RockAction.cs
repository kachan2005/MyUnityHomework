using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class RockAction : MonoBehaviour {
    
    ConstantForce c;
    Rigidbody r;
    public Vector3 force;
    public Vector3 velocity;
    public int collideCount;

    public bool system_pause;
    bool prev_pause;

    // Use this for initialization
    void Start () {
        r = GetComponent<Rigidbody>();
        c = GetComponent<ConstantForce>();
        c.force = new Vector3(
                Random.Range(-80, 80),
                Random.Range(-80, 80),
                Random.Range(-80, 80)
            );

        system_pause = true;
        prev_pause = false;


        if (system_pause != prev_pause)
        {
            if (system_pause)
            {
                velocity = r.velocity;
                r.velocity = new Vector3();
                storeForce();

                GetComponent<Collider>().isTrigger = true;
                r.isKinematic = true;
            }
            else
            {
                GetComponent<Collider>().isTrigger = false;
                r.isKinematic = false;

                r.velocity = velocity;
                restoreForce();
            }
            prev_pause = system_pause;
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (system_pause != prev_pause)
        {
            if (system_pause)
            {
                velocity = r.velocity;
                r.velocity = new Vector3();
                storeForce();

                GetComponent<Collider>().isTrigger = true;
                r.isKinematic = true;
            }
            else
            {
                GetComponent<Collider>().isTrigger = false;
                r.isKinematic = false;

                r.velocity = velocity;
                restoreForce();
            }
            prev_pause = system_pause;
        }
    }

    

    private void OnCollisionEnter(Collision collision)
    {

        if (isAircraft(collision.gameObject.transform))
        {
            GameObject.Find("Aircraft").GetComponent<aircraft_collide>().collide_rock(gameObject);
            //Debug.LogFormat("{0} has collide with aircraft", gameObject.name);

            Destroy(gameObject);
            return;
        }


        //Debug.LogFormat("{0} collide with object {1}, time remain = {2}", gameObject.name, collision.gameObject.name, collideCount);
        if ( collideCount-- <= 0)
        {
            Destroy(gameObject);
            return;
        }

        ConstantForce c = GetComponent<ConstantForce>();
        Vector3 newForce = c.force;

        switch (collision.gameObject.name)
        {
            case "bottom_boundary":
            case "top_boundary":
                newForce.y *= -3;
                break;
            case "right_boundary":
            case "left_boundary":
                newForce.x *= -3;
                break;
            case "front_boundary":
            case "back_boundary":
                newForce.z *= -3;
                break;
                
        }
        //Debug.LogFormat("Before force = ({0}. {1}, {2})", c.force.x, c.force.y, c.force.z);
        //Debug.LogFormat("After force = ({0}. {1}, {2})", newForce.x, newForce.y, newForce.z);
        c.force = newForce;
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

    public void storeForce() {
        force = c.force;
        c.force = new Vector3();
    }

    public void restoreForce() {
        c.force = force;
    }
}
