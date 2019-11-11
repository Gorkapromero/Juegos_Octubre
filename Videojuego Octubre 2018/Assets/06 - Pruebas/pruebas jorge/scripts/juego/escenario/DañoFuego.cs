using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoFuego : MonoBehaviour
{
    movimiento_personaje Personaje;
    ParticleSystem ps;

    CapsuleCollider ColliderFuego;

    // Use this for initialization
    void Start()
    {
        Personaje = GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>();
        ps = this.GetComponent<ParticleSystem>();

        ColliderFuego = GetComponent<CapsuleCollider>();
        ColliderFuego.enabled = false;
        Invoke("ActivarCollider", 1.5f);
        Invoke("DesactivarCollider", 2.45f);
        Invoke("DestruirFuego", 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!ps.IsAlive())
        {
            GameObject.Destroy(this.gameObject);
        }*/
    }

    void DestruirFuego()
    {
        Destroy(this.transform.parent.gameObject);
    }

    void DesactivarCollider()
    {
        ColliderFuego.enabled = false;

    }

    void ActivarCollider()
    {
        ColliderFuego.enabled = true;
    }

    private void OnTriggerEnter(Collider coli)
    {
        switch (coli.gameObject.tag)
        {
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
                Personaje.DentroFuego = false;
                break;
        }

    }


}