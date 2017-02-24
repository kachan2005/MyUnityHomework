using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class LeapExample : MonoBehaviour {

    LeapProvider provider;

    // Use this for initialization
    void Start () {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }
	
	// Update is called once per frame
	void Update () {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            Debug.LogFormat(hand.ToString());
            Debug.LogFormat("Test: {0} ", hand.Finger(0).ToString());
            if (hand.IsLeft)
            {
                transform.position = hand.PalmPosition.ToVector3() +
                                     hand.PalmNormal.ToVector3() *
                                    (transform.localScale.y * .5f + .02f);
                transform.rotation = hand.Basis.rotation.ToQuaternion();
            }
        }
    }
}
