using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

    float speed = 0.1f;
    LineRenderer lr;

	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
        movement();
        
    }

    private bool detectCollide(Vector3 position)
    {
        Ray right = new Ray(position, transform.right);
        Ray left = new Ray(position, transform.right * -1);
        Ray forward = new Ray(position, transform.forward);
        Ray backward = new Ray(position, transform.forward * -1);
        bool isCollide = false;
        RaycastHit rayHit;

        if (Physics.Raycast(right, out rayHit, 0.5f))
        {
            Debug.LogFormat("You hit {0} on the right!", rayHit.collider.name);
            isCollide = true;
        }
        if (Physics.Raycast(left, out rayHit, 0.5f))
        {
            Debug.LogFormat("You hit {0} on the left!", rayHit.collider.name);
            isCollide = true;
        }
        if (Physics.Raycast(forward, out rayHit, 0.5f))
        {
            Debug.LogFormat("You hit {0} on the forward!", rayHit.collider.name);
            isCollide = true;
        }
        if (Physics.Raycast(backward, out rayHit, 0.5f))
        {
            Debug.LogFormat("You hit {0} on the back!", rayHit.collider.name);
            isCollide = true;
        }

        return isCollide;
    }

    private void movement()
    {

        Vector3 direction = Vector3.zero;
        Vector3 newPosition;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction = transform.forward;
            //Debug.Log("You pressed the 'LeftArrow' key");
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            direction = transform.forward * -1;
            //Debug.Log("You pressed the 'RightArrow' key");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = transform.right * -1;
            //Debug.Log("You pressed the 'LeftArrow' key");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = transform.right;
            //Debug.Log("You pressed the 'RightArrow' key");
        }

        newPosition = transform.position + direction;
        if (!detectCollide(newPosition))
        {
            transform.Translate(direction * speed);
            newPosition = transform.position;
            newPosition.y = 3.0f;
            transform.position = newPosition;
        }
    }
}
