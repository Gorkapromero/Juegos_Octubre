using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swim : MonoBehaviour {

    public float waterLevel = 4f;
    public float floatHeight = 2f;
    public float bounceDamp = 0.05f;
    public Vector3 buoyancyCentreOffset;

    float forceFactor;
    Vector3 actionPoint;
    Vector3 upLift;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
        forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);

        if(forceFactor > 0f)
        {
            upLift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
            GetComponent<Rigidbody>().AddForceAtPosition(upLift, actionPoint);
        }
	}
}
