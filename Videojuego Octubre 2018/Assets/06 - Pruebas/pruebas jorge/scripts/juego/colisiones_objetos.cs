using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colisiones_objetos : MonoBehaviour
{
    Slider vidas;
    Ctrl_Puntuacion Puntuacion;

	// Use this for initialization
	void Start ()
    {
        Puntuacion = GameObject.Find("C_Puntuacion").GetComponent<Ctrl_Puntuacion>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider coli)
    {
        switch(coli.gameObject.name)                
        {
            case "suelo":                           //objeto toca suelo
                print("chocamos con "+coli);
                Destroy(this.gameObject);
                break;

            case "personaje":                       //objeto toca personaje
                print("quitamos vida");
                vidas = GameObject.Find("Vida").GetComponent<Slider>();
                vidas.value++;
                
                Destroy(this.gameObject);
                break;

            case "A_Basic(Clone)":
                print("desruimos enemigo");
                Puntuacion.Enemigos_Eliminados++;
                Puntuacion.Actualizar_enemigos();
                Destroy(this.gameObject);
                break;
                
        }
    }
}
