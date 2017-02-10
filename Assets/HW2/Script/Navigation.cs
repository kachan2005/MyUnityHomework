using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour {

    public LayerMask grabMask;

    public MenuSignals menu;
    public GameObject stick;

    public float distance = 4.0f;
    private const float MIN_DISTANCE = 2.0f;

    private bool trigger;

    private Vector3 groundScale = new Vector3(1, 10, 1);
    public float set_distance;

    // Use this for initialization
    void Start () {
        menu = GameObject.Find("Menu").GetComponent<MenuSignals>();
        stick = GameObject.Find("Stick");
    }
	
	// Update is called once per frame
	void Update () {
		if( menu.mode == menu.MODE_SELECT_BALL || menu.mode == menu.MODE_ADD || menu.mode == menu.MODE_TELEPORT)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            float diff = distance - set_distance;
            float r_length = distance + (diff < 0 ? 0 : diff * diff);
            Vector3 position = stick.transform.position + stick.transform.up * r_length;

            if(menu.mode == menu.MODE_SELECT_BALL)
            {
                transform.position = position;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                position.y = menu.mode != menu.MODE_SELECT_BALL ? 0 : position.y;
                transform.position = position;
                transform.localScale = groundScale;
            }

            if (Input.GetAxis("Oculus_GearVR_RIndexTrigger") == 1)
            {
                if (!trigger)
                {
                    if (menu.mode == menu.MODE_SELECT_BALL)
                    {
                        SelectItem();
                    }
                    else if( menu.mode == menu.MODE_ADD)
                    {
                        menu.CreateObject(position);
                    }
                    else if( menu.mode == menu.MODE_TELEPORT)
                    {
                        GameObject player = GameObject.Find("OVRPlayerController");
                        Vector3 playerPosition = player.transform.position;
                        position.y = playerPosition.y;
                        player.transform.position = position;
                    }
                }
                trigger = true;
            }
            if (Input.GetAxis("Oculus_GearVR_RIndexTrigger") < 1) trigger = false;

            distance += -0.05f * Input.GetAxis("Oculus_GearVR_LThumbstickY");
            distance = distance < MIN_DISTANCE ? MIN_DISTANCE : distance;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }


	}

    void SelectItem()
    {
        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, 1.0f, transform.forward, 0f, grabMask);

        if (hits.Length > 0)
        {
            int closestHit = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance > hits[closestHit].distance) closestHit = i;
                Debug.LogFormat("{0} GrabObject name: {1}", i, hits[i].collider.gameObject.name);
            }

            hits[closestHit].collider.gameObject.GetComponent<ApplyOutlined>().getSelected();
        }
    }
}
