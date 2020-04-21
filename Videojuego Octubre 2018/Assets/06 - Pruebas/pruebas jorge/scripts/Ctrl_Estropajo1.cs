using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Estropajo1 : MonoBehaviour
{
    public bool contactoEstropajo;
    //public GameObject Estropajo;
    public GameObject grupoAnimEstropajo;

    Animator animatorEstropajo;
    public bool i = true;


    // Use this for initialization
    void Start () {
        animatorEstropajo = grupoAnimEstropajo.GetComponent<Animator>();
    }

    private void OnCollisionEnter()
    {
        //contactoEstropajo = true;
        animatorEstropajo.SetBool("primeraColision", true);
        if (i)
        {
            GameObject.Find("SonidoSplashEstropajo").GetComponent<AudioSource>().Play();
            i = false;
        }
            
    }
    




}
