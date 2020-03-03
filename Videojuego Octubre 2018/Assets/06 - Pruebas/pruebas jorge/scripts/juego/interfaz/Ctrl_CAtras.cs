using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_CAtras : MonoBehaviour {
    public GameObject Grupo_cuenta;
    public Text T_Timer;

    public float Tiempo;
    public GameObject GO;

    private bool ActivarCuenta = false;
    public float InicioCuenta;

    private float Cuenta;
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
            Tiempo = -Time.time + Cuenta;

            T_Timer.text = Tiempo.ToString("f0");

            if (Tiempo <= 0)
            {
                if(GameObject.Find("creador_objetos"))
                {
                    GameObject.Find("creador_objetos").GetComponent<Ctrl_oleadas>().enabled = true;
                    GameObject.Find("creador_objetos").GetComponent<Ctrl_oleadas>().PlayerState = EstadoJugador.Vivo;
                    GameObject.Find("creador_objetos").GetComponent<Ctrl_oleadas>().Estado = Ctrl_oleadas.SpawnState.COUNTING;
                    if (GO)
                    {
                        GO.SetActive(true);

                        Invoke("ApagarTexto",3f);
                    }
                    
                }
                
                Grupo_cuenta.SetActive(false);
                pararcuenta();
                tiempo.ReanuadarTiempo();
            }
        }
    }

    void pararcuenta()
    {
        ActivarCuenta = false;
    }

    public void iniciarCuenta()
    {   
        if(GO)
        {
            GO.SetActive(false); 
        }
        
        Grupo_cuenta.SetActive(true);
        Cuenta = Time.time + InicioCuenta;
        if(GameObject.Find("creador_objetos"))
        {
            GameObject.Find("creador_objetos").GetComponent<Ctrl_oleadas>().enabled = false;
        }
        
        ActivarCuenta = true;
    }

    void ApagarTexto()
    {
        GO.SetActive(false);
    }
}
