using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_Puntuacion : MonoBehaviour
{

    public int Enemigos_Eliminados = 0;
    int P_Final;

    //public int Tiempo;
    //public int Total;

    public Text Texto_Tiempo;
    public Text Texto_Oleadas;
    public Text Texto_Enemigos;
    public Text Texto_Score;
    public Text Texto_BestScore;
    public GameObject NewScore;

    Timer tiempo;
    Ctrl_oleadas Oleadas;

    public GameObject PantallaGuardar;

    public InputField Nombre;
    public RankingManager Ranking;

    int BestScore;


    // Use this for initialization
    void Start ()
    {
        tiempo = GameObject.Find("Timer").GetComponent<Timer>();
        Oleadas = GameObject.Find("creador_objetos").GetComponent<Ctrl_oleadas>();
        //Actualizar_enemigos();
        ApagarTextos();
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
        Invoke("MostrarBestscore", 5);
    }

    void Puntuacion_final()
    {
        tiempo.PararTiempo();
        P_Final = (Enemigos_Eliminados * 10) + (Oleadas.ContadorOleadas*100);
    }

    void Guardar()
    {
        Ranking.BorrarPuntos(1);
        Ranking.InsertarMejorPuntuacion(1, P_Final);
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
        BestScore = Ranking.ObtenerPuntuacion();
        if (P_Final > BestScore)
        {
            Guardar();
            //new Score(actualizamos texto bestscore)
            Texto_BestScore.text = P_Final.ToString("f0");
            NewScore.SetActive(true);
        }
        else
        {
            Texto_BestScore.text = BestScore.ToString();
        }
    }

    public void ApagarTextos()
    {
        Texto_Tiempo.text = null;
        Texto_Oleadas.text = null;
        Texto_Enemigos.text = null;
        Texto_Score.text = null;
        Texto_BestScore.text = null;
    }
}
