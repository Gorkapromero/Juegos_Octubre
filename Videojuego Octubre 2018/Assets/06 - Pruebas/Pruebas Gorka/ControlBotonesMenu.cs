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
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	public void CargarescenaPlay(string juego)
    {
		SceneManager.LoadScene("juego"); 
    }
	public void CargarescenaRanking(string Ranking)
	{
		SceneManager.LoadScene("Ranking"); 
	}
	public void CargarescenaAjustes(string Ajustes)
	{
		SceneManager.LoadScene("Ajustes"); 
	}
	public void CargarescenaCreditos(string Creditos)
	{
		SceneManager.LoadScene("Creditos"); 
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
