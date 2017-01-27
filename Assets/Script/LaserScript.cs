using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {

    //debug info
    float debugTime = 0.0f;

    //change laser color
    Renderer rend;

    //collide detection
    private GameObject currentCollideObject;
    bool sameCollideObject = false;

    //Time measurment
    float currentTime;
    private const float DWELLING_TIME = 2.0f;


    [SerializeField] private GameObject cannon;

    int mode;

    //laser variable
    CapsuleCollider capCollider;
    Vector3 defaultLocation;
    Quaternion defaultRotation;

    //Cannon variable
    Quaternion spawnRotation = Quaternion.Euler(Vector3.zero);
    bool leftCannon = true;

    // Use this for initialization
    void Start () {

        rend = GetComponent<Renderer>();
        capCollider = GetComponent<CapsuleCollider>();
        mode = 0;
        resetLaserColor();

        defaultLocation = transform.localPosition;
        defaultRotation = transform.localRotation;
    }
	
	// Update is called once per frame
	void Update () {
        debugTime += Time.deltaTime;

        if (currentCollideObject != null && currentCollideObject.name == "Brick(Clone)" && mode != 2)
        {
            currentTime += Time.deltaTime;
            updateLaserColor();
        }

        if (sameCollideObject && currentTime > DWELLING_TIME)
        {
            if(mode == 0)
            {
                capCollider.isTrigger = false;
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();

                rb.mass = 10.0f;
                rb.AddForce(transform.up * 60000.0f);
                Debug.LogFormat("Now should be shooting");
            }
            else if(mode == 1)
            {
                resetLaserColor();
                GameObject spawnedCannon;
                Vector3 spawnLocation = transform.position;
                spawnLocation += 1.5f * transform.right * (leftCannon ? -1 : 1);
                spawnedCannon = GameObject.Instantiate(cannon, spawnLocation, spawnRotation) as GameObject;
                CannonScript cs = spawnedCannon.GetComponent<CannonScript>();
                if(cs != null)
                {
                    cs.thrust = 20000.0f;
                    cs.direction = transform.up;
                }
                leftCannon = !leftCannon;
            }
        }
        else
        {
            updateCollideObject();
        }
    }

    private void updateCollideObject()
    {
        Ray ray = new Ray(transform.position, transform.up);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit, 100.0f))
        {
            //Debug.LogFormat("You hit {0} on the forward!", rayHit.collider.name, debugTime);
            GameObject thisCollideObject = rayHit.collider.gameObject;
            if( thisCollideObject != currentCollideObject)
            {
                if(thisCollideObject.name == "Button")
                {
                    ButtonScript bs = thisCollideObject.GetComponent<ButtonScript>();
                    if(bs != null)
                    {
                        this.changeMode();
                        bs.changeMode(mode);
                    }
                }
                resetLaserColor();
                currentCollideObject = thisCollideObject;
                sameCollideObject = false;
            }
            else
            {
                sameCollideObject = true;
            }
        }
        else
        {
            //Debug.LogFormat("Time: {0}: You does not hit anything forward!", debugTime);
            sameCollideObject = false;
            currentCollideObject = null;
            resetLaserColor();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Main Camera")
        {
            resetAfterShot();
        }
    }

    void resetAfterShot()
    {
        transform.localPosition = defaultLocation;
        transform.localRotation = defaultRotation;
        Destroy(GetComponent<Rigidbody>());
        capCollider.isTrigger = true;
        resetLaserColor();
    }


    void updateLaserColor()
    {
        Color newColor = rend.material.GetColor("_TintColor");
        //time += Time.deltaTime;
        newColor.b += (1.0f / DWELLING_TIME) * Time.deltaTime;
        newColor.r -= (1.0f / DWELLING_TIME) * Time.deltaTime;

        //Debug.LogFormat("{3}: Color = ({0}, {1}, {2})", newColor.r, newColor.b, newColor.g, time);
        if (newColor.r >= 0.0f)
        {
            rend.material.SetColor("_TintColor", newColor);
        }

    }

    void resetLaserColor()
    {
        Color newColor = new Color(1.0f, 0.3f, 0.0f, 0.5f);
        rend.material.SetColor("_TintColor", newColor);
        currentTime = 0.0f;
    }

    public void changeMode()
    {
        mode = (mode + 1) % 3;
    }

    public int getMode()
    {
        return mode;
    }

}
