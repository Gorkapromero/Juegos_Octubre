using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text T_Timer;
    public float Tiempo;
    private bool ActivarTiempo = false;
    public float startTime;

    public GameObject Grupo_cuenta;

    // Use this for initialization
    void Start ()
    {
        Grupo_cuenta.SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ActivarTiempo == false)
            return;

        Tiempo = Time.time+startTime;

        string Minutos = ((int)Tiempo / 59).ToString("f0");
        string Segundos = (Tiempo % 59).ToString("f0");

        /*if((int)Tiempo/59==0 && Tiempo % 59 < 59.5f)
        {
            if (Tiempo%59 < 9.5f)
            {
                T_Timer.text = 0+Segundos;
            }
            else
            {
                T_Timer.text = Segundos;
            }
        }
        else
        {*/
            if (Tiempo % 59 < 9.5f)
            {
                T_Timer.text = Minutos + ":" + 0 + Segundos;
            }
            else
            {
                T_Timer.text = Minutos + ":" + Segundos;
            }
            
       // }
	}

    public void PararTiempo()
    {
        ActivarTiempo = false;
    }
    public void iniciarTiempo()
    {
        startTime = -Time.time;
        ActivarTiempo = true;
    }

    public void ReanuadarTiempo()
    {
        startTime = -Time.time + Tiempo;
        ActivarTiempo = true;
    }
}
