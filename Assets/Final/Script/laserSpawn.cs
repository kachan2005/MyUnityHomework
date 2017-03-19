using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class laserSpawn : MonoBehaviour {

    public GameObject crosshair;
    public GameObject menuCanvas;

    private LineRenderer l;

    public bool debug = false;


    public Material glass_material;
    
    private float time = 0;

    // Use this for initialization
    void Start () {
        l = GetComponent<LineRenderer>();
        l.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))  //used for menu selection or interaction
        {
            l.enabled = true;
            Vector3 coord = menuCanvas.transform.position;
            Vector3 a = transform.position;
            Vector3 b = a + transform.forward * 100;
            Vector3 ray = b - a;

            Vector3 normal = -menuCanvas.transform.forward;

            float d = Vector3.Dot(normal, coord);

            float denom = Vector3.Dot(normal, ray);
            if (denom != 0)
            {
                Vector3 diff = coord - a;
                float t = Vector3.Dot(diff, normal) / denom;

                Vector3 p = a + ray * t;
                if(debug)
                    DrawLine(a, b, Color.cyan);

                else
                    DrawLine(a, p, Color.cyan);

                crosshair.transform.position = p;
                Vector3 lc = crosshair.transform.localPosition;
                lc.z = 0;
                crosshair.transform.localPosition = lc;
            }
        }
        else
        {
            l.enabled = false;
        }
        
    }

    void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        l.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        l.startColor = color;
        l.endColor = color;
        float size = 0.01f;
        l.startWidth = size;
        l.endWidth = size;
        l.SetPosition(0, start);
        l.SetPosition(1, end);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "selectable")
        {
            GameObject d = other.gameObject;
            d.GetComponent<Image>().color = Color.red;
            d.GetComponent<Image>().material = null;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "selectable")
        {
            GameObject d = collision.gameObject;
            d.GetComponent<Image>().color = Color.white;
            d.GetComponent<Image>().material = glass_material;
        }

    }
}
