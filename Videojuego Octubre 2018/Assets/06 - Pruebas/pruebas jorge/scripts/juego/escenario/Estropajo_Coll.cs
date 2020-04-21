using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estropajo_Coll : MonoBehaviour
{

    Ctrl_Estropajo1 Script_Estropajo;
    Animator animatorEstropajo;

	// Use this for initialization
	void Start ()
    {
        Script_Estropajo = GameObject.Find("Estropajo_00").GetComponent<Ctrl_Estropajo1>();
        animatorEstropajo = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider trigger)
    {
    
        if(trigger.tag == "Jugador")
        {
            animatorEstropajo.SetBool("primeraColision", false);
            Script_Estropajo.i = true;


        }

    }

    public void reactivarSonidoEstropajo()
    {
        Script_Estropajo.i = true;

    }

}
