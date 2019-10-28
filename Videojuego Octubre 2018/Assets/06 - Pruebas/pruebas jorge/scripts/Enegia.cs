using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enegia : MonoBehaviour {

    public float VelocidadAnimacion;
    public float VelocidadGastoEscudo;
    public float Energia;
    public float EnergiaFinal;
    public float EnergiaInicial;
    public Slider BarraEnergia;
    //public float sumar;

    public float energiaMax;

    Ctrl_Habilidades Habilidades;
    //public bool energia2;

	// Use this for initialization
	void Start ()
    {
        //AñadirEnergia(sumar);
        BarraEnergia.maxValue = energiaMax;
        BarraEnergia.value = Energia;
        Habilidades = GameObject.Find("CTRL_Habilidades").GetComponent<Ctrl_Habilidades>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Energia != EnergiaFinal)
        {
            if(EnergiaInicial<EnergiaFinal)
            {
                Energia += (VelocidadAnimacion * Time.deltaTime) * (EnergiaFinal - EnergiaInicial);
                BarraEnergia.value = Energia;
                if (Energia >= EnergiaFinal)
                {
                    Energia = EnergiaFinal;
                    BarraEnergia.value = Energia;
                }
               
                if(Energia>energiaMax)
                {
                    Energia = energiaMax;
                }
            }
            else
            {
                Energia -= (EnergiaInicial - EnergiaFinal);
                BarraEnergia.value = Energia;
            }
        }

        if(Habilidades.escudoActivado==true)
        {
            Energia -= VelocidadGastoEscudo * Time.deltaTime;
            if(Energia<=0)
            {
                Energia = 0;
                Habilidades.escudooff();
            }
        }

    }

    public void AñadirEnergia(float Valor)
    {
        if (EnergiaFinal < energiaMax)
        {
            EnergiaInicial = Energia;
            EnergiaFinal += Valor;
        }
    }

    public void RestarEnergia(float Valor)
    {
        EnergiaInicial = Energia;
        EnergiaFinal += -Valor;
    }
}
