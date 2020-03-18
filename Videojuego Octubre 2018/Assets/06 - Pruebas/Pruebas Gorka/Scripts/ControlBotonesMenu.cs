using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlBotonesMenu : MonoBehaviour 
{
    //RankingManager Ranking;
    public Text TextoScore;
	public Text TextoMonedas;
	int Bestscore;

	public GameObject cuadroAnuncio;

	DatosGuardados DatosGuardados;
    // Use this for initialization
    void Start () 
	{
        //Ranking = gameObject.GetComponent<RankingManager>();
		DatosGuardados = GameObject.Find("Datosguardados").GetComponent<DatosGuardados>();
        BestScore();
		VerMonedas();
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
    void BestScore()
    {
        Bestscore = DatosGuardados.puntuacion;
        //print(Bestscore);
        TextoScore.text = Bestscore.ToString();
    }

	public void VerMonedas()
	{
		TextoMonedas.text = DatosGuardados.Monedas.ToString();
	}

	public void Recompensa()
	{
		cuadroAnuncio.SetActive(true);
	}
	public void NoVerAnuncio()
	{
		cuadroAnuncio.SetActive(false);
	}
}
