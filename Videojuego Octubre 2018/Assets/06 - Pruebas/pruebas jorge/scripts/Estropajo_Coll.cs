using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estropajo_Coll : MonoBehaviour
{
//    Ctrl_Estropajo1 Ctrl_Estropajo;
    Animator animatorEstropajo;
	// Use this for initialization
	void Start ()
    {
        animatorEstropajo = gameObject.GetComponent<Animator>();


    }



    private void OnTriggerEnter(Collider trigger)
    {

        if(trigger.tag == "Jugador")
        {
            animatorEstropajo.SetBool("primeraColision", false);

        }

    }

}
