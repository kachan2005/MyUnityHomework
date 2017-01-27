using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner : MonoBehaviour {

    [SerializeField] private GameObject sphereToSpawn;
    [SerializeField] private Vector3 spawnLocation;
    

    // Use this for initialization
    void Start () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        GameObject spawnedSphere;
        Quaternion startRotation = Quaternion.Euler(Vector3.zero);

        //Vector3 newRotation = new Vector3(90.0f, 90.0f, 0.0f);
        //transform.rotation = Quaternion.Euler(newRotation);

        //Vector3 eulerAngles = transform.rotation.eulerAngles;

        float x = Random.Range(-0.1f, 0.1f);
        float y = Random.Range(3.0f, 10.0f);
        float z = Random.Range(-0.1f, 0.1f);
        spawnLocation.x += x;
        spawnLocation.y = y;
        spawnLocation.z += z;

        spawnedSphere = GameObject.Instantiate(sphereToSpawn, spawnLocation, startRotation) as GameObject;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
