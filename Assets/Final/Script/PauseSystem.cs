using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseSystem : MonoBehaviour {

    public bool systemPause;
    public bool trigger;

    GameObject checkpoints;

	// Use this for initialization
	void Start () {
        trigger = false;
        checkpoints = GameObject.Find("Checkpoints");

        systemPause = true;
        updateSystemPause();
    }
	
	// Update is called once per frame
	void Update () {
        Color c = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color;

        if(c == Color.red && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)) {
            if(!trigger) {
                systemPause = !systemPause;
                trigger = true;


                updateSystemPause();
            }
        }
        else if(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)){
            trigger = false;
        }

        GameObject.Find("FPS").GetComponent<Text>().text = "FPS: " + Mathf.Round(100f * 1.0f / Time.deltaTime) / 100.0f;

        
	}

    public void updateSystemPause(bool s) {
        systemPause = s;
        updateSystemPause();
    }

    public void updateSystemPause() {

        checkpoints.GetComponent<spawnRock>().system_pause = systemPause;
        for (int i = 0; i < checkpoints.transform.childCount; i++)
        {
            RockAction r = checkpoints.transform.GetChild(i).GetComponent<RockAction>();
            if (r != null) r.system_pause = systemPause;
        }

        GameObject.Find("display_coin").GetComponent<Renderer>().enabled = systemPause;
        GameObject canvas = GameObject.Find("System_Menu").transform.GetChild(0).gameObject;
        canvas.GetComponent<Canvas>().enabled = systemPause;
        
        if(systemPause == false) {
            Transform p = GameObject.Find("Purchase_Lists").transform;
            for(int i = 0; i < p.childCount; i++) {
                if(p.GetComponent<DisplayPurchaseList>() != null) {
                    p.GetComponent<DisplayPurchaseList>().displayList(false);
                }
            }
        }
    }


}
