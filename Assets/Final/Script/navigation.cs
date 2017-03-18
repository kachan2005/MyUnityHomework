using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navigation : MonoBehaviour {

    public GameObject point;
    public GameObject points;
    public GameObject checkpoints;

    private float time;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        if(time > 1.0f)
        {
            for(int i = 0; i < points.transform.childCount; i++)
            {
                Destroy(points.transform.GetChild(i).gameObject);
            }

            for(int i = 0; i < checkpoints.transform.childCount; i++)
            {
                Transform checkpoint = checkpoints.transform.GetChild(i);
                Vector3 diff = checkpoint.position - transform.position;
                float distance = diff.magnitude;

                Debug.LogFormat("{0}: distance = {1}, distance2 = {5}, diff = ({2},{3},{4}), diff.normalized = ({6},{7},{8})", 
                    checkpoint.name, distance, diff.x, diff.y, diff.z, (0.5f * distance / 500.0f), diff.normalized.x, diff.normalized.y, diff.normalized.z);

                if (distance > 500)
                    continue;

                GameObject spawnedPoint;
                Vector3 position = new Vector3();
                Quaternion rotation = Quaternion.Euler(Vector3.zero);

                spawnedPoint = GameObject.Instantiate(point, position, rotation) as GameObject;
                spawnedPoint.transform.parent = points.transform;
                spawnedPoint.transform.localPosition = diff.normalized * (0.5f * distance / 500.0f);
            }

            time = 0.0f;
        }
	}
}
