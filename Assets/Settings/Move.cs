using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CreateChairParameter(gameObject);
    }

    // Update is called once per frame
    void Update() {

        //Vector3 position = transform.position;
        //if (Input.GetKey(KeyCode.UpArrow))
        //{

        //    print("up arrow key was pressed");
        //    position.z += 0.01f;
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{

        //    print("dow arrow key was pressed");
        //    position.z -= 0.01f;
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{

        //    print("left arrow key was pressed");
        //    position.x += 0.01f;
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{

        //    print("right arrow key was pressed");
        //    position.x -= 0.01f;
        //}

        //transform.position = position;

        
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.LogFormat("Collision with {0}", collision.gameObject.name);
    }

    void CreateChairParameter(GameObject o)
    {

        Vector3 position = new Vector3(0, 3.75f, 0);
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Vector3 scale = new Vector3(0.65f, 0.65f, 0.65f);

        o.transform.position = position;
        o.transform.rotation = rotation;
        o.transform.localScale = scale;

        BoxCollider b;
        //box collider 1
        b = o.AddComponent<BoxCollider>();
        b.center = new Vector3(0, 2.3f, 2);
        b.size = new Vector3(6, 1.8f, 7.7f);
        //box collider 2
        b = o.AddComponent<BoxCollider>();
        b.center = new Vector3(-3.3f, -0.5f, 0.8f);
        b.size = new Vector3(1, 3, 2.6f);
        //box collider 3
        b = o.AddComponent<BoxCollider>();
        b.center = new Vector3(3.3f, -0.5f, 0.8f);
        b.size = new Vector3(1, 3, 2.6f);
        //box collider 4
        b = o.AddComponent<BoxCollider>();
        b.center = new Vector3(0, 0, -0.4f);
        b.size = new Vector3(7, 6.5f, 0.7f);
        //box collider 5
        b = o.AddComponent<BoxCollider>();
        b.center = new Vector3(-2.2f, -2.3f, -3);
        b.size = new Vector3(0.5f, 0.5f, 5.5f);
        //box collider 6
        b = o.AddComponent<BoxCollider>();
        b.center = new Vector3(2.2f, -2.3f, -3);
        b.size = new Vector3(0.5f, 0.5f, 5.5f);
        //box collider 5
        b = o.AddComponent<BoxCollider>();
        b.center = new Vector3(-2.2f, 1.6f, -3);
        b.size = new Vector3(0.5f, 1.5f, 5.5f);
        //box collider 6
        b = o.AddComponent<BoxCollider>();
        b.center = new Vector3(2.2f, 1.6f, -3);
        b.size = new Vector3(0.5f, 1.5f, 5.5f);

        Rigidbody r = o.AddComponent<Rigidbody>();
        r.mass = 50;
    }
}
