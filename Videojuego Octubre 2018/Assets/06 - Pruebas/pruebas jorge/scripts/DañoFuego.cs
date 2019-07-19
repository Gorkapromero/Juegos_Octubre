using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoFuego : MonoBehaviour
{
    movimiento_personaje Personaje;
    ParticleSystem ps;

    // Use this for initialization
    void Start()
    {
        Personaje = GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>();
        ps = this.GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        /*if (!ps.IsAlive())
        {
            GameObject.Destroy(this.gameObject);
        }*/
    }
    private void OnTriggerEnter(Collider coli)
    {
        switch (coli.gameObject.tag)
        {
            case "Enemigo":
                //destruimos enemigo
                break;
            case "Jugador":
                //restamos vida
                if (!Personaje.DentroFuego)
                {
                    Personaje.quitarvida_Vida();
                    Personaje.DentroFuego = true;
                }
                //añadimos particulas de fuego
                break;
        }
    }

    private void OnTriggerExit(Collider coli)
    {
        switch (coli.gameObject.tag)
        {
            case "Jugador":
                //restamos vida
                Personaje.DentroFuego = false;
                //añadimos particulas de fuego
                break;
        }
    }
}
