using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class RockAction : MonoBehaviour {
    
    public Vector3 force;
    public Vector3 velocity;
    public int collideCount;

    public bool system_pause;
    bool prev_pause;

    // Use this for initialization
    void Start () {
        GetComponent<ConstantForce>().force = new Vector3(
                Random.Range(-80, 80),
                Random.Range(-80, 80),
                Random.Range(-80, 80)
            );

        //   c.force = new Vector3(
        //    Random.Range(-1, 1),
        //    Random.Range(-1, 1),
        //    Random.Range(-1, 1)
        //);


        system_pause = GameObject.Find("Menu").GetComponent<PauseSystem>().systemPause;
        pauseRock(system_pause);
    }
	

    public void pauseRock(bool isPause)
    {
        if (isPause)
        {
            velocity = GetComponent<Rigidbody>().velocity;
            GetComponent<Rigidbody>().velocity = new Vector3();
            storeForce();

            GetComponent<Collider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            GetComponent<Collider>().isTrigger = false;
            GetComponent<Rigidbody>().isKinematic = false;

            GetComponent<Rigidbody>().velocity = velocity;
            restoreForce();
        }
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.LogFormat("{0} collide with object {1}, time remain = {2}", gameObject.name, collision.gameObject.name, collideCount);

        if (isAircraft(collision.gameObject.transform))
        {
            GameObject.Find("Aircraft").GetComponent<aircraft_collide>().collide_rock(gameObject);
            //Debug.LogFormat("{0} has collide with aircraft", gameObject.name);

            Destroy(gameObject);
            return;
        }

        if ( collideCount-- <= 0)
        {
            Destroy(gameObject);
            return;
        }
        
        Vector3 newForce = GetComponent<ConstantForce>().force;

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
        GetComponent<ConstantForce>().force = newForce;
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
        force = GetComponent<ConstantForce>().force;
        GetComponent<ConstantForce>().force = new Vector3();
    }

    public void restoreForce() {
        GetComponent<ConstantForce>().force = force;
    }
}
