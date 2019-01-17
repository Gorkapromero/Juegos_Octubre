using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prueba1 : MonoBehaviour {
    float y;
	// Use this for initialization
	void Start ()
    {
        //y = gameObject.GetComponent<BoxCollider>().center.y;

    }
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<BoxCollider>().size = transform.localScale;
        y = (gameObject.GetComponent<BoxCollider>().size.y-1)/2;
        gameObject.GetComponent<BoxCollider>().center = new Vector3(0, y, 0);
    }
}
