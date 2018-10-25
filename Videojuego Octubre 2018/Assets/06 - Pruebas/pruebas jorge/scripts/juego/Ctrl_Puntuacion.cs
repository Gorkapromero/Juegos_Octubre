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

    public Text T_Enemigos;
    public Text TP_Final;

	// Use this for initialization
	void Start ()
    {
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
        P_Final = (Enemigos_Eliminados * 10); //+ el tiempo
        TP_Final.text = P_Final.ToString();
    }
}
