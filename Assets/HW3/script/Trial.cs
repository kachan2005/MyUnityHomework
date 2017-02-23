using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trial : MonoBehaviour {

    public GameObject Arrow;
    public float spawnTime;
    private float currentTime = 0;

    private int lastCheckpoint_index = 0;
    private GameObject aircraft;
    private bool isShow = true;

	// Use this for initialization
	void Start () {
        aircraft = GameObject.Find("Aircraft");
		
	}
	
	// Update is called once per frame
	void Update () {
        int currentCheckpint_index = aircraft.GetComponent<flying>().checkpoint_Index;
        if (currentCheckpint_index != lastCheckpoint_index) DestroyChildren();
        

        if(currentTime > spawnTime)
        {
            currentTime = 0;
            Vector3 nextPosition = aircraft.GetComponent<flying>().getCheckPoint(currentCheckpint_index).transform.position;
            Vector3 position = aircraft.GetComponent<flying>().getCheckPoint(currentCheckpint_index - 1).transform.position;
            Vector3 direction = Vector3.Normalize(nextPosition - position);

            position = position + direction * 5;
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            Vector3 scale = new Vector3(20, 20, 20);

            GameObject newArrow = GameObject.Instantiate(Arrow, position, rotation) as GameObject;
            newArrow.transform.forward = direction;
            newArrow.transform.parent = transform;
            //newArrow.transform.localScale = scale;
            newArrow.name = "Arrow";
        }


        currentTime += Time.deltaTime;
        lastCheckpoint_index = currentCheckpint_index;

        showTrial(gameObject);
	}


    void DestroyChildren()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void showTrial(bool isShow)
    {
        this.isShow = isShow;
    }

    private void showTrial(GameObject g)
    {
        if( g.GetComponent<Renderer>() != null)
        {
            g.GetComponent<Renderer>().enabled = isShow;
        }

        for (int i = 0; i < g.transform.childCount; i++)
            showTrial(g.transform.GetChild(i).gameObject);
    }
}
