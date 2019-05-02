using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_ABasico : MonoBehaviour
{

    public float Velocidad = 500f;
    public float Distancia_Max;
    public float Distancia_Min = -50.4883f;

    Ctrl_Habilidades Habilidades;

	// Use this for initialization
	void Start ()
    {
        Habilidades = GameObject.Find("CTRL_Habilidades").GetComponent<Ctrl_Habilidades>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Habilidades.AtaqueBasico==true)
        {
            if(transform.position.z >= (-Distancia_Max-50.4883f))
            {
                print("lanzamos Pajarita");
                print("z pajarita " +transform.position.z);
                transform.position += transform.forward * Velocidad * Time.deltaTime;
           
            }
            else if (transform.position.z <= (-Distancia_Max - 50.4883f))
            {
                print("stop Pajarita");
                Habilidades.AtaqueBasico = false;
            }
            
        }
        else if (Habilidades.AtaqueBasico==false&&transform.position.z <= Distancia_Min)
        {
            print("z pajarita " + transform.position.z);
            transform.position -= transform.forward * Velocidad * Time.deltaTime;
        }
        
	}

    private void OnTriggerEnter(Collider coli)
    {
        switch (coli.gameObject.name)
        {
            case "Top":                           //objeto toca suelo
                print("chocamos con " + coli);
                Destroy(this.gameObject);
                break;

            case "objeto(Clone)":
                print("desruimos enemigo");
                
                Destroy(this.gameObject);
                break;

        }
    }
}
