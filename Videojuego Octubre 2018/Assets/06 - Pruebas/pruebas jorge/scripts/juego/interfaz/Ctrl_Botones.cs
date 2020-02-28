using System.Collections;
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
    bool FX;

    DatosGuardados DatosGuardar;
    movimiento_personaje personaje;


    AudioSource sonidoMuerte;
    AudioSource sonidoMuerte_02;
    AudioSource musicaDeFondo;
    Animator animatorProta;

    Timer timer;
    public Ctrl_CAtras CuentaAtras;

    GameObject[] Efectos;
    public GameObject BMusica;
    public GameObject BFX;

    public int MonedasResucitar;
	// Use this for initialization
	void Start ()
    {
        animatorProta = GameObject.FindWithTag("Jugador").GetComponent<Animator>();

        DatosGuardar =GameObject.Find("Datosguardados").GetComponent<DatosGuardados>();
        personaje = GameObject.FindWithTag("Jugador").GetComponent<movimiento_personaje>();
        Puntuacion = GameObject.Find("C_Puntuacion").GetComponent<Ctrl_Puntuacion>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        //CuentaAtras = GameObject.Find("cunta_atras").GetComponent<Ctrl_CAtras>();


        //Variables Sonido
        musicaDeFondo = GameObject.Find("MusicaFondo").GetComponent<AudioSource>();
        sonidoMuerte = GameObject.Find("SonidoMuerte").GetComponent<AudioSource>();
        sonidoMuerte_02 = GameObject.Find("SonidoMuerte_02").GetComponent<AudioSource>();

        Musica.mute = !DatosGuardar.musica;
        Efectos = GameObject.FindGameObjectsWithTag("SoundEffects");
        FX = DatosGuardar.fx;
        for(int i = 0; i<Efectos.Length; i++)
        {
            Efectos[i].GetComponent<AudioSource>().mute = !DatosGuardar.fx;
        }
        
    }
	


    public void volverJugar()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        DatosGuardar.Save();

        if(SceneManager.GetActiveScene().name == "02_escenario_tutorial")
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void CargarMenu()
    {
        DatosGuardar.Monedas+=Puntuacion.monedasRecojidas;
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        DatosGuardar.Save();
        
        SceneManager.LoadScene(1);
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
        DatosGuardar.musica = !DatosGuardar.musica;
    }

    public void QuitarEfectos()
    {
        FX = !FX;
        for(int i = 0; i<Efectos.Length; i++)
        {
            Efectos[i].GetComponent<AudioSource>().mute = !Efectos[i].GetComponent<AudioSource>().mute;
        }
        DatosGuardar.fx = FX;
    }

    public void GastarMonedas()
    {
        if(DatosGuardar.Monedas>=MonedasResucitar)
        {
            DatosGuardar.Monedas -= MonedasResucitar;
            SeguirJugando();
        }

    }

    public void VerAnuncio()
    {
        personaje.resucitadoAnuncio = true;
        SeguirJugando();
    }

    void SeguirJugando()
    {
        //resetear valores
        //Paramos la cancion de "MUERTE"
        sonidoMuerte.Stop();
        sonidoMuerte_02.Stop();

        //Y subimos el sonido de la musica de fondo
        musicaDeFondo.volume = 0.9f;

        animatorProta.Play("LevantarDeCaidaAtras", -1,0);

        personaje.velocidad_fin = personaje.velocidad;

        //GetComponent<Animator>().SetFloat("Speed", 0.0f);

        personaje.desbloquearControles();
        personaje.panelBloqueoControles.SetActive(false);

        GameObject.Find("ctrl_CuentaAtras").GetComponent<Ctrl_CAtras>().iniciarCuenta();

        //timer.ReanuadarTiempo();

        //menu fin de partida
        personaje.Finpartida.SetActive(false);
        
        //desactivar fin de partida
        //timer 3 segundos

        //Reseteamos la camara
        GameObject.FindGameObjectWithTag("ShakeCamara").GetComponent<Animator>().Play("Cam_Standby");

        //Reseteamos las vidas del jugador
        personaje.Vidas = 3;
        personaje.ActualizarVidasInicio();

        Puntuacion.ApagarTextos();

        personaje.resucitado++;
        MonedasResucitar = personaje.resucitado*50;

        GameObject.Find("Recolctables").GetComponent<Ctrl_Recolectables>().enabled = true;
        GameObject.Find("Elementos_Escenario").GetComponent<Ctrl_Fuego>().enabled = true;
    }
}
