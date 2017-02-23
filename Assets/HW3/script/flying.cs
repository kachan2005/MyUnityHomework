using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flying : MonoBehaviour {

    public float speed;

    public int checkpoint_Index;
    private int checkpoint_Size;
    public bool showChecked = true;

    private float parseTime;
    private float playTime;
    private bool rechedTarget = false;

    private int visualMode = 1;


	// Use this for initialization
	void Start () {

        getCheckPoint(0).GetComponent<Check_point>().setChecked();

        checkpoint_Index = 1;
        checkpoint_Size = GameObject.Find("CheckPoints").transform.childCount;
        getCheckPoint(checkpoint_Index).GetComponent<Check_point>().setTarget();
        resetAircraft();
        playTime = 0.0f;
        ChangeMode();
    }
	
	// Update is called once per frame
	void Update () {
        
        parseTime -= Time.deltaTime;

        
        if( parseTime <= 0)
        {
            if(checkpoint_Index < checkpoint_Size) playTime += Time.deltaTime;

            //update position upon speed
            gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;

            //update rotation upon input
            //rotate by x-axis
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Vector3 rotation = transform.rotation.eulerAngles;
                rotation.x -= 1;
                transform.rotation = Quaternion.Euler(rotation);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Vector3 rotation = transform.rotation.eulerAngles;
                rotation.x += 1;
                transform.rotation = Quaternion.Euler(rotation);
            }

            //rotate by y-axis
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Vector3 rotation = transform.rotation.eulerAngles;
                rotation.y -= 1;
                transform.rotation = Quaternion.Euler(rotation);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Vector3 rotation = transform.rotation.eulerAngles;
                rotation.y += 1;
                transform.rotation = Quaternion.Euler(rotation);
            }

           
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            showChecked = !showChecked;
            UpdateCheckpoints();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeMode();
        }
        updateInfo();
    }

    bool collideSelf(GameObject selfObject, GameObject target)
    {
        if (selfObject == target)
            return true;
        else
        {
            for( int i = 0; i < selfObject.transform.childCount; i++)
            {
                bool result = collideSelf(selfObject.transform.GetChild(i).gameObject, target);
                if (result)
                    return result;
            }
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collideSelf(gameObject, collision.gameObject))
            return;
        Debug.LogFormat("{0}: Aircraft collide with {1}", Time.time, collision.gameObject.name);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3();
        resetAircraft();
        
    }
    

    private void resetAircraft()
    {
        parseTime = 3.0f;
        transform.position = getCheckPoint(checkpoint_Index - 1).transform.position;
        Vector3 target = getCheckPoint(checkpoint_Index).transform.position;
        transform.forward = target - transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        
        //Debug.LogFormat("{0}: Aircraft trigger with {1}", Time.time, other.gameObject.name);
        if(other.gameObject.name == "Checkpoint" + checkpoint_Index)
        {

            //Debug.LogFormat("I hit target", Time.time, other.gameObject.name);
            other.gameObject.GetComponent<Check_point>().setChecked();
            if(++checkpoint_Index < checkpoint_Size)
            {
                getCheckPoint(checkpoint_Index).GetComponent<Check_point>().setTarget();
            }
            rechedTarget = true;
        }
    }


    public void ChangeMode()
    {
        visualMode = (visualMode + 1) % 3;
        GameObject arrow = GameObject.Find("Arrow1");
        for (int i = 0; i < arrow.transform.childCount; i++)
            arrow.transform.GetChild(i).GetComponent<Renderer>().enabled = visualMode != 1;
        arrow = GameObject.Find("Arrow2");
        for (int i = 0; i < arrow.transform.childCount; i++)
            arrow.transform.GetChild(i).GetComponent<Renderer>().enabled = visualMode != 1;

        GameObject trial = GameObject.Find("Trials");
        trial.GetComponent<Trial>().showTrial(visualMode != 0);

    }

    public GameObject getCheckPoint(int index)
    {
        return GameObject.Find("CheckPoints").transform.GetChild(index).gameObject;
    }

    void updateInfo()
    {
        Vector3 direction = transform.forward;
        float distance = 1.0f;
        if (checkpoint_Index < checkpoint_Size)
        {
            Transform checkpoint = getCheckPoint(checkpoint_Index).transform;
            direction = checkpoint.position - transform.position;
            distance = direction.magnitude / 20.0f;
            distance = Mathf.Sqrt(distance);
        }

        //update count down text if it is paused
        string text = "";
        if (parseTime > 2.5f) GameObject.Find("CountDown").GetComponent<GUIText>().text = "3";
        else if (parseTime > 1.5f) GameObject.Find("CountDown").GetComponent<GUIText>().text = "2";
        else if (parseTime > 0.5f) GameObject.Find("CountDown").GetComponent<GUIText>().text = "1";
        else if (parseTime > -0.5f) GameObject.Find("CountDown").GetComponent<GUIText>().text = "Go!";
        else if (rechedTarget)
        {
            if(checkpoint_Index == checkpoint_Size) GameObject.Find("Notification").GetComponent<GUIText>().text = "Game End!";
            else GameObject.Find("Notification").GetComponent<GUIText>().text = "Recach Checkpoint " + (checkpoint_Index - 1);
            rechedTarget = false;
        }

        //update play info
        text = "Time: " + playTime + "s\n";
        text += "Checkpoint: " + checkpoint_Index + "\n";
        text += "Distance: " + distance + "m\n";
        GameObject.Find("Time").GetComponent<GUIText>().text = text;

        UpdateArrow(direction, distance);
    }


    void UpdateArrow(Vector3 direction, float distance)
    {
        GameObject arrow = GameObject.Find("Arrow1");
        arrow.transform.forward = direction;
        arrow.transform.RotateAround(arrow.transform.position, arrow.transform.forward, 400.0f * playTime);

        arrow.transform.localScale = new Vector3(1.0f, 1.0f, distance);

        arrow = GameObject.Find("Arrow2");
        arrow.transform.forward = direction;
        arrow.transform.localScale = new Vector3(1.0f, 1.0f, distance);
        arrow.transform.RotateAround(arrow.transform.position, arrow.transform.forward, 400.0f * playTime);
    }

    void UpdateCheckpoints()
    {
        for(int i = 0; i < checkpoint_Size; i++)
        {
            if (getCheckPoint(i).GetComponent<Check_point>().isChecked)
            {
                getCheckPoint(i).GetComponent<Renderer>().enabled = showChecked;
            }
        }
    }
}
