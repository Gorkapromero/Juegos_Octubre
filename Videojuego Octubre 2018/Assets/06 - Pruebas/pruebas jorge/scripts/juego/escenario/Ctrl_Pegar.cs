using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Pegar : MonoBehaviour
{
    movimiento_personaje move;
    public float TiempoPeg;
    bool Dentro;

    Animator AnimatorJugador;
	// Use this for initialization
	void Start ()
    {
		move = GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>();
        AnimatorJugador = GameObject.FindGameObjectWithTag("Jugador").GetComponent<Animator>();
        Invoke("Despegar", TiempoPeg);

    }
	
    private void OnTriggerEnter(Collider other) 
    {
        switch (other.gameObject.tag)
        {
            case "Jugador":   
                Dentro=true;                    //personaje toca pegajoso
                Invoke("Pegar", 0.2f);
                break;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        switch (other.gameObject.tag)
        {
            case "Jugador":                       //personaje toca pegajoso
                Dentro = false;
                break;
        }
    }

    void Despegar()
    {
        Destroy(this.gameObject);
        //move.enabled = true;
        move.desbloquearControles();
    }

    void Pegar()
    {
        if(Dentro)
        {
            print("Nos quedamos pegados");
            move.Stop();
            //move.bloquearControles();
            AnimatorJugador.Play("Caida_Atras");
            //move.enabled = false;
        }
    }
}
