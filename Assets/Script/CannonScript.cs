using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour {

    private Rigidbody rb;
    [SerializeField] private GameObject cannon;
    private bool isCollied;
    private float collidedTime;
    public float thrust;
    public Vector3 direction;

    // Use this for initialization
    void Start()
    {
        isCollied = false;
        collidedTime = 0.0f;
        rb = GetComponent<Rigidbody>();
        release();
    }

    // Update is called once per frame
    void Update () {
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    thrust += (thrust > MAX_THRUST ? 0 :  3 * Time.deltaTime * MAX_THRUST);
        //    Debug.LogFormat("You pressed space");
        //}

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    release(thrust);
        //    thrust = 0.0f;
        //}

        if( isCollied)
        {
            collidedTime += Time.deltaTime;
            if( collidedTime > 0.5f)
            {
                //reSpawnConnon();
                Destroy(gameObject);
            }
        }
        
    }

    public void release()
    {
        //Vector3 direction = Vector3.one;
        //direction.x = 0.0f;
        //direction.z = 1.3f;
        rb.AddForce(direction * thrust);
        Debug.LogFormat("Ball is Shooting");
    }

    //private void reSpawnConnon()
    //{
    //    GameObject spawnedCannon;
    //    Quaternion startRotation = Quaternion.Euler(Vector3.zero);
    //    Vector3 spawnLocation = Vector3.zero;
    //    spawnLocation.y = 10.0f;
    //    spawnedCannon = GameObject.Instantiate(cannon, spawnLocation, startRotation) as GameObject;

    //}

    public void OnCollisionExit(Collision collision)
    {
        isCollied = true;
    }
    
}
