﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ctrl_Botones : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void volverJugar()
    {
        SceneManager.LoadScene("juego");
    }

    public void salir()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GuardarPuntuacion()
    {

    }

}
