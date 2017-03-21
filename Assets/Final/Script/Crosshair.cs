using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class Crosshair : MonoBehaviour {

    public Material glass_material;
    public Material toggle_material;
    public bool activative = false;
    public bool trigger = false;

    private GameObject inChart;
    private Quaternion originalRotation;

    public float sliderMaxPostion;
    public float sliderMinPostion;

    public AudioClip clickSound;
    public AudioClip coinDrop;

    // Use this for initialization
    void Start ()
    {
        Material skybox = RenderSettings.skybox;
        Color c = skybox.GetColor("_Tint");
        float i = skybox.GetFloat("_Exposure");

        GameObject.Find("Skybox Red").GetComponent<Slider>().value = c.r * 255;
        GameObject.Find("Skybox Red").transform.GetChild(5).GetComponent<Text>().text = "" + c.r * 255;
        GameObject.Find("Skybox Green").GetComponent<Slider>().value = c.g * 255;
        GameObject.Find("Skybox Green").transform.GetChild(5).GetComponent<Text>().text = "" + c.g * 255;
        GameObject.Find("Skybox Blue").GetComponent<Slider>().value = c.b * 255;
        GameObject.Find("Skybox Blue").transform.GetChild(5).GetComponent<Text>().text = "" + c.b * 255;
        GameObject.Find("Skybox Intensity").GetComponent<Slider>().value = i;
        GameObject.Find("Skybox Intensity").transform.GetChild(5).GetComponent<Text>().text = "" + i;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            activative = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
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
            else if (d.GetComponent<Toggle>() != null)
            {
                for (int i = 0; i < d.transform.childCount; i++)
                {
                    if (d.transform.GetChild(i).GetComponent<Image>() != null)
                    {
                        d.transform.GetChild(i).GetComponent<Image>().color = Color.blue;
                        d.transform.GetChild(i).GetComponent<Image>().material = null;
                    }
                }
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
        //    Debug.LogFormat("Crosshair select object {0}", coll.gameObject.name);
        if (activative)
        {
            //Avoid accidently activate that menu is not displayed
            Transform parent = coll.transform;
            while(parent != null)
            {
                if (parent.GetComponent<DisplayMenu>() != null) break;
                parent = parent.parent;
            }
            if (parent == null || parent.GetComponent<DisplayMenu>().displayed == false) return;


            if (manipulation(coll))
            {
                AudioSource.PlayClipAtPoint(clickSound, coll.transform.position);
                return;
            }
            if (skybox(coll))
            {
                AudioSource.PlayClipAtPoint(clickSound, coll.transform.position);
                return;
            }

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
                AudioSource.PlayClipAtPoint(clickSound, coll.transform.position);
                return;
            }

            if (coll.gameObject.name == "Item 0: Tile")
            {
                inChart = null;
                GameObject.Find("System_Menu").GetComponent<system_menu>().closeSubMenu();
                GameObject.Find("Purchase_List1").GetComponent<DisplayMenu>().displayList(true);
                GameObject.Find("Dropdown").GetComponent<Dropdown>().value = 0;
                GameObject.Find("Dropdown").GetComponent<Dropdown>().Hide();
                AudioSource.PlayClipAtPoint(clickSound, coll.transform.position);
                return;
            }
            if (coll.gameObject.name == "Item 1: Fruntiure")
            {
                inChart = null;
                GameObject.Find("System_Menu").GetComponent<system_menu>().closeSubMenu();
                GameObject.Find("Purchase_List2").GetComponent<DisplayMenu>().displayList(true);
                GameObject.Find("Dropdown").GetComponent<Dropdown>().value = 1;
                GameObject.Find("Dropdown").GetComponent<Dropdown>().Hide();
                AudioSource.PlayClipAtPoint(clickSound, coll.transform.position);
                return;
            }


            float money = GameObject.Find("System_Menu").GetComponent<system_menu>().money;
            if (coll.transform.GetChild(0).GetComponent<Purchase>() != null)
            {
                Debug.LogFormat("Crosshair select object {0}", coll.gameObject.name);
                coll.gameObject.GetComponent<Toggle>().isOn = true;

                Purchase p = coll.transform.GetChild(0).GetComponent<Purchase>();
                if(money > p.cost) {

                    clearChart();
                    inChart = p.gameObject;
                    originalRotation = inChart.transform.localRotation;
                    p.GetComponent<AutoMoveAndRotate>().enabled = true;
                }
                AudioSource.PlayClipAtPoint(clickSound, coll.transform.position);
                return;
            }
            
            if (coll.gameObject.name == "Purchase")
            {
                if (inChart != null)
                {
                    AudioSource.PlayClipAtPoint(coinDrop, coll.transform.position);
                    if (inChart.gameObject.name == "Tile 1" ||
                        inChart.gameObject.name == "Tile 2" ||
                        inChart.gameObject.name == "Tile 3" ||
                        inChart.gameObject.name == "Tile 4" ||
                        inChart.gameObject.name == "Tile 5")
                    {
                        inChart.GetComponent<Purchase>().checkout(0);
                    }
                    else
                    {
                        inChart.GetComponent<Purchase>().checkout(1);
                    }
                }

                coll.transform.parent.parent.parent.GetComponent<DisplayMenu>().displayList(false);

                return;
            }


            if (coll.gameObject.name == "Quit_Purchase")
            {
                clearChart();
                coll.transform.parent.parent.parent.GetComponent<DisplayMenu>().displayList(false);
                AudioSource.PlayClipAtPoint(clickSound, coll.transform.position);
                return;
                return;
            }


            if (coll.gameObject.name == "Tutorial")
            {
                GameObject.Find("TutorialMenu").GetComponent<DisplayMenu>().toggleDisplay();
                AudioSource.PlayClipAtPoint(clickSound, coll.transform.position);
                return;
            }
            if (coll.gameObject.name == "Quit Tutorial")
            {
                GameObject.Find("TutorialMenu").GetComponent<DisplayMenu>().displayList(false);
                AudioSource.PlayClipAtPoint(clickSound, coll.transform.position);
                return;
            }


            if (coll.gameObject.name == "Skybox")
            {
                GameObject.Find("SkyboxMenu").GetComponent<DisplayMenu>().toggleDisplay();
                AudioSource.PlayClipAtPoint(clickSound, coll.transform.position);
                return;
            }

            if (coll.gameObject.name == "Quit")
            {
                clearChart();
                GameObject.Find("Menu").GetComponent<PauseSystem>().updateSystemPause(false);
                AudioSource.PlayClipAtPoint(clickSound, coll.transform.position);
                return;
            }

            if (coll.gameObject.name == "Teleport Mode")
            {
                clearChart();
                GameObject.Find("System_Menu").GetComponent<system_menu>().changeMode(1);
                AudioSource.PlayClipAtPoint(clickSound, coll.transform.position);
                return;
            }

            if (coll.gameObject.name == "Select Mode")
            {
                clearChart();
                GameObject.Find("System_Menu").GetComponent<system_menu>().changeMode(2);
                AudioSource.PlayClipAtPoint(clickSound, coll.transform.position);
                return;
            }

            if (coll.gameObject.name == "Move Object")
            {
                clearChart();
                GameObject.Find("System_Menu").GetComponent<system_menu>().changeMode(3);
                AudioSource.PlayClipAtPoint(clickSound, coll.transform.position);
                return;
            }

            if (coll.gameObject.name == "Quit Select")
            {
                GameObject.Find("System_Menu").GetComponent<system_menu>().changeMode(-1);
                GameObject.Find("System_Menu").GetComponent<system_menu>().closeSubMenu();
                AudioSource.PlayClipAtPoint(clickSound, coll.transform.position);
                return;
            }
        }
    }


    private float getSliderValue(Collision2D coll) {
        GameObject pos = coll.transform.GetChild(0).gameObject;
        pos.transform.position = transform.position;

        Vector3 position = pos.transform.localPosition;
        position.x = position.x > sliderMaxPostion ? sliderMaxPostion : (position.x < sliderMinPostion ? sliderMinPostion : position.x);
        position.y = 0;
        position.z = 0;
        pos.transform.localPosition = position;

        Slider s = coll.gameObject.GetComponent<Slider>();

        return (position.x - sliderMinPostion) / (sliderMaxPostion - sliderMinPostion) * (s.maxValue - s.minValue);

    }

    private bool manipulation(Collision2D coll)
    {
        if (coll.gameObject.name == "RotateY")
        {
            Slider s = coll.gameObject.GetComponent<Slider>();
            float value = (int)getSliderValue(coll);
            s.value = value;
            coll.transform.GetChild(5).GetComponent<Text>().text = "" + value;
            //Debug.LogFormat("    Slider 3 : update slide value = {0}", value);

            GameObject.Find("ManipulationMenu").GetComponent<Manipulation>().RotateY(value);
            return true;
        }

        if (coll.gameObject.name == "Light Red")
        {
            Slider s = coll.gameObject.GetComponent<Slider>();
            float value = (int)getSliderValue(coll);
            s.value = value;
            coll.transform.GetChild(5).GetComponent<Text>().text = "" + value;

            value /= 255.0f;
            GameObject.Find("ManipulationMenu").GetComponent<Manipulation>().updateLightColor(value, 1);
            return true;
        }
        if (coll.gameObject.name == "Light Blue")
        {
            Slider s = coll.gameObject.GetComponent<Slider>();
            float value = (int)getSliderValue(coll);
            s.value = value;
            coll.transform.GetChild(5).GetComponent<Text>().text = "" + value;

            value /= 255.0f;
            GameObject.Find("ManipulationMenu").GetComponent<Manipulation>().updateLightColor(value, 3);
            return true;
        }
        if (coll.gameObject.name == "Light Green")
        {
            Slider s = coll.gameObject.GetComponent<Slider>();
            float value = (int)getSliderValue(coll);
            s.value = value;
            coll.transform.GetChild(5).GetComponent<Text>().text = "" + value;

            value /= 255.0f;
            GameObject.Find("ManipulationMenu").GetComponent<Manipulation>().updateLightColor(value, 2);
            return true;
        }
        if (coll.gameObject.name == "Light Intensity")
        {
            Slider s = coll.gameObject.GetComponent<Slider>();
            float value = getSliderValue(coll);
            s.value = value;
            coll.transform.GetChild(5).GetComponent<Text>().text = "" + value;

            //value /= 8.0f;

            Debug.LogFormat("Light Intensity = {0}", value);
            GameObject.Find("ManipulationMenu").GetComponent<Manipulation>().updateLightColor(value, 4);
            return true;
        }

        return false;
    }
    private bool skybox(Collision2D coll)
    {
        Material skybox = RenderSettings.skybox;
        Color c = skybox.GetColor("_Tint");
        if (coll.gameObject.name == "Skybox Red")
        {
            Slider s = coll.gameObject.GetComponent<Slider>();
            float value = (int)getSliderValue(coll);
            s.value = value;
            coll.transform.GetChild(5).GetComponent<Text>().text = "" + value;

            value /= 255.0f;
            Debug.LogFormat("Crosshair select object {0}, value = {1}, slider value = {2}", coll.gameObject.name, value, s.value);
            c.r = value;
            skybox.SetColor("_Tint", c);
            return true;
        }
        if (coll.gameObject.name == "Skybox Blue")
        {
            Slider s = coll.gameObject.GetComponent<Slider>();
            float value = (int)getSliderValue(coll);
            s.value = value;
            coll.transform.GetChild(5).GetComponent<Text>().text = "" + value;

            value /= 255.0f;
            Debug.LogFormat("Crosshair select object {0}, value = {1}, slider value = {2}", coll.gameObject.name, value, s.value);
            c.b = value;
            skybox.SetColor("_Tint", c);
            return true;
        }
        if (coll.gameObject.name == "Skybox Green")
        {
            Slider s = coll.gameObject.GetComponent<Slider>();
            float value = (int)getSliderValue(coll);
            s.value = value;
            coll.transform.GetChild(5).GetComponent<Text>().text = "" + value;

            value /= 255.0f;
            Debug.LogFormat("Crosshair select object {0}, value = {1}, slider value = {2}", coll.gameObject.name, value, s.value);
            c.g = value;
            skybox.SetColor("_Tint", c);
            return true;
        }
        if (coll.gameObject.name == "Skybox Intensity")
        {
            Slider s = coll.gameObject.GetComponent<Slider>();
            float value = getSliderValue(coll);
            s.value = value;
            coll.transform.GetChild(5).GetComponent<Text>().text = "" + value;

            skybox.SetFloat("_Exposure", value);
            return true;
        }

        return false;
    }

    public void clearChart() {
        if (inChart != null)
        {
            Debug.LogFormat("chart object: {0}", inChart.name);
            inChart.GetComponent<AutoMoveAndRotate>().enabled = false;
            inChart.transform.localRotation = originalRotation;
            inChart.transform.parent.GetComponent<Toggle>().isOn = false;
            inChart = null;
        }
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
            else if (d.GetComponent<Toggle>() != null)
            {
                for (int i = 0; i < d.transform.childCount; i++)
                {
                    if (d.transform.GetChild(i).GetComponent<Image>() != null)
                    {
                        d.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                        d.transform.GetChild(i).GetComponent<Image>().material = toggle_material;
                    }
                }
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
