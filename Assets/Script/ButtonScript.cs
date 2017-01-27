using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    Renderer rend;

    // Use this for initialization
    void Start ()
    { 
        rend = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
	}
    

    public void changeMode(int mode)
    {
        Color newColor = new Color(0.0f, 0.0f, 0.0f);
        newColor.r = mode == 0 ? 1.0f : 0.0f;
        newColor.b = mode == 1 ? 1.0f : 0.0f;
        newColor.g = mode == 2 ? 1.0f : 0.0f;

        rend.material.SetColor("_Color", newColor);

        GameObject child = transform.GetChild(0).gameObject;

        TextMesh textMesh = child.GetComponent<TextMesh>();
        if (textMesh != null)
        {
            if (mode == 0)
            {
                textMesh.text = "Laser\nMode";
            }
            else if (mode == 1)
            {
                textMesh.text = "Cannon\nMode";
            }
            else if (mode == 2)
            {
                textMesh.text = "None\nMode";
            }
        }
    }
    
}
