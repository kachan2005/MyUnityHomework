using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour {

    public Material outline_material;
    public GameObject chair;
    public GameObject desk;
    public GameObject whiteboard;
    public GameObject TV;
    public GameObject locker;
    public GameObject cabinet;
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(2))
        {
            CreateChairParameter();
        }
    }

    public GameObject CreateObject(string tag)
    {
        //Debug.LogFormat("Creating Object {0}", tag);
        GameObject o = null;

        switch (tag)
        {
            case "chair":
                o = CreateChairParameter();
                break;
            case "cabinet":
                o = CreateCabinetParameter();
                break;
            case "3DTV":
                o = CreateTVParameter();
                break;
            case "desk":
                o = CreateDeskParameter();
                break;
            case "locker":
                o = CreateLockerParameter();
                break;
            case "whiteboard":
                o = CreateWhiteboardParameter();
                break;
        }

        if (o != null)
        {
            o.layer = LayerMask.NameToLayer("grabble");
            o.transform.parent = GameObject.Find("Objects").transform;
            Rigidbody r = o.AddComponent<Rigidbody>();
            r.mass = 1000;
            ApplyOutlined outline = o.AddComponent<ApplyOutlined>();
            outline.theMaterial = outline_material;
        }

        return o;
    }

    public GameObject CreateOjbect(Vector3 position)
    {
        string tag = GameObject.Find("AddObjectName").GetComponent<TextMesh>().text;
        //Debug.LogFormat("Creating Object {0}", tag);
        GameObject o = null;

        switch (tag)
        {
            case "chair":
                o = CreateChairParameter();
                break;
            case "cabinet":
                o = CreateCabinetParameter();
                break;
            case "3DTV":
                o = CreateTVParameter();
                break;
            case "desk":
                o = CreateDeskParameter();
                break;
            case "locker":
                o = CreateLockerParameter();
                break;
            case "whiteboard":
                o = CreateWhiteboardParameter();
                break;
        }

        if(o != null)
        {
            o.layer = LayerMask.NameToLayer("grabble");
            o.transform.parent = GameObject.Find("Objects").transform;
            Rigidbody r = o.AddComponent<Rigidbody>();
            r.mass = 1000;
            ApplyOutlined outline = o.AddComponent<ApplyOutlined>();
            outline.theMaterial = outline_material;

            position.y = o.transform.position.y;
            o.transform.position = position;

            //test if it will collide with other thing
            r.isKinematic = true;
            Collider[] colliders = o.GetComponents<Collider>();
            for(int i = 0; i < colliders.Length; i++)
            {
                colliders[i].isTrigger = true;
            }
            o.AddComponent<NewObjectCollideDetect>();
        }

        return o;
    }


    public GameObject CreateChairParameter()
    {
        Vector3 position = new Vector3(0, 3.75f, 0);
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Vector3 scale = new Vector3(0.65f, 0.65f, 0.65f);
        
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

        newChair.tag = "chair";
        newChair.layer = LayerMask.NameToLayer("grabble");



        return newChair;
    }

    public GameObject CreateDeskParameter()
    {
        Vector3 position = new Vector3(0, 4.6f, 0);
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Vector3 scale = new Vector3(20, 20, 20);

        GameObject newObject = GameObject.Instantiate(desk, position, rotation) as GameObject;
        newObject.transform.localScale = scale;

        BoxCollider b;
        //box collider 1
        b = newObject.AddComponent<BoxCollider>();
        b.center = new Vector3(0, -0.355f, -0.08f);
        b.size = new Vector3(0.33f, 0.015f, 0.3f);
        //box collider 2
        b = newObject.AddComponent<BoxCollider>();
        b.center = new Vector3(0, 0.355f, -0.08f);
        b.size = new Vector3(0.33f, 0.015f, 0.3f);
        //box collider 3
        b = newObject.AddComponent<BoxCollider>();
        b.center = new Vector3(-0.16f, 0, 0);
        b.size = new Vector3(0.01f, 0.73f, 0.14f);
        //box collider 4
        b = newObject.AddComponent<BoxCollider>();
        b.center = new Vector3(0, -0.01f, -0.13f);
        b.size = new Vector3(0.22f, 0.17f, 0.2f);
        //box collider 5
        b = newObject.AddComponent<BoxCollider>();
        b.center = new Vector3(0, 0, 0.15f);
        b.size = new Vector3(0.33f, 0.73f, 0.17f);

        newObject.tag = "desk";

        return newObject;
    }

    public GameObject CreateLockerParameter()
    {
        Vector3 position = new Vector3(0, 8.75f, 0);
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Vector3 scale = new Vector3(25, 25, 25);

        GameObject newObject = GameObject.Instantiate(locker, position, rotation) as GameObject;
        newObject.transform.localScale = scale;

        BoxCollider b;
        //box collider 1
        b = newObject.AddComponent<BoxCollider>();
        b.center = new Vector3(0, 0, 0);
        b.size = new Vector3(0.13f, 0.11f, 0.7f);

        newObject.tag = "locker";

        return newObject;
    }

    public GameObject CreateWhiteboardParameter()
    {
        Vector3 position = new Vector3(0, 8.75f, 0);
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Vector3 scale = new Vector3(20,20,20);

        GameObject newObject = GameObject.Instantiate(whiteboard, position, rotation) as GameObject;
        newObject.transform.localScale = scale;

        BoxCollider b;
        //box collider 1
        b = newObject.AddComponent<BoxCollider>();
        b.center = new Vector3(0, 0, 0);
        b.size = new Vector3(0.75f, 0.015f, 0.505f);

        newObject.tag = "whiteboard";
        newObject.AddComponent<WallAttach>();

        return newObject;
    }


    public GameObject CreateTVParameter()
    {
        Vector3 position = new Vector3(0, 8.28f, 0);
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Vector3 scale = new Vector3(20, 20, 20);

        GameObject newObject = GameObject.Instantiate(TV, position, rotation) as GameObject;
        newObject.transform.localScale = scale;

        BoxCollider b;
        //box collider 1
        b = newObject.AddComponent<BoxCollider>();
        b.center = new Vector3(0, 0, 0);
        b.size = new Vector3(0.45f, 0.4f, 0.828f);

        newObject.tag = "3DTV";

        return newObject;
    }

    public GameObject CreateCabinetParameter()
    {
        Vector3 position = new Vector3(0, 5.5f, 0);
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);

        GameObject newObject = GameObject.Instantiate(cabinet, position, rotation) as GameObject;

        BoxCollider b;
        //box collider 1
        b = newObject.AddComponent<BoxCollider>();
        b.center = new Vector3(0, 0, 0);
        b.size = new Vector3(4.6f, 12, 11);

        newObject.tag = "cabinet";



        return newObject;
    }
}
