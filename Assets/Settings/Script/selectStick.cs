using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectStick : MonoBehaviour {

    public LayerMask grabMask;

    public float test;
    public MenuSignals menu;

    private bool trigger;
    private string spawnString = "chair";

	// Use this for initialization
	void Start () {
        menu = GameObject.Find("Menu").GetComponent<MenuSignals>();
        trigger = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(menu.mode == menu.MODE_SELECT_RAY)
        {
            if (Input.GetAxis("Oculus_GearVR_RIndexTrigger") == 1)
            {
                if (!trigger)
                {
                    //Debug.LogFormat("{0}: trigger is flase now", Input.GetAxis("Oculus_GearVR_RIndexTrigger"));
                    SelectItem();
                }
                trigger = true;
            }
            if (Input.GetAxis("Oculus_GearVR_RIndexTrigger") < 1) trigger = false;
        }
    }

    void SelectItem()
    {
        //Debug.LogFormat("Stick get trigger");
        Ray ray = new Ray(transform.position, transform.up);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, 3000.0f, grabMask))
        {
            rayHit.collider.gameObject.GetComponent<ApplyOutlined>().getSelected();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("menu"))
        {
            if(other.gameObject.tag != "Untagged" && OVRInput.GetDown(OVRInput.Button.One))
            {
                TextMesh t = GameObject.Find("AddObjectName").GetComponent<TextMesh>();
                t.text = other.gameObject.tag;
                //Debug.LogFormat("Trigger Object {0} with tag {1}", other.gameObject.name, other.gameObject.tag);
            }
        }
    }
    
}
