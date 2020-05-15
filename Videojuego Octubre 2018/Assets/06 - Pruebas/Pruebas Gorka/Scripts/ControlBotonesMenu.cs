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

    public GameObject gOBJ_LevelLoader;

    public GameObject Canvas;
    public GameObject FondoOpening;


	public Sprite[] Rangos;
	public GameObject Rango;
	DatosGuardados DatosGuardados;
    // Use this for initialization
    void Start () 
	{
        //Ranking = gameObject.GetComponent<RankingManager>();
		DatosGuardados = GameObject.Find("Datosguardados").GetComponent<DatosGuardados>();
        BestScore();
		VerMonedas();
		ActualizarRango();

	}


    public void CargarescenaPlay(string juego)
    {
        //Ejecutamos la animacion de "Despejar Pantalla" y la animacion de Play del Opening
        Canvas.GetComponent<Animator>().Play("Anim_DespejarCanvas");
        FondoOpening.GetComponent<Animator>().Play("AnimOpening_Play");

        Invoke("ArrancarJuego", 2.0f);
    }
    public void ArrancarJuego()
    {
        // SceneManager.LoadScene(1);
        gOBJ_LevelLoader.GetComponent<Loader>().LoadLevel(2);

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
		if(Application.internetReachability == NetworkReachability.NotReachable)
		{
			gameObject.GetComponent<ToastMessage>().crearTexto("Error. Check internet connection!");
			return;
		}
		cuadroAnuncio.SetActive(true);
	}
	public void NoVerAnuncio()
	{
		cuadroAnuncio.SetActive(false);
	}

	void ActualizarRango()
	{
		Rango.GetComponent<Image>().sprite = Rangos[DatosGuardados.rango];
	}
}
