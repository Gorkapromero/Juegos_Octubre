using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento_personaje : MonoBehaviour
{

    protected Joystick joystick;

	// Use this for initialization
	void Start ()
    {
        joystick = FindObjectOfType<Joystick>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = new Vector3(joystick.Horizontal * 100f,
                                         rigidbody.velocity.y,
                                         0);
	}
}
