using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Estropajo : MonoBehaviour
{
    public float Velocidad;
    public GameObject Estropajo;

    bool ContactoEstropajo;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if(ContactoEstropajo)
        {
            Estropajo.transform.position -= Estropajo.transform.up * Velocidad * Time.deltaTime;
        }
    }

    private void OnCollisionEnter()
    {
        print("other");
        //if(other.gameObject.tag=="Jugador")
        //{
        print("colision estropajo");
        ContactoEstropajo = true;
        //}
    }
}
