using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour {

    private Rigidbody rb;
    [SerializeField] private GameObject cannon;
    public Vector3 direction = Vector3.zero;
    private bool shoted;
    private float collidedTime;
    private float thrust;
    private const float MAX_THRUST = 3500.0f;

    // Use this for initialization
    void Start()
    {
        shoted = false;
        collidedTime = 0.0f;
        thrust = 0.0f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            thrust += (thrust > MAX_THRUST ? 0 :  3 * Time.deltaTime * MAX_THRUST);
            Debug.LogFormat("You pressed space");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            release(thrust);
            thrust = 0.0f;
        }

        if( shoted)
        {
            collidedTime += Time.deltaTime;
            if( collidedTime > 0.5f)
            {
                reSpawnConnon();
                Destroy(gameObject);
            }
        }
        
    }

    private void release(float thrust)
    {
        Vector3 direction = Vector3.one;
        direction.x = 0.0f;
        direction.z = 1.3f;
        rb.AddForce(direction * thrust);
        shoted = true;
    }

    private void reSpawnConnon()
    {
        GameObject spawnedCannon;
        Quaternion startRotation = Quaternion.Euler(Vector3.zero);
        Vector3 spawnLocation = Vector3.zero;
        spawnLocation.y = 10.0f;
        spawnedCannon = GameObject.Instantiate(cannon, spawnLocation, startRotation) as GameObject;

    }

    public void OnCollisionExit(Collision collision)
    {
        if( collision.gameObject.name != "Plane")
        {
            shoted = true;
        }
    }
    
}
