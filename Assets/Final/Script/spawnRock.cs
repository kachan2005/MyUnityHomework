using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnRock : MonoBehaviour {

    public GameObject rock;
    public GameObject coin;

    public GameObject checkpoints;

    public int numOjbect;
    public float x_size;
    public float y_size;
    public float z_size;
    public float step;

    public float spawnTime;
    public int spawnRatio;
    float time;

    public int count;
    public int total_count = 0;

    // Use this for initialization
    void Start()
    {
        time = 0.0f;
        for (float x = -x_size; x < x_size - step; x += step)
        {
            for (float y = -y_size; y < y_size - step; y += step)
            {

                for (float z = -z_size; z < z_size - step; z += step)
                {
                    int hasObject = (int)(Random.Range(0, spawnRatio));
                    if (hasObject > 3)
                        continue;

                    for (int i = 0; i < hasObject; i++)
                    {
                        int objectType = (int)(Random.Range(0, 10));
                        Object spawnObject = rock;
                        if (objectType > 7)
                            spawnObject = coin;


                        GameObject newObject;
                        Vector3 position = new Vector3(
                            x + Random.Range(0, step),
                            y + Random.Range(0, step),
                            z + Random.Range(0, step)
                        );

                        Quaternion rotation = Quaternion.Euler(Vector3.zero);

                        newObject = GameObject.Instantiate(spawnObject, position, rotation) as GameObject;
                        newObject.transform.parent = checkpoints.transform;
                        newObject.name = "Object " + total_count++;
                    }

                }
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        count = checkpoints.transform.childCount;
        time += Time.deltaTime;

        int hasObject = 0;
        if(time > spawnTime)
        {
            hasObject = (int)(Random.Range(0, spawnRatio));
            time = 0;
        }

        for (int i = 0; i < hasObject; i++)
        {
            int objectType = (int)(Random.Range(0, 10));
            Object spawnObject = rock;
            if (objectType > 7)
                spawnObject = coin;


            GameObject newObject;
            Vector3 position = new Vector3(
                Random.Range(-x_size, x_size),
                Random.Range(-y_size, y_size),
                Random.Range(-z_size, z_size)
            );

            Quaternion rotation = Quaternion.Euler(Vector3.zero);

            newObject = GameObject.Instantiate(spawnObject, position, rotation) as GameObject;
            newObject.transform.parent = checkpoints.transform;
            newObject.name = "Object " + total_count++;
            total_count = total_count % 100000;
        }
    }
}
