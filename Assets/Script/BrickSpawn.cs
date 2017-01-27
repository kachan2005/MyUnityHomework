using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BrickSpawn : MonoBehaviour {

    [SerializeField]
    private GameObject brickToSpawn;
    Vector3 center;

    // Use this for initialization
    void Start () {
        center = transform.position;

        GameObject tower = new GameObject();
        
        GameObject circle = createCircle();

        circle.transform.parent = tower.transform;
        GameObject circleClone;

        float floorAngel = 27.0f;
        float orbitAngle = 0.0f;

        Vector3 newRotation;
        Quaternion spwanRotation;
        Vector3 spwanLocation = circle.transform.position;


        for (int i = 0; i < 15; i++)
        {
            spwanLocation.y += 1.0f;
            orbitAngle += floorAngel;

            newRotation = new Vector3(0.0f, orbitAngle, 0.0f);
            spwanRotation = Quaternion.Euler(newRotation);

            circleClone = GameObject.Instantiate(circle, spwanLocation, spwanRotation);
            circleClone.name = "Circle " + i;
            circleClone.transform.parent = tower.transform;

        }
        

        tower.transform.parent = transform;
        Vector3 towerScale = tower.transform.localScale;
        //towerScale.x = 3.0f;
        //towerScale.z = 3.0f;
        tower.transform.localScale = towerScale * 3.0f;

        tower.name = "Tower";
        string prefabPath = "Assets/GameObjects/Tower.prefab";
        PrefabUtility.CreatePrefab(prefabPath, tower);

    }

    // Update is called once per frame
    void Update () {
		
	}

    GameObject createCircle()
    {
        float radius = 6.0f;
        float orbitAngle = 0.0f;

        GameObject circle = new GameObject();
        circle.transform.parent = transform;

        //1. Create 1st brick in 0 degree and x = 6 position
        GameObject spawnedBrick;

        //Set up for location
        Vector3 spwanLocation = center;
        spwanLocation.x = center.x + radius * Mathf.Cos(orbitAngle / 180 * Mathf.PI);
        spwanLocation.y = 0.51f;
        spwanLocation.z = center.z + radius * Mathf.Sin(orbitAngle / 180 * Mathf.PI);

        //Set up for rotation
        Vector3 newRotation = new Vector3(0.0f, orbitAngle, 0.0f);
        Quaternion spwanRotation = Quaternion.Euler(newRotation);

        Debug.Log(string.Format("Frist Brick location y {0}: orbitAngle {1}!", spwanLocation.y, orbitAngle));
        spawnedBrick = GameObject.Instantiate(brickToSpawn, spwanLocation, spwanRotation) as GameObject;
        spawnedBrick.transform.parent = circle.transform;


        //2. set up for new  2nd brick try
        Vector3 scale = spawnedBrick.transform.localScale;
        Collider[] hitColliders;

        for (int j = 0; j < 1; j++)
        {
            //spwanLocation.y = j * scale.y + 1f;
            Debug.LogFormat("Begin Circle {0} on  height {1}", j, center.y);
            for (int i = 0; i < 40; i++)
            {
                int count = 0;
                bool doneCircle = false;
                do
                {
                    Debug.LogFormat("Trial {0}: orbitAngle {1}!", count, orbitAngle);
                    if (count > 200)
                    {
                        doneCircle = true;
                        break;
                    }

                    spwanLocation.x = center.x + radius * Mathf.Cos(orbitAngle / 180 * Mathf.PI);
                    spwanLocation.z = center.z - radius * Mathf.Sin(orbitAngle / 180 * Mathf.PI);

                    newRotation = new Vector3(0.0f, orbitAngle, 0.0f);
                    spwanRotation = Quaternion.Euler(newRotation);
                    orbitAngle += 0.1f;

                    hitColliders = Physics.OverlapBox(spwanLocation, scale / 2, spwanRotation);
                    count++;
                } while (hitColliders.Length > 0);

                if (doneCircle)
                {
                    Debug.LogFormat("Done Circle {0}", j);
                    break;
                }
                else
                {
                    spawnedBrick = GameObject.Instantiate(brickToSpawn, spwanLocation, spwanRotation) as GameObject;
                    spawnedBrick.transform.parent = circle.transform;
                }

            }
            Debug.LogFormat("Done Circle {0}", j);
            orbitAngle = (orbitAngle + 5.0f) % 360;
        }
        scale.x = 0.6f;
        for (int i = 0; i < circle.transform.childCount; i++)
        {
            circle.transform.GetChild(i).localScale = scale;
        }

        circle.name = "Circle";
        string prefabPath = "Assets/GameObjects/Circle.prefab";
        PrefabUtility.CreatePrefab(prefabPath, circle);

        return circle;
    }

}
