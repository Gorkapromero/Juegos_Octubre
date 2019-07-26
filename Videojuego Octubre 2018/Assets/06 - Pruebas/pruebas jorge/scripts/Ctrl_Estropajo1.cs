using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Estropajo1 : MonoBehaviour
{
    public bool contactoEstropajo;
    public GameObject Estropajo;
    public float Velocidad;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (contactoEstropajo)
        {
            Estropajo.transform.position -= Estropajo.transform.forward * Velocidad * Time.deltaTime;
        }
        else if (!contactoEstropajo&& Estropajo.transform.position.y >= -35f)
        {
            Estropajo.transform.position += Estropajo.transform.forward * Velocidad * Time.deltaTime;
        }
    }
}
