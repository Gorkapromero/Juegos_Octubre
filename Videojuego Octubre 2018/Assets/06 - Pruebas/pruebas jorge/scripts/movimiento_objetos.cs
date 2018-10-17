using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento_objetos : MonoBehaviour
{

    public float Velocidad = 500f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position -= transform.up * Velocidad * Time.deltaTime;
	}
}
