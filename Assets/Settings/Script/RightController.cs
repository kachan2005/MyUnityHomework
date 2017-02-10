using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
public class RightController : MonoBehaviour {

    public OVRInput.Controller controller;

    private Vector3 direction;
    private bool shooting;

    public MenuSignals menu;

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
                Vector3 currentPosition = OVRInput.GetLocalControllerPosition(controller);
                Vector3 diff = currentPosition - lastCursorPosition;
                Debug.LogFormat("Diff Position: ({0}, {1}, {2})", diff.x, diff.y, diff.z);
                
                Vector3 rotation = child.transform.rotation.eulerAngles;
                rotation.z += diff.x * -250;
                child.transform.rotation = Quaternion.Euler(rotation);
            }
        }
        else if(menu.mode == menu.MODE_MOVE && isAction)
        {
            Transform select =  GameObject.Find("Select").transform;
            Vector3 currentPosition = OVRInput.GetLocalControllerPosition(controller);
            Vector3 diff = currentPosition - lastCursorPosition;
            Debug.LogFormat("Diff Position: ({0}, {1}, {2})", diff.x, diff.y, diff.z);

            Vector3 position = select.position;
            position += diff * -25;
            select.position = position;
            
        }

        lastCursorPosition = OVRInput.GetLocalControllerPosition(controller);
    }


    private void Load()
    {
        Debug.Log("Load Call");
        GameObject objects = GameObject.Find("Objects");
        for (int i = 0; i < objects.transform.childCount; i++)
        {
            if (objects.transform.GetChild(i).gameObject.name != "Select")
            {
                Destroy(objects.transform.GetChild(i).gameObject);
            }
            else
            {
                for (int j = 0; j < objects.transform.GetChild(i).childCount; j++)
                {
                    GameObject g_child = objects.transform.GetChild(i).GetChild(j).gameObject;
                    Destroy(g_child);
                }
            }
        }

        SpawnObjects spawn = GameObject.Find("Menu").GetComponent<SpawnObjects>();
        string[] lines = System.IO.File.ReadAllLines("C:\\Users\\knc007\\Desktop\\MyUnityHomework-master\\Assets\\data.txt");
        GameObject newObject = null;
        for (int i = 0; i < lines.Length; i++)
        {
            if (i % 3 == 0)
            {
                Debug.LogFormat("Create Object {0}", lines[i]);
                newObject = spawn.CreateObject(lines[i]);
            }
            else if (i % 3 == 1)
            {
                string[] tokens = lines[i].Split(',');
                float x, y, z;
                float.TryParse(tokens[0], out x);
                float.TryParse(tokens[1], out y);
                float.TryParse(tokens[2], out z);
                Vector3 position = new Vector3(x, y, z);
                newObject.transform.position = position;
            }
            else
            {
                string[] tokens = lines[i].Split(',');
                float x, y, z, w;
                float.TryParse(tokens[0], out x);
                float.TryParse(tokens[1], out y);
                float.TryParse(tokens[2], out z);
                float.TryParse(tokens[3], out w);
                Quaternion rotation = new Quaternion(x, y, z, w);
                newObject.transform.rotation = rotation;
            }
        }

    }
    private void Save()
    {
        Debug.Log("Save Call");
        File.Delete("C:\\Users\\knc007\\Desktop\\MyUnityHomework-master\\Assets\\data.txt");
        StreamWriter file = new StreamWriter("C:\\Users\\knc007\\Desktop\\MyUnityHomework-master\\Assets\\data.txt", true);

        GameObject objects = GameObject.Find("Objects");
        for (int i = 0; i < objects.transform.childCount; i++)
        {
            GameObject child = objects.transform.GetChild(i).gameObject;
            if (child.name != "Select")
            {
                Vector3 position = child.transform.position;
                Quaternion rotation = child.transform.rotation;
                Debug.LogFormat("{0} Save Object {1}", i, child.tag);
                file.WriteLine(child.tag);
                file.WriteLine(position.x + "," + position.y + "," + position.z);
                file.WriteLine(rotation.x + "," + rotation.y + "," + rotation.z + "," + rotation.w);
            }
            else
            {
                Debug.LogFormat("{0} Meet Select Object", i, child.name);
                for (int j = 0; j < child.transform.childCount; j++)
                {
                    GameObject g_child = child.transform.GetChild(j).gameObject;
                    Vector3 position = g_child.transform.position;
                    Quaternion rotation = g_child.transform.rotation;
                    Debug.LogFormat("{0}.{1} Save Object {2} from Select", i, j, g_child.name);
                    file.WriteLine(g_child.tag);
                    file.WriteLine(position.x + "," + position.y + "," + position.z);
                    file.WriteLine(rotation.x + "," + rotation.y + "," + rotation.z + "," + rotation.w);
                }
            }
        }

        file.Close();
    }
}
