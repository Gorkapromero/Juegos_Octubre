using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text T_Timer;
    public float Tiempo;
    private bool ActivarTiempo = true;
    private float startTime;

	// Use this for initialization
	void Start ()
    {
        iniciarTiempo();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ActivarTiempo == false)
            return;

        Tiempo = Time.time-startTime;

        string Minutos = ((int)Tiempo / 60).ToString();
        string Segundos = (Tiempo % 60).ToString("f0");

        if((int)Tiempo/60==0)
        {
            T_Timer.text = Segundos;
        }
        else
        {
            T_Timer.text = Minutos + ":" + Segundos;
        }
	}

    public void PararTiempo()
    {
        ActivarTiempo = false;
    }
    public void iniciarTiempo()
    {
        startTime = Time.time;
        ActivarTiempo = true;
    }
}
