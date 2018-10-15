using System.Collections;
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
	public void CargarescenaPlay(string Nivel1)
    {
		SceneManager.LoadScene("Nivel 1"); 
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

}
