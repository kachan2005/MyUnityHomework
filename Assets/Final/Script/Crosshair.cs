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

    public float sliderMaxPostion;
    public float sliderMinPostion;

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

        //Debug.LogFormat("Crosshair enter object {0}", coll.gameObject.name);
        if (coll.gameObject.tag == "selectable")
        {
            //Debug.LogFormat("Crosshair enter select object {0}", coll.gameObject.name);
            GameObject d = coll.gameObject;
            if(d.GetComponent<Image>() != null)
            {
                d.GetComponent<Image>().color = Color.red;
                d.GetComponent<Image>().material = null;
            }
            else if (d.GetComponent<Slider>() != null)
            {
                ColorBlock colors = d.GetComponent<Slider>().colors;
                colors.normalColor = Color.red;
                d.GetComponent<Slider>().colors = colors;
            }
            else
            {
                for (int i = 0; i < d.transform.childCount; i++)
                {
                    if (d.transform.GetChild(i).GetComponent<Image>() != null)
                    {
                        d.transform.GetChild(i).GetComponent<Image>().color = Color.red;
                        d.transform.GetChild(i).GetComponent<Image>().material = null;
                    }
                }
            }
        }

    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        //Debug.LogFormat("Crosshair select object {0}", coll.gameObject.name);
        if (activative)
        {

            if (coll.gameObject.name == "RotateY")
            {
                GameObject pos = coll.transform.GetChild(0).gameObject;
                pos.transform.position = transform.position;
                Debug.LogFormat("    Slider 1 : get pos object");

                Vector3 position = pos.transform.localPosition;
                position.x = position.x > sliderMaxPostion ? sliderMaxPostion : (position.x < sliderMinPostion ? sliderMinPostion : position.x);
                position.y = 0;
                position.z = 0;
                pos.transform.localPosition = position;
                Debug.LogFormat("    Slider 2 : local position = ({0}, {1}, {2})", position.x, position.y, position.z);

                Slider s = coll.gameObject.GetComponent<Slider>();
                float value = (int)((position.x - sliderMinPostion) / (sliderMaxPostion - sliderMinPostion) * (s.maxValue - s.minValue));
                s.value = value;
                coll.transform.GetChild(5).GetComponent<Text>().text = "" + value;
                Debug.LogFormat("    Slider 3 : update slide value = {0}", value);

                GameObject.Find("ManipulationMenu").GetComponent<Manipulation>().RotateY(value);
            }


            //Avoid accidently activate that menu is not displayed
            Transform p = coll.transform;
            while(p != null)
            {
                if (p.GetComponent<DisplayMenu>() != null) break;
                p = p.parent;
            }
            if (p == null || p.GetComponent<DisplayMenu>().displayed == false) return;
            
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
            
            if (coll.gameObject.name == "Item 0: Tile") {
                inChart = null;
                //GameObject.Find("System_Menu").GetComponent<system_menu>().closeSubMenu();
                GameObject.Find("Purchase_List1").GetComponent<DisplayMenu>().toggleDisplay();
            }

            if (purchaseList1(coll)) // it is select from subMenu, so close out.
                return;
            
            if (coll.gameObject.name == "Quit")
            {
                inChart = null;
                GameObject.Find("Menu").GetComponent<PauseSystem>().updateSystemPause(false);
            }

            if (coll.gameObject.name == "Teleport Mode")
            {
                inChart = null;
                GameObject.Find("System_Menu").GetComponent<system_menu>().changeMode(1);
            }

            if (coll.gameObject.name == "Select Mode")
            {
                inChart = null;
                GameObject.Find("System_Menu").GetComponent<system_menu>().changeMode(2);
            }

            if (coll.gameObject.name == "Move Object")
            {
                inChart = null;
                GameObject.Find("System_Menu").GetComponent<system_menu>().changeMode(3);
            }

            if (coll.gameObject.name == "Quit Select")
            {
                coll.transform.parent.parent.parent.GetComponent<DisplayMenu>().displayList(false);
            }
        }
    }




    private bool purchaseList1(Collision2D coll) //handle 1st subMenu purchaseList 1
    {
        float money = GameObject.Find("System_Menu").GetComponent<system_menu>().money;
        if (coll.gameObject.name == "Tile 1" && money > 200 ||
            coll.gameObject.name == "Tile 2" && money > 400 ||
            coll.gameObject.name == "Tile 3" && money > 600 ||
            coll.gameObject.name == "Tile 4" && money > 800 ||
            coll.gameObject.name == "Tile 5" && money > 1000

            )
        {
            coll.transform.GetChild(0).GetComponent<AutoMoveAndRotate>().enabled = true;
            if (inChart != null) inChart.transform.GetChild(0).GetComponent<AutoMoveAndRotate>().enabled = false;
            inChart = coll.gameObject;

            return true;
        }



        if (coll.gameObject.name == "Purchase")
        {
            checkout();
            coll.transform.parent.parent.parent.GetComponent<DisplayMenu>().displayList(false);

            return true;
        }


        if (coll.gameObject.name == "Quit_Purchase")
        {
            coll.transform.parent.parent.parent.GetComponent<DisplayMenu>().displayList(false);
            return true;
        }
        
        return false;
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
            if (d.GetComponent<Image>() != null)
            {
                d.GetComponent<Image>().color = Color.white;
                d.GetComponent<Image>().material = glass_material;
            }
            else if (d.GetComponent<Slider>() != null)
            {
                ColorBlock colors = d.GetComponent<Slider>().colors;
                colors.normalColor = Color.white;
                d.GetComponent<Slider>().colors = colors;
            }
            else
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
        }


        if (coll.gameObject.name == "Dropdown")
        {
            Dropdown d = coll.gameObject.GetComponent<Dropdown>();
            //d.Hide();
        }
    }
}
