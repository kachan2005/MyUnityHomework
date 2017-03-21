using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purchase : MonoBehaviour {

    public GameObject purchaseObject;
    public GameObject newObject;
    public float cost;

	// Use this for initialization
	void Start ()
    {
        Debug.LogFormat("gameObject name {0}", gameObject.name);
        Debug.LogFormat("purchaseObject name {0}", purchaseObject.name);
        Transform p = transform.parent;
        Debug.LogFormat("parent name {0}, child count = {1}", p.name, p.childCount);
        Transform c = p.GetChild(2);
        Debug.LogFormat("childe 2 , name {0}", c.name);
        if (c.GetComponent<Text>() != null)
            c.GetComponent<Text>().text = purchaseObject.name + ": $" + cost;

        newObject = null;
	}

    // Update is called once per frame
    void Update()
    {
        if (newObject == null) return;

        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            GameObject.Find("System_Menu").GetComponent<system_menu>().changeMode(-1);
            GameObject.Find("SelectBall").GetComponent<SelectObject>().display(false);
            newObject = null;
        }
        else
        {
            if (!GameObject.Find("SelectBall").GetComponent<SelectObject>().inBounard()) return;
            newObject.transform.position = GameObject.Find("SelectBall").transform.position; 
        }
    }

    public void checkout(int objectType) {
        GameObject.Find("System_Menu").GetComponent<system_menu>().changeMoney(-cost);

        if (objectType == 0) { //tile
            Material m = purchaseObject.GetComponent<Renderer>().material;
            GameObject.Find("Aircraft").GetComponent<SwitchTiles>().switchTiles(m);
            return;
        }

        Vector3 position = GameObject.Find("SelectBall").transform.position;
        Quaternion rotation = Quaternion.Euler(Vector3.zero);

        newObject = GameObject.Instantiate(purchaseObject, position, rotation) as GameObject;
        newObject.transform.parent = GameObject.Find("Furniture").transform;
        newObject.name = purchaseObject.name;

        GameObject.Find("SelectBall").GetComponent<SelectObject>().display(true);
        GameObject.Find("System_Menu").GetComponent<system_menu>().changeMode(4);
    }
}
