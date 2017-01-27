using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawn : MonoBehaviour {

    [SerializeField]
    private GameObject brickToSpawn;
    Vector3 center;

    // Use this for initialization
    void Start () {
        float radius = 6.0f;
        float orbitAngle = 0.0f;
        center = transform.position;

        //1. Create 1st brick in 0 degree and x = 5 position
        GameObject spawnedBrick;
        
        //Set up for location
        Vector3 spwanLocation = center;
        spwanLocation.x = center.x + radius * Mathf.Cos(orbitAngle / 180 * Mathf.PI);
        spwanLocation.z = center.z + radius* Mathf.Sin(orbitAngle / 180 * Mathf.PI);

        //Set up for rotation
        Vector3 newRotation = new Vector3(0.0f, orbitAngle, 0.0f);
        Quaternion spwanRotation = Quaternion.Euler(newRotation);

        //Debug.Log(string.Format("Frist Brick locatino {0}: orbitAngle {1}!", count, orbitAngle));
        spawnedBrick = GameObject.Instantiate(brickToSpawn, spwanLocation, spwanRotation) as GameObject;
        
        //2. set up for new  2nd brick try
        Vector3 scale = spawnedBrick.transform.localScale;
        Collider[] hitColliders;

        for(int j = 0; j <1; j++){
            spwanLocation.y = j * scale.y + 1f;
            Debug.LogFormat("Begin Circle {0} on  height {1}", j, center.y);
            for (int i = 0; i < 40; i++){
                int count = 0;
                bool doneCircle = false;
                do{
                    Debug.LogFormat("Trial {0}: orbitAngle {1}!", count, orbitAngle);
                    if(count > 50){
                        doneCircle = true;
                        break;
                    }

                    spwanLocation.x = center.x + radius * Mathf.Cos(orbitAngle / 180 * Mathf.PI);
                    spwanLocation.z = center.z - radius * Mathf.Sin(orbitAngle / 180 * Mathf.PI);

                    newRotation = new Vector3(0.0f, orbitAngle, 0.0f);
                    spwanRotation = Quaternion.Euler(newRotation);
                    orbitAngle += 1f;

                    hitColliders = Physics.OverlapBox(spwanLocation, scale / 2, spwanRotation);
                    count++;
                } while(hitColliders.Length > 0);

                if (doneCircle) {
                    Debug.LogFormat("Done Circle {0}", j);
                    break;
                }
                else {
                    spawnedBrick = GameObject.Instantiate(brickToSpawn, spwanLocation, spwanRotation) as GameObject;
                }

            }
            Debug.LogFormat("Done Circle {0}", j);
            orbitAngle = (orbitAngle + 5.0f) % 360;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
