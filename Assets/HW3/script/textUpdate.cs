using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        string currentText = gameObject.GetComponent<GUIText>().text;
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
                gameObject.GetComponent<GUIText>().text = "";
                lastText = "";
            }
        }

		
	}
}
