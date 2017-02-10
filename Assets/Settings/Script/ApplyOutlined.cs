using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyOutlined : MonoBehaviour {

    public Material theMaterial;
    public Material[,] oldMaterials;
    int count = 0;
    bool isSelected = false;

    const float CHAIR_HEIGHT = 3.75f;
    const float DESK_HEIGHT = 4.6f;
    const float TV_HEIGHT = 8.28f;
    const float CABINET_HEIGHT = 5.5f;
    const float LOCKER_HEIGHT = 8.75f;

    //public Renderer[] renderers;


    // Use this for initialization
    void Start ()
    {
        oldMaterials = new Material[100, 10];
    }
	
	// Update is called once per frame
	void Update () {

        StayGround();
        
	}

    public void getSelected()
    {
        isSelected = !isSelected;
        if (isSelected)
        {
            count = 0;
            oldMaterials = new Material[100, 10];
            //renderers = new Renderer[0];
            applyHighLights(gameObject);
            transform.parent = GameObject.Find("Select").transform;
        }
        else
        {
            count = 0;
            unSelectHighLights(gameObject);
            oldMaterials = new Material[100, 10];
            transform.parent = null;
        }
    }

    private void applyHighLights(GameObject o)
    {
        Renderer r = o.GetComponent<Renderer>();
        if (r != null)
        {
            int length = oldMaterials.Length;
            int i = 0;
            for(; i < r.materials.Length; i++)
            {
                oldMaterials[count, i] = r.materials[i];
            }
            oldMaterials[count, i] = null;
            r.materials = new Material[1] { theMaterial };
            count++;
        }
        else
        {
            int childrenCount = o.transform.childCount;
            for (int i = 0; i < childrenCount; i++)
            {
                GameObject child = o.transform.GetChild(i).gameObject;
                applyHighLights(child);
            }
        }
    }

    private void unSelectHighLights(GameObject o)
    {
        Renderer r = o.GetComponent<Renderer>();
        if (r != null)
        {
            Material[] m = new Material[0];
            int i = 0;
            while(oldMaterials[count, i] != null)
            {
                System.Array.Resize<Material>(ref m, i + 1);
                m[i] = oldMaterials[count, i];
                i++;
            }
            r.materials = m;
            count++;
        }
        else
        {
            int childrenCount = o.transform.childCount;
            for (int i = 0; i < childrenCount; i++)
            {
                GameObject child = o.transform.GetChild(i).gameObject;
                unSelectHighLights(child);
            }
        }

    }

    void StayGround()
    {
        Vector3 position = transform.position;
        Vector3 rotation = transform.rotation.eulerAngles;
        switch (gameObject.tag)
        {
            case "chair":
                position.y = CHAIR_HEIGHT;
                transform.position = position;

                rotation.x = -90;
                transform.rotation = Quaternion.Euler(rotation);
                break;
            case "desk":
                position.y = DESK_HEIGHT;
                transform.position = position;

                rotation.x = -90;
                transform.rotation = Quaternion.Euler(rotation);
                break;
            case "locker":
                position.y = LOCKER_HEIGHT;
                transform.position = position;

                rotation.x = -90;
                transform.rotation = Quaternion.Euler(rotation);
                break;
            case "cabinet":
                position.y = CABINET_HEIGHT;
                transform.position = position;

                rotation.x = -90;
                transform.rotation = Quaternion.Euler(rotation);
                break;
            case "3DTV":
                position.y = TV_HEIGHT;
                transform.position = position;

                rotation.x = -90;
                transform.rotation = Quaternion.Euler(rotation);
                break;

        }
    }


    
}
