using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyOutlined : MonoBehaviour {

    public Material theMaterial;
    int count = 0;
    bool isSelected = false;

    const float CHAIR_HEIGHT = 3.75f;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            isSelected = !isSelected;
            GameObject camera = GameObject.Find("Main Camera");
            HighlightsFX hl = camera.GetComponent<HighlightsFX>();
            Debug.LogFormat("This game object is: {0}", gameObject.tag);
            if (isSelected)
            {
                hl.changeObject(gameObject);
            }
            else
            {
                hl.changeObject(null);
            }
        }

        StayGround();
        
	}

    void StayGround()
    {
        Vector3 position = transform.position;
        Vector3 rotation = transform.rotation.eulerAngles;
        switch (gameObject.tag)
        {
            case "chair":
                position.y = CHAIR_HEIGHT;
                transform.position = position;

                rotation.x = -90;
                transform.rotation = Quaternion.Euler(rotation);
                break;
            case "desk":
                position.y = CHAIR_HEIGHT;
                transform.position = position;

                rotation.x = -90;
                transform.rotation = Quaternion.Euler(rotation);
                break;
        }
    }
    
}
