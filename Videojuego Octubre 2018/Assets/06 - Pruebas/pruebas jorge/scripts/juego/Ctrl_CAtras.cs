using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_CAtras : MonoBehaviour {
    public GameObject Grupo_cuenta;
    public Text T_Timer;

    public float Tiempo;

    private bool ActivarCuenta = false;
    public float InicioCuenta;

    Timer tiempo;

    // Use this for initialization
    void Start ()
    {
        tiempo = GameObject.Find("Timer").GetComponent<Timer>();
        iniciarCuenta();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(ActivarCuenta == true)
        {
            Tiempo = -Time.time + InicioCuenta;

            T_Timer.text = Tiempo.ToString("f0");

            if (Tiempo <= 0)
            {
                GameObject.Find("creador_objetos").GetComponent<crar_objeto>().enabled = true;
                Grupo_cuenta.SetActive(false);
                pararcuenta();
                tiempo.iniciarTiempo();
            }
        }
    }

    void pararcuenta()
    {
        ActivarCuenta = false;
    }

    void iniciarCuenta()
    {
        //Grupo_cuenta.SetActive(true);
        GameObject.Find("creador_objetos").GetComponent<crar_objeto>().enabled = false;
        ActivarCuenta = true;
    }
}
