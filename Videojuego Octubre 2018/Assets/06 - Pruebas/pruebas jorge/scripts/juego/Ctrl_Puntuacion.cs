using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_Puntuacion : MonoBehaviour
{

    public int Enemigos_Eliminados = 0;
    float P_Final;

    //public int Tiempo;
    //public int Total;

    public Text Texto_Tiempo;
    public Text Texto_Oleadas;
    public Text Texto_Enemigos;
    public Text Texto_Score;
    public Text Texto_BestScore;

    Timer tiempo;
    Ctrl_oleadas Oleadas;

    public GameObject PantallaGuardar;

    public InputField Nombre;
    public RankingManager Ranking;


    // Use this for initialization
    void Start ()
    {
        tiempo = GameObject.Find("Timer").GetComponent<Timer>();
        Oleadas = GameObject.Find("creador_objetos").GetComponent<Ctrl_oleadas>();
        //Actualizar_enemigos();
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void Mostrar_Textos()
    {
        Puntuacion_final();
        Invoke("Mostrartiempo", 1);
        Invoke("Mostraroleadas", 2);
        Invoke("MostrarEnemigos", 3);
        Invoke("Mostrarscore", 4);
    }

    void Puntuacion_final()
    {
        tiempo.PararTiempo();
        P_Final = (Enemigos_Eliminados * 10) + (Oleadas.ContadorOleadas*100); 
    }

    public void GuardarPuntuacion()
    {
        PantallaGuardar.SetActive(true);
    }

    public void Guardar()
    {
        Ranking.InsertarPuntos(Nombre.text, P_Final.ToString("f0"));
        PantallaGuardar.SetActive(false);
    }

    void Mostrartiempo()
    {
        Texto_Tiempo.text = tiempo.T_Timer.text;
    }
    void Mostraroleadas()
    {
        Texto_Oleadas.text = Oleadas.ContadorOleadas.ToString();
    }
    void MostrarEnemigos()
    {
        Texto_Enemigos.text = Enemigos_Eliminados.ToString();
    }
    void Mostrarscore()
    {
        Texto_Score.text = P_Final.ToString("f0");
    }
    void MostrarBestscore()
    { 
        
    }
}
