using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionColor : MonoBehaviour {

    MeshRenderer thisRenderer;

    // Use this for initialization
    void Start () {
        thisRenderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    Color GetRandomColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    void OnCollisionEnter(Collision other)
    {
        Color randomColor = GetRandomColor();

        Material newMaterial = new Material(thisRenderer.material);
        newMaterial.SetColor("_Color", randomColor);
        thisRenderer.material = newMaterial;

        Collider otherCollider = other.collider;
        MyFirstScript otherScript = otherCollider.GetComponent<MyFirstScript>();

        if (otherScript != null)
        {
            otherScript.enabled = false;
        }

    }
}
