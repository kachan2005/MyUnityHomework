using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class Crosshair : MonoBehaviour {

    public Material glass_material;
    public bool activative = false;
    public bool trigger = false;

    private GameObject inChart;

	// Use this for initialization
	void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            activative = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            trigger = false;
            activative = false;
        }
    }



    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag == "selectable")
        {
            //Debug.LogFormat("Crosshair enter select object {0}", coll.gameObject.name);
            GameObject d = coll.gameObject;
            if(d.GetComponent<Image>() == null) {
                for(int i = 0; i < d.transform.childCount; i++) {
                    if(d.transform.GetChild(i).GetComponent<Image>() != null) {
                        d.transform.GetChild(i).GetComponent<Image>().color = Color.red;
                        d.transform.GetChild(i).GetComponent<Image>().material = null;
                    }
                }
            }
            else {
                d.GetComponent<Image>().color = Color.red;
                d.GetComponent<Image>().material = null;
            }
        }

    }

    private void OnCollisionStay2D(Collision2D coll)
    {

        Debug.LogFormat("Crosshair select object {0}", coll.gameObject.name);
        if (activative)
        {
            if (trigger) return;
            trigger = true;
            if (coll.gameObject.name == "Dropdown")
            {
                Dropdown d = coll.gameObject.GetComponent<Dropdown>();
                if(d.transform.childCount > 3) {
                    d.Hide();
                }
                else {
                    d.Show();
                }
            }

            //Debug.LogFormat("Crosshair select object {0} and trigger", coll.gameObject.name);
            if (coll.gameObject.name == "Quit")
            {
                inChart = null;
                GameObject.Find("Menu").GetComponent<PauseSystem>().updateSystemPause(false);
            }
            
            if (coll.gameObject.name == "Item 0: Tile") {
                inChart = null;
                GameObject.Find("Purchase_List1").GetComponent<DisplayPurchaseList>().toggleDisplay();
            }

            float money = GameObject.Find("System_Menu").GetComponent<system_menu>().money;
            if (coll.gameObject.name == "Tile 1" && money > 200)
            {
                coll.transform.GetChild(0).GetComponent<AutoMoveAndRotate>().enabled = true;
                if (inChart != null) inChart.transform.GetChild(0).GetComponent<AutoMoveAndRotate>().enabled = false;
                inChart = coll.gameObject;
            }
            if (coll.gameObject.name == "Tile 2" && money > 400)
            {
                coll.transform.GetChild(0).GetComponent<AutoMoveAndRotate>().enabled = true;
                if (inChart != null) inChart.transform.GetChild(0).GetComponent<AutoMoveAndRotate>().enabled = false;
                inChart = coll.gameObject;
            }
            if (coll.gameObject.name == "Tile 3"  && money > 600)
            {
                coll.transform.GetChild(0).GetComponent<AutoMoveAndRotate>().enabled = true;
                if (inChart != null) inChart.transform.GetChild(0).GetComponent<AutoMoveAndRotate>().enabled = false;
                inChart = coll.gameObject;
            }
            if (coll.gameObject.name == "Tile 4" && money > 800)
            {
                coll.transform.GetChild(0).GetComponent<AutoMoveAndRotate>().enabled = true;
                if (inChart != null) inChart.transform.GetChild(0).GetComponent<AutoMoveAndRotate>().enabled = false;
                inChart = coll.gameObject;
            }
            if (coll.gameObject.name == "Tile 5" && money > 1000)
            {
                coll.transform.GetChild(0).GetComponent<AutoMoveAndRotate>().enabled = true;
                if (inChart != null) inChart.transform.GetChild(0).GetComponent<AutoMoveAndRotate>().enabled = false;
                inChart = coll.gameObject;
            }



            if (coll.gameObject.name == "Purchase") {
                checkout();
                coll.transform.parent.parent.parent.GetComponent<DisplayPurchaseList>().displayList(false);
            }


            if (coll.gameObject.name == "Quit_Purchase")
            {
                coll.transform.parent.parent.parent.GetComponent<DisplayPurchaseList>().displayList(false);
            }

        }
    }

    private void checkout() {
        if(inChart == null) {
            return;
        }

        switch(inChart.name)
        {
            case "Tile 1":
                GameObject.Find("Aircraft").GetComponent<SwitchTiles>().switchTiles(1);
                GameObject.Find("System_Menu").GetComponent<system_menu>().changeMoney(-200);
                break;
            case "Tile 2":
                GameObject.Find("Aircraft").GetComponent<SwitchTiles>().switchTiles(2);
                GameObject.Find("System_Menu").GetComponent<system_menu>().changeMoney(-400);
                break;
            case "Tile 3":
                GameObject.Find("Aircraft").GetComponent<SwitchTiles>().switchTiles(3);
                GameObject.Find("System_Menu").GetComponent<system_menu>().changeMoney(-600);
                break;
            case "Tile 4":
                GameObject.Find("Aircraft").GetComponent<SwitchTiles>().switchTiles(4);
                GameObject.Find("System_Menu").GetComponent<system_menu>().changeMoney(-800);
                break;
            case "Tile 5":
                GameObject.Find("Aircraft").GetComponent<SwitchTiles>().switchTiles(5);
                GameObject.Find("System_Menu").GetComponent<system_menu>().changeMoney(-1000);
                break;
        }


        inChart = null;
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "selectable")
        {
            GameObject d = coll.gameObject;
            if (d.GetComponent<Image>() == null)
            {
                for (int i = 0; i < d.transform.childCount; i++)
                {
                    if (d.transform.GetChild(i).GetComponent<Image>() != null)
                    {
                        d.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                        d.transform.GetChild(i).GetComponent<Image>().material = glass_material;
                    }
                }
            }
            else
            {
                d.GetComponent<Image>().color = Color.white;
                d.GetComponent<Image>().material = glass_material;
            }
        }


        if (coll.gameObject.name == "Dropdown")
        {
            Dropdown d = coll.gameObject.GetComponent<Dropdown>();
            //d.Hide();
        }
    }
}
