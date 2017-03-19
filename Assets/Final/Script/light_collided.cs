using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_collided : MonoBehaviour {

    public float animation_time = 3.0f;
    float time = 0.0f;
    public bool isCollided = false;
    Color originalColor;


	// Use this for initialization
	void Start () {
        originalColor = GetComponent<Renderer>().material.GetColor("_TintColor");
        Debug.LogFormat("{0}: color = ({1}, {2}, {3})", gameObject.name, originalColor.r, originalColor.g, originalColor.b);
	}
	
	// Update is called once per frame
	void Update () {
        if (isCollided)
        {
            time += Time.deltaTime;
            Color c;
            if( time < animation_time)
            {
                c = new Color(
                        Random.Range(0, 255) / 255.0f,
                        Random.Range(0, 255) / 255.0f,
                        Random.Range(0, 255) / 255.0f
                    );

                Debug.LogFormat("{0}: color = ({1}, {2}, {3})", gameObject.name, c.r, c.g, c.b);
            }
            else
            {
                isCollided = false;
                c = originalColor;
                time = 0;
            }

            GetComponent<Light>().color = c;
            GetComponent<Renderer>().material.SetColor("_TintColor", c);
        }
		
	}

    
}
