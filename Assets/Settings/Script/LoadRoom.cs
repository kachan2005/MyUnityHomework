using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class LoadRoom : MonoBehaviour {
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Stick" && OVRInput.GetDown(OVRInput.Button.One))
        {
            Load();
        }
    }

    private void Load()
    {
        Debug.Log("Load Call");
        GameObject objects = GameObject.Find("Objects");
        for (int i = 0; i < objects.transform.childCount; i++)
        {
            if(objects.transform.GetChild(i).gameObject.name != "Select")
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
        for(int i = 0; i < lines.Length; i++)
        {
            if( i % 3 == 0)
            {
                //Debug.LogFormat("Create Object {0}", lines[i]);
                newObject = spawn.CreateObject(lines[i]);
            }
            else if (i %3 == 1)
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
                Quaternion rotation = new Quaternion(x, y, z,w);
                newObject.transform.rotation = rotation;
            }
        }
        
    }
}
