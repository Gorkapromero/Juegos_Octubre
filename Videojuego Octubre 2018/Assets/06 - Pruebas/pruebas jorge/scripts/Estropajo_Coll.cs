using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estropajo_Coll : MonoBehaviour
{
    Ctrl_Estropajo1 Ctrl_Estropajo;
	// Use this for initialization
	void Start ()
    {
        Ctrl_Estropajo = GameObject.Find("Elementos_Escenario").GetComponent<Ctrl_Estropajo1>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter()
    {
        print("colision estropajo");
       
        Ctrl_Estropajo.contactoEstropajo = true;
        //}
    }

}
