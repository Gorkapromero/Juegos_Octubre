using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class colisiones_objetos : MonoBehaviour
{
    Slider vidas;
    Ctrl_Puntuacion Puntuacion;
    public Rigidbody rb;
    public float FuerzaSalto = 10.0f;

	// Use this for initialization
	void Start ()
    {
        Puntuacion = GameObject.Find("C_Puntuacion").GetComponent<Ctrl_Puntuacion>();
        //rb = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /*private void OnTriggerEnter(Collider coli)
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
    }*/
    private void OnCollisionEnter(Collision coli)
    {

        switch (coli.gameObject.name)
        {
            case "suelo":                           //objeto toca suelo
                print("chocamos con suelo");
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
    private void OnTriggerExit(Collider other)
    {
        print("salimos coll");
        switch(other.tag)
        {
            case "jump":

                rb.velocity = Vector3.up * FuerzaSalto;
                //rb.AddForce(Vector3.up * FuerzaSalto);
                print("saltamos");
                break;
        }
    }
}
