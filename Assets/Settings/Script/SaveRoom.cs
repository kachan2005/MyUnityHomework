using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveRoom : MonoBehaviour {

    public StreamWriter file;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Stick" && OVRInput.GetDown(OVRInput.Button.One))
        {
            Save();
        }
        
            
    }

    private void Save()
    {
        Debug.Log("Save Call");
        File.Delete("C:\\Users\\knc007\\Desktop\\MyUnityHomework-master\\Assets\\data.txt");
        file = new StreamWriter("C:\\Users\\knc007\\Desktop\\MyUnityHomework-master\\Assets\\data.txt", true);

        GameObject objects = GameObject.Find("Objects");
        for(int i = 0; i < objects.transform.childCount; i++)
        {
            GameObject child = objects.transform.GetChild(i).gameObject;
            if (child.name != "Select")
            {
                Vector3 position = child.transform.position;
                Quaternion rotation = child.transform.rotation;
                //Debug.LogFormat("{0} Save Object {1}", i, child.tag);
                file.WriteLine(child.tag);
                file.WriteLine(position.x + "," + position.y + "," + position.z);
                file.WriteLine(rotation.x + "," + rotation.y + "," + rotation.z + "," + rotation.w);
            }
            else
            {
                //Debug.LogFormat("{0} Meet Select Object", i, child.name);
                for (int j = 0; j < child.transform.childCount; j++)
                {
                    GameObject g_child = child.transform.GetChild(j).gameObject;
                    Vector3 position = g_child.transform.position;
                    Quaternion rotation = g_child.transform.rotation;
                    Debug.LogFormat("{0}.{1} Save Object {2} from Select", i, j ,g_child.name);
                    file.WriteLine(g_child.tag);
                    file.WriteLine(position.x + "," + position.y + "," + position.z);
                    file.WriteLine(rotation.x + "," + rotation.y + "," + rotation.z + "," + rotation.w);
                }
            }
        }

        file.Close();
    }
}
