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

    public Text T_Enemigos;
    public Text TP_Final;
    Timer tiempo;

    // Use this for initialization
    void Start ()
    {
        tiempo = GameObject.Find("Timer").GetComponent<Timer>();
        Actualizar_enemigos();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Actualizar_enemigos()
    {
        T_Enemigos.text = "Enemigos Eliminados: " + Enemigos_Eliminados.ToString();
    }

    public void Puntuacion_final()
    {
        tiempo.PararTiempo();
        P_Final = (Enemigos_Eliminados * 10) + tiempo.Tiempo; //+ el tiempo
        TP_Final.text = P_Final.ToString("f0");
    }
}
