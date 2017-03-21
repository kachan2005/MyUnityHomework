using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class system_menu : MonoBehaviour {

    public float default_money;
    public float money;
    public float play_time;
    private float time = 0;

    public int mode;

	// Use this for initialization
	void Start () {
        money = 0;
        changeMoney(default_money);
        play_time = 0.0f; 
        GameObject.Find("PlayTime2").GetComponent<Text>().text = "PlayTime: " + Mathf.Round(100f * play_time) / 100.0f;

    }

    // Update is called once per frame
    void Update () {
		if(!transform.GetChild(0).GetComponent<Canvas>().enabled) {
            play_time += Time.deltaTime;
            GameObject.Find("PlayTime2").GetComponent<Text>().text = "PlayTime: " + Mathf.Round(100f * play_time) / 100.0f;
            GameObject.Find("PlayTime").GetComponent<Text>().text = "PlayTime: " + Mathf.Round(100f * play_time) / 100.0f;
        }

        Material skybox = RenderSettings.skybox;
        skybox.SetFloat("_Rotation", time);
        time += Time.deltaTime;
        
    }

    private void OnDestroy()
    {

        Material skybox = RenderSettings.skybox;
        skybox.SetFloat("_Rotation", 0);
        skybox.SetFloat("_Exposure", 1);
    }

    public void applySystemPause(bool system_pause)
    {
        GetComponent<DisplayMenu>().displayList(system_pause);

        if (system_pause)
        {
            changeMode(-1);
        }
        else
        {
            closeSubMenu();
            Destroy(GameObject.Find("Dropdown List"));
        }
    }

    public void changeMode(int mode_index)
    {
        if (mode == 4 && mode_index != -1) return;
        Text t = GameObject.Find("Mode").GetComponent<Text>();
        switch (mode_index)
        {
            case -1:
                t.text = "Choose A Mode";
                GameObject.Find("SelectBall").GetComponent<SelectObject>().display(false);
                break;
            case 1:
                t.text = "Teleport Mode";
                closeSubMenu();
                GameObject.Find("SelectBall").GetComponent<SelectObject>().display(true);
                break;
            case 2:
                t.text = "Select Mode";
                closeSubMenu();
                GameObject.Find("TransformMenu").GetComponent<DisplayMenu>().displayList(true);
                GameObject.Find("SelectBall").GetComponent<SelectObject>().display(true);

                //bool showLight = GameObject.Find("ManipulationMenu").GetComponent<Manipulation>().modifiedObject.GetComponent<Light>() != null;
                //GameObject.Find("LightMenu").GetComponent<DisplayMenu>().displayList(showLight);

                break;
            case 3:
                if (GameObject.Find("ManipulationMenu").GetComponent<Manipulation>().modifiedObject == null) return;
                t.text = "Move Object";
                break;

            case 4: //Purchase
                t.text = "Purchasing";
                closeSubMenu();
                GameObject.Find("SelectBall").GetComponent<SelectObject>().display(true);
                break;
        }

        mode = mode_index;
    }

    public void closeSubMenu()
    {
        Transform p = GameObject.Find("SubMenu").transform;
        for (int i = 0; i < p.childCount; i++)
        {
            if (p.GetChild(i).GetComponent<DisplayMenu>() != null)
            {
                p.GetChild(i).GetComponent<DisplayMenu>().displayList(false);
            }

            for (int j = 0; j < p.GetChild(i).childCount; j++)
            {
                if (p.GetChild(i).GetChild(j).GetComponent<DisplayMenu>() != null)
                {
                    p.GetChild(i).GetChild(j).GetComponent<DisplayMenu>().displayList(false);
                }
            }
        }
        GameObject.Find("SelectBall").GetComponent<SelectObject>().selectedObject = null;
        GameObject.Find("ManipulationMenu").GetComponent<Manipulation>().modifiedObject = null;
    }

    public void getCoin() {
        float amount = Mathf.Round(Mathf.Log10(play_time + 1.0f)) * 100;
        GameObject.Find("Message").GetComponent<Text>().text = "Gain Coin\nMoney " + amount;
        changeMoney( amount );
    }

    public void hitRock()
    {
        float amount = -Mathf.Round(play_time) * 10;
        GameObject.Find("Message").GetComponent<Text>().text = "Hit Rock\nMoney " + amount;
        changeMoney(amount);
    }

    public void changeMoney(float amount) {
        money += amount;
        GameObject.Find("Money").GetComponent<Text>().text = "Money: " + Mathf.Round(100f * money) / 100.0f;
        GameObject.Find("Money2").GetComponent<Text>().text = "Money: " + Mathf.Round(100f * money) / 100.0f;
    }
}
