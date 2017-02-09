using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour {

    public GameObject chair;
    public GameObject desk;
    public GameObject whiteboard;
    public GameObject TV;
    public GameObject locker;
    public GameObject cabinet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(2))
        {
            CreateChairParameter();
        }
    }


    public GameObject CreateChairParameter()
    {
        Vector3 position = new Vector3(0, 3.75f, 0);
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Vector3 scale = new Vector3(0.65f, 0.65f, 0.65f);

        //o.transform.position = position;
        //o.transform.rotation = rotation;
        //o.transform.localScale = scale;
        GameObject newChair = GameObject.Instantiate(chair, position, rotation) as GameObject;
        newChair.transform.localScale = scale;

        BoxCollider b;
        //box collider 1
        b = newChair.AddComponent<BoxCollider>();
        b.center = new Vector3(0, 2.3f, 2);
        b.size = new Vector3(6, 1.8f, 7.7f);
        //box collider 2
        b = newChair.AddComponent<BoxCollider>();
        b.center = new Vector3(-3.3f, -0.5f, 0.8f);
        b.size = new Vector3(1, 3, 2.6f);
        //box collider 3
        b = newChair.AddComponent<BoxCollider>();
        b.center = new Vector3(3.3f, -0.5f, 0.8f);
        b.size = new Vector3(1, 3, 2.6f);
        //box collider 4
        b = newChair.AddComponent<BoxCollider>();
        b.center = new Vector3(0, 0, -0.4f);
        b.size = new Vector3(7, 6.5f, 0.7f);
        //box collider 5
        b = newChair.AddComponent<BoxCollider>();
        b.center = new Vector3(-2.2f, -2.3f, -3);
        b.size = new Vector3(0.5f, 0.5f, 5.5f);
        //box collider 6
        b = newChair.AddComponent<BoxCollider>();
        b.center = new Vector3(2.2f, -2.3f, -3);
        b.size = new Vector3(0.5f, 0.5f, 5.5f);
        //box collider 5
        b = newChair.AddComponent<BoxCollider>();
        b.center = new Vector3(-2.2f, 1.6f, -3);
        b.size = new Vector3(0.5f, 1.5f, 5.5f);
        //box collider 6
        b = newChair.AddComponent<BoxCollider>();
        b.center = new Vector3(2.2f, 1.6f, -3);
        b.size = new Vector3(0.5f, 1.5f, 5.5f);

        Rigidbody r = newChair.AddComponent<Rigidbody>();
        r.mass = 50;

        newChair.AddComponent<ApplyOutlined>();

        newChair.tag = "chair";
        newChair.layer = 8;

        return newChair;
    }
}
