using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enegia : MonoBehaviour {

    public float VelocidadAnimacion;
    public float Energia;
    float EnergiaFinal;
    float EnergiaInicial;
    public Slider BarraEnergia;
    //public float sumar;

    public float energiaMax;

    //public bool energia2;

	// Use this for initialization
	void Start ()
    {
        //AñadirEnergia(sumar);
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
        }
    }

    public void AñadirEnergia(float Valor)
    {
        EnergiaInicial = Energia;
        EnergiaFinal += Valor;
    }
}
