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

        GameObject.Find("System_Menu").GetComponent<system_menu>().closeSubMenu();
    }
	
	// Update is called once per frame
	void Update () {
        Color c = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color;

        if(c == Color.red && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)) {
            if(!trigger) {
                systemPause = !systemPause;
                trigger = true;


                updateSystemPause();
            }
        }
        else if(OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger)){
            trigger = false;
        }

        GameObject.Find("FPS").GetComponent<Text>().text = "FPS: " + Mathf.Round(100f * 1.0f / Time.deltaTime) / 100.0f;
        
	}

    public void updateSystemPause(bool s) {
        systemPause = s;
        updateSystemPause();
    }

    public void updateSystemPause() {
        
        if(systemPause == false) {
            GameObject.Find("Aircraft").GetComponent<AudioSource>().Play();
        }


        checkpoints.GetComponent<spawnRock>().system_pause = systemPause;
        for (int i = 0; i < checkpoints.transform.childCount; i++)
        {
            RockAction r = checkpoints.transform.GetChild(i).GetComponent<RockAction>();
            if (r != null) r.pauseRock(systemPause);
        }

        GameObject.Find("ControlBall").GetComponent<ControlBall2>().applySystemPause(systemPause);

        GameObject.Find("System_Menu").GetComponent<system_menu>().applySystemPause(systemPause);


        GameObject.Find("SelectBall").GetComponent<SelectObject>().applySystemPause(systemPause);
    }


}
