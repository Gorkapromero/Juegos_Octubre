﻿using System.Collections;
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
    public Text Texto_Monedas;
    public GameObject NewScore;

    public Text MonedasRecojidas;

    Timer tiempo;
    Ctrl_oleadas Oleadas;
    DatosGuardados DatosGuardar;

    public GameObject PantallaGuardar;

    public InputField Nombre;
    //public RankingManager Ranking;

    int monedas;

    public float VelocidadAnimacion;
    float Valor;
    float ValorFinal;
    float ValorInicial = 0;
    Text Texto;

    bool B_tiempo = true;
    bool FinTextos = true;
    bool restarmonedas = false;
    float ValorMonedas;
    int MonedasInicio;
    public int texto = 1;

    public int monedasRecojidas;


    // Use this for initialization
    void Start ()
    {
        DatosGuardar =GameObject.Find("Datosguardados").GetComponent<DatosGuardados>();
        tiempo = GameObject.Find("Timer").GetComponent<Timer>();
        Oleadas = GameObject.Find("creador_objetos").GetComponent<Ctrl_oleadas>();
        //Actualizar_enemigos();
        ApagarTextos();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(restarmonedas)
        {
            ValorMonedas -= (VelocidadAnimacion * Time.deltaTime)*(monedasRecojidas);
            
            if(ValorMonedas<=0)
            {
                ValorMonedas = 0;
                restarmonedas=false;
            }
            MonedasRecojidas.text = ValorMonedas.ToString("f0");
        }
        if (Valor != ValorFinal)
        {
            if (ValorInicial < ValorFinal)
            {
                Valor += (VelocidadAnimacion * Time.deltaTime) * (ValorFinal - ValorInicial);
                if(B_tiempo)
                {
                    string Minutos = ((int)Valor / 59).ToString("f0");
                    string Segundos = (Valor % 59).ToString("f0");
                    if (Valor % 59 < 9.5f)
                    {
                        Texto.text = Minutos + ":" + 0 + Segundos;
                    }
                    else
                    {
                        Texto.text = Minutos + ":" + Segundos;
                    }
                }
                else
                {
                    Texto.text = Valor.ToString("f0");
                }
                
                if (Valor >= ValorFinal)
                {
                    Valor = ValorFinal;
                    if (B_tiempo)
                    {
                        string Minutos = ((int)Valor / 59).ToString("f0");
                        string Segundos = (Valor % 59).ToString("f0");
                        if (Valor % 59 < 9.5f)
                        {
                            Texto.text = Minutos + ":" + 0 + Segundos;
                        }
                        else
                        {
                            Texto.text = Minutos + ":" + Segundos;
                        }
                    }
                    else
                    {
                        Texto.text = Valor.ToString("f0");
                    }

                    texto++;
                    MostrarTexto(texto);
                }

            }
        }
        else if(Valor==ValorFinal&&!FinTextos)
        {
            Valor = ValorFinal;
            Texto.text = Valor.ToString();

            texto++;
            MostrarTexto(texto);
        }

    }

    public void Mostrar_Textos()
    {
        FinTextos = false;
        Puntuacion_final();
        MostrarTexto(texto);
        //MonedasTotales.text = "TIENES " + DatosGuardar.Monedas.ToString() + " MONEDAS";
    }

    void Puntuacion_final()
    {
        tiempo.PararTiempo();
        P_Final = (Enemigos_Eliminados * 10) + (Oleadas.ContadorOleadas*100);
    }

    void Guardar()
    {
        DatosGuardar.puntuacion = P_Final;
        /*Ranking.BorrarPuntos(1);
        Ranking.InsertarMejorPuntuacion(1, P_Final);*/
    }

    void Mostrartiempo()
    {
        print("tiempo");
        B_tiempo = true;
        Valor = ValorInicial;
        Texto = Texto_Tiempo;
        ValorFinal = tiempo.Tiempo;
        //Texto_Tiempo.text = tiempo.T_Timer.text;
    }
    void Mostraroleadas()
    {
        print("oleadas");
        B_tiempo = false;
        FinTextos = false;
        Valor = ValorInicial;
        Texto = Texto_Oleadas;
        ValorFinal = Oleadas.ContadorOleadas;
        //Texto_Oleadas.text = Oleadas.ContadorOleadas.ToString();
    }
    void MostrarEnemigos()
    {
        print("Enemigos");
        B_tiempo = false;
        FinTextos = false;
        Valor = ValorInicial;
        Texto = Texto_Enemigos;
        ValorFinal = Enemigos_Eliminados;
        //Texto_Enemigos.text = Enemigos_Eliminados.ToString();
    }
    void Mostrarscore()
    {
        print("score");
        B_tiempo = false;
        FinTextos = false;
        Valor = ValorInicial;
        Texto = Texto_Score;
        ValorFinal = P_Final;
        //Texto_Score.text = P_Final.ToString("f0");
    }
    void MostrarMonedas()
    {
        monedas = DatosGuardar.Monedas;
        print("monedas");
        //B_tiempo = false;
        //FinTextos = false;
        Valor = ValorInicial;
        Texto = Texto_Monedas;
        ValorMonedas = monedasRecojidas;
        restarmonedas = true;
        ValorFinal = DatosGuardar.Monedas + monedasRecojidas;
    }

    public void ApagarTextos()
    {
        Texto_Tiempo.text = null;
        Texto_Oleadas.text = null;
        Texto_Enemigos.text = null;
        Texto_Score.text = null;
        Texto_Monedas.text = DatosGuardar.Monedas.ToString();
        NewScore.SetActive(false);
        texto = 1;
        Valor = ValorInicial;
        ValorFinal = 0;
        MonedasRecojidas.text = monedasRecojidas.ToString();
    }

    void MostrarTexto(int x)
    {
        switch(x)
        {
            case 1:
                Mostrartiempo();
                break;
            case 2:
                FinTextos = true;
                Invoke("Mostraroleadas", 0.5f);
                //Mostraroleadas();
                break;
            case 3:
                FinTextos = true;
                Invoke("MostrarEnemigos", 0.5f);
                //MostrarEnemigos();
                break;
            case 4:
                FinTextos = true;
                Invoke("Mostrarscore", 0.5f);
                //Mostrarscore();
                break;
            case 5:
                FinTextos = true;
                Invoke("MostrarMonedas", 0.5f);
                break;
        }
    }

    public void ActualizarMonedas()
    {
        monedasRecojidas++;
        MonedasRecojidas.text = monedasRecojidas.ToString();
    }
}
