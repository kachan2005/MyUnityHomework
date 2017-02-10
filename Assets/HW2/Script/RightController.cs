using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
public class RightController : MonoBehaviour {

    public OVRInput.Controller controller;

    private Vector3 direction;
    private bool shooting;

    public MenuSignals menu;
    public Vector3 dx;

    private Vector3 lastCursorPosition;
    private Vector3 lastValue;

    private bool isAction;

    // Use this for initialization
    void Start()
    {
        Debug.LogFormat("Controller Info: {0}", controller.ToString());
        menu = GameObject.Find("Menu").GetComponent<MenuSignals>();
        lastCursorPosition = OVRInput.GetLocalControllerPosition(controller);
        isAction = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
        transform.localRotation = OVRInput.GetLocalControllerRotation(controller);

        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            isAction = !isAction;
        }

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            menu.changeMode();
        }

        if(menu.mode == menu.MODE_ROTATE && isAction)
        {
            if(GameObject.Find("Select").transform.childCount == 1)
            {
                GameObject child = GameObject.Find("Select").transform.GetChild(0).gameObject;
                if (child.tag != "whiteboard" || child.GetComponent<WallAttach>().attachToWall == false)
                {
                    Vector3 currentPosition = OVRInput.GetLocalControllerPosition(controller);
                    Vector3 diff = currentPosition - lastCursorPosition;
                    Debug.LogFormat("Diff Position: ({0}, {1}, {2})", diff.x, diff.y, diff.z);

                    Vector3 rotation = child.transform.rotation.eulerAngles;
                    rotation.z += diff.x * -250;
                    child.transform.rotation = Quaternion.Euler(rotation);
                }
            }
        }
        else if(menu.mode == menu.MODE_MOVE && isAction)
        {
            movement();
        }

        lastCursorPosition = OVRInput.GetLocalControllerPosition(controller);
    }

    private void movement()
    {
        bool hasWhiteboard = false;
        bool onlyWhiteboard = true;
        Transform select = GameObject.Find("Select").transform;
        for (int i = 0; i < select.childCount; i++)
        {
            GameObject child = select.GetChild(i).gameObject;
            if (child.tag != "whiteboard") onlyWhiteboard = false;
            if (child.tag == "whiteboard") hasWhiteboard = true;
        }

        if(hasWhiteboard && !onlyWhiteboard)
        {
            Debug.LogFormat("Not supposed to move");
            isAction = false;
        }
        else
        {
            Vector3 currentPosition = OVRInput.GetLocalControllerPosition(controller);
            Vector3 diff = currentPosition - lastCursorPosition;
            //Debug.LogFormat("Diff Position: ({0}, {1}, {2})", diff.x, diff.y, diff.z);
            if (!hasWhiteboard) //normal movement
            {
                Vector3 position = select.position;
                position += diff * -25;
                select.position = position;
            }
            else
            {
                GameObject whiteboard = select.GetChild(0).gameObject;
                if(whiteboard.GetComponent<WallAttach>().attachToWall == false)
                {
                    Vector3 position = select.position;
                    position += diff * -25;
                    select.position = position;
                }
                else
                {
                    Vector3 position = whiteboard.transform.position;
                    dx = whiteboard.transform.right * diff.x;
                    Vector3 dy = whiteboard.transform.forward * diff.y;
                    //diff.z = diff.x;
                    //diff.x = 0;
                    position += (dx * -1 + dy) * 25;
                    position.y = position.y < 14.12f ? position.y : 14.12f;
                    whiteboard.transform.position = position;
                }
            }
        }
    }
}
