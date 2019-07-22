﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Fuego : MonoBehaviour {

    float TiempoFuego = 2.0f;
    public float TiempoActivacion;
    public float TiempoMinimo;
    public float TiempoMax;
    bool FuegoActivado;

    Animator Animaciones;
    // Use this for initialization
    movimiento_personaje Personaje;
    public GameObject ParticulasFuego;
	void Start ()
    {
        Animaciones = GameObject.Find("Elementos_Escenario").GetComponent<Animator>();
        Personaje = GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(TiempoActivacion>0)
        {
            TiempoActivacion -= Time.deltaTime;
        }
        else if(!FuegoActivado&&TiempoActivacion<=0)
        {
            ActivarFuego();
            FuegoActivado = true;
        }
		if(TiempoFuego <= 0)
        {
            TiempoFuego = 2.0f;
            //Animaciones.Play("Anim_FuegoOff");
            TiempoActivacion = Random.Range(TiempoMinimo,TiempoMax);
            FuegoActivado = false;
            Personaje.DentroFuego = false;
            Destroy(GameObject.FindGameObjectWithTag("Fuego"));
        }
        else if(FuegoActivado)
        {
            TiempoFuego -= Time.deltaTime;
        }

	}

    void ActivarFuego()
    {
        //Animaciones.Play("Anim_FuegoOn");
        GameObject fuego = Instantiate(ParticulasFuego);
    }

    private void OnTriggerEnter(Collider coli)
    {
        switch (coli.gameObject.tag)
        {
            case "Enemigo":
                //destruimos enemigo
                break;
            case "Jugador":
                //restamos vida
                if (!Personaje.DentroFuego)
                {
                    Personaje.DentroFuego = true;
                    Personaje.quitarvida_Vida();
                    
                }
                //añadimos particulas de fuego
                break;
        }
    }

    private void OnTriggerExit(Collider coli)
    {
        switch (coli.gameObject.tag)
        {
            case "Jugador":
                //restamos vida
                Personaje.DentroFuego = false;
                //añadimos particulas de fuego
                break;
        }
    }
}