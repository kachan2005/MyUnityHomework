using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textUpdate : MonoBehaviour {

    public float emptyTime;
    private float time = 0.0f;
    private string lastText = "";
    private bool isEmpty = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        string currentText = gameObject.GetComponent<Text>().text;
        if (currentText != lastText) {
            isEmpty = false;
            time = emptyTime;
        }

        lastText = currentText;

        if (!isEmpty)
        {
            if(time > 0)time -= Time.deltaTime;
            else
            {
                isEmpty = true;
                gameObject.GetComponent<Text>().text = "";
                lastText = "";
            }
        }

		
	}
}
