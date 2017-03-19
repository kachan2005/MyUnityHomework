using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class system_menu : MonoBehaviour {

    public float default_money;
    public float money;
    public float play_time;

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
        }
	}


    public void getCoin() {
        changeMoney( Mathf.Log10(play_time + 1.0f) );
    }

    public void hitRock() {
        changeMoney(-play_time);
    }

    public void changeMoney(float amount) {
        money += amount;
        GameObject.Find("Money").GetComponent<Text>().text = "Money: " + Mathf.Round(100f * money) / 100.0f;
        GameObject.Find("Money2").GetComponent<Text>().text = "Money: " + Mathf.Round(100f * money) / 100.0f;
    }
}
