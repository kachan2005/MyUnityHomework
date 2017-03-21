using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMenu : MonoBehaviour {

    [SerializeField]
    private List<GameObject> objects;

    public bool displayed = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void toggleDisplay()
    {
        displayList(!displayed);
    }

    public void displayList(bool isDisplay)
    {
        displayed = isDisplay;
        transform.GetChild(0).GetComponent<Canvas>().enabled = isDisplay;

        for (int i = 0; i < objects.Count; i++)
        {
            renderObject(objects[i].transform, isDisplay);
        }

    }

    void renderObject(Transform t, bool isRender)
    {

        if (t.GetComponent<Renderer>() != null)
            t.GetComponent<Renderer>().enabled = isRender;


        for (int i = 0; i < t.childCount; i++)
        {
            renderObject(t.GetChild(i), isRender);
        }

    }
}
