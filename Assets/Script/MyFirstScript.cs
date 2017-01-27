using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFirstScript : MonoBehaviour {


    //some basic value

    [SerializeField] int testNum;
    int myFirstInt;
    float myFirstFloat;
    string myFirstString;

    //GL value
    GameObject myFirstObject;
    Camera myMainCamera;
    SphereCollider mySphereCollider;
    MyFirstScript myNewScript;

    //color
    //public Color sphereColor;
    [SerializeField]
    private Color sphereColor;

    MeshRenderer thisRenderer;


    //Runs before start
    void Awake(){

    }

    // Use this for initialization
    void Start () {
        //SphereCollider thisCollider = GetComponent<SphereCollider>();
        thisRenderer = GetComponent<MeshRenderer>();
        //MyFirstScript thisScript = GetComponent<MyFirstScript>();

        Material newCubeMaterial = new Material(thisRenderer.material);
        newCubeMaterial.SetColor("_Color", sphereColor);
        thisRenderer.material = newCubeMaterial;
	}

    Color GetRandomColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    //Runs when the script is enabled
    void OnEnable(){

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("You pressed the left mouse button");
        }

        if (Input.GetKeyDown("a")) {
            Debug.Log("You pressed the 'a' key");
        }

        //Create a ray starting at this object and going forward
        Ray myRay = new Ray(transform.position, transform.forward);
        RaycastHit rayHit; //variable to store raycase output

        if(Physics.Raycast(myRay, out rayHit, Mathf.Infinity)) {
            Debug.LogFormat("You hit {0}!", rayHit.collider.name);
        }

        Material currentMaterial = thisRenderer.material;
        Material newMaterial = new Material(currentMaterial);
        newMaterial.SetColor("_Color", GetRandomColor());
        thisRenderer.material = newMaterial;


	}

    //Used for Physical calculations
    void FixedUpdate(){

    }
    
}
