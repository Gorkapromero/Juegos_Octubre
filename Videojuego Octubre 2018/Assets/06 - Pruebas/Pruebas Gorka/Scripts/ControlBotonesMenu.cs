﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ControlBotonesMenu : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		
	}


    public void CargarescenaPlay(string juego)
    {
		SceneManager.LoadScene(1); 
    }
	public void CargarescenaRanking(string Ranking)
	{
		SceneManager.LoadScene(3); 
	}
	public void CargarescenaAjustes(string Ajustes)
	{
		SceneManager.LoadScene(2); 
	}
	public void CargarescenaCreditos(string Creditos)
	{
		SceneManager.LoadScene(4); 
	}
	public void cargarInstagram ()
	{
		Application.OpenURL ("https://www.instagram.com/studios_ikki/?hl=es");
	}
	public void cargarFacebook()
	{
		Application.OpenURL ("https://www.facebook.com/ikkiStudios/");
	}
	public void cargarTwitter ()
	{
		Application.OpenURL ("https://twitter.com/Studiosikki?lang=en");
	}
	public void cargarWeb ()
	{
		Application.OpenURL ("http://studiosikki.com/");
	}
}
