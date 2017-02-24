using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parseXYZFile : MonoBehaviour {

    public string fileName;
    public GameObject checkpoint;

    private const float unitInch = 0.0254f;

    // Use this for initialization
    void Start() {
        //parseFile();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void parseFile()
    {

        string[] lines = System.IO.File.ReadAllLines(fileName);


        transform.localScale = new Vector3(unitInch, unitInch, unitInch);

        for (int i = 0; i < lines.Length; i++)
        {
            string[] tokens = lines[i].Split(new char[] { ' ', '\t' });
            float x, y, z;
            float.TryParse(tokens[0], out x);
            float.TryParse(tokens[1], out y);
            float.TryParse(tokens[2], out z);
            //Debug.LogFormat("line {0}: {1} : {2}, {3}, {4}", i, lines[i], x, y, z);//tokens[0], tokens[1], tokens[2]);
            x *= unitInch;
            y *= unitInch;
            z *= unitInch;
            float size = unitInch * 12 * 30;

            Vector3 position = new Vector3(x, y, z);
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            Vector3 scale = new Vector3(size, size, size);

            GameObject newCheckpoint = GameObject.Instantiate(checkpoint, position, rotation) as GameObject;
            newCheckpoint.transform.localScale = scale;
            newCheckpoint.transform.parent = transform;
            newCheckpoint.name = "Checkpoint" + i;

        }
    }
}
