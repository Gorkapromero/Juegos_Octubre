﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ctrl_Botones : MonoBehaviour
{
    //public InputField Nombre;
    //public RankingManager Ranking;
    Ctrl_Puntuacion Puntuacion;

    public static bool GamePaused = false;

    public GameObject pauseMenuUI;

    public AudioSource Musica;

    DatosGuardados DatosGuardar;
    movimiento_personaje personaje;


    AudioSource sonidoMuerte;
    AudioSource sonidoMuerte_02;
    AudioSource musicaDeFondo;
    Animator animatorProta;

    Timer timer;
    Ctrl_CAtras CuentaAtras;
	// Use this for initialization
	void Start ()
    {
        DatosGuardar=GameObject.Find("Datosguardados").GetComponent<DatosGuardados>();
        personaje = GameObject.FindWithTag("Jugador").GetComponent<movimiento_personaje>();
        Puntuacion = GameObject.Find("C_Puntuacion").GetComponent<Ctrl_Puntuacion>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        CuentaAtras = GameObject.Find("cunta_atras").GetComponent<Ctrl_CAtras>();
    }
	


    public void volverJugar()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        DatosGuardar.Save();

        SceneManager.LoadScene(1);
    }

    public void pantallaAjustes()
    {
        SceneManager.LoadScene(2);
    }

    public void pantallaRanking()
    {
        SceneManager.LoadScene(3);
    }

    public void CargarMenu()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        DatosGuardar.Save();
        
        SceneManager.LoadScene(0);
    }

    public void pausa()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void QuitarMusica()
    {
        Musica.mute = !Musica.mute;
    }

    public void QuitarEfectos()
    {
        GameObject[] Efectos = GameObject.FindGameObjectsWithTag("SoundEffects");
        for(int i = 0; i<Efectos.Length; i++)
        {
            Efectos[i].GetComponent<AudioSource>().mute = !Efectos[i].GetComponent<AudioSource>().mute;
        }
    }

    public void GastarMonedas(int monedas)
    {
        DatosGuardar.Monedas -= monedas;
        VolverAJugar();
    }

    public void VerAnuncio()
    {

    }

    void VolverAJugar()
    {
        //resetear valores
        //Reproducimos el sonido de "MUERTE"
                sonidoMuerte.Stop();
                sonidoMuerte_02.Stop();

                //Y bajamos el sonido de la musica de fondo
                musicaDeFondo.volume = 0.4f;

                //animatorProta.Play("Muerte",-1,0);
                personaje.velocidad_fin = personaje.velocidad;
                //GetComponent<Animator>().SetFloat("Speed", 0.0f);
                personaje.desbloquearControles();
                personaje.panelBloqueoControles.SetActive(true);

                CuentaAtras.iniciarCuenta();
                timer.ReanuadarTiempo();

                //menu fin de partida
                personaje.Finpartida.SetActive(false);
        //desactivar fin de partida
        //timer 3 segundos
    }
}
