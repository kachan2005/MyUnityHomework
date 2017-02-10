using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MenuSignals : MonoBehaviour {

    public int mode;

    public const int CONST_MODE_ADD = 0;
    public const int CONST_MODE_MOVE = 1;
    public const int CONST_MODE_ROTATE = 2;
    public const int CONST_MODE_SELECT_RAY = 3;
    public const int CONST_MODE_SELECT_BALL = 4;
    public const int CONST_MODE_TELEPORT = 5;

    public int MODE_ADD = CONST_MODE_ADD;
    public int MODE_MOVE = CONST_MODE_MOVE;
    public int MODE_ROTATE = CONST_MODE_ROTATE;
    public int MODE_SELECT_RAY = CONST_MODE_SELECT_RAY;
    public int MODE_SELECT_BALL = CONST_MODE_SELECT_BALL;
    public int MODE_TELEPORT = CONST_MODE_TELEPORT;

    // Use this for initialization
    void Start () {

        mode = 0; //default ADD
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeMode()
    {
        mode = (mode + 1) % 6;
        TextMesh t = GameObject.Find("Mode").GetComponent<TextMesh>();
        switch (mode)
        {
            case CONST_MODE_ADD:
                t.text = "Add";
                break;
            case CONST_MODE_MOVE:
                t.text = "Move";
                break;
            case CONST_MODE_ROTATE:
                t.text = "Rotate";
                break;
            case CONST_MODE_SELECT_RAY:
                t.text = "Select Ray";
                break;
            case CONST_MODE_SELECT_BALL:
                t.text = "Select Ball";
                break;
            case CONST_MODE_TELEPORT:
                t.text = "Teleport";
                break;
        }
    }

    public void CreateObject(Vector3 position)
    {
        gameObject.GetComponent<SpawnObjects>().CreateOjbect(position);
    }
}
