﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class movimiento_objetos : MonoBehaviour
{

    public float vision;
    public float Velocidad = 0.00001f;
    public float FuerzaSalto = 10.0f;
    public float tiempoDeEspera;

    float dist;

    Transform jugador;
    Vector3 Destino;
    public Rigidbody rb;

    Slider vidas;
    Ctrl_Puntuacion Puntuacion;

    bool Jvisto = false;
    bool Salto = false;
    bool stop = false;
    Vector3 target;

    // Use this for initialization
    void Start ()
    {
        Puntuacion = GameObject.Find("C_Puntuacion").GetComponent<Ctrl_Puntuacion>();
        jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
        Destino = new Vector3(0, this.transform.position.y, this.transform.position.z);
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        FuerzaSalto = Velocidad + 10;
        dist = Vector3.Distance(jugador.position, transform.position);
        if ((dist < vision|| Jvisto==true)&&!stop)   //vemos al jugador
        {
            if (!Jvisto)
            {
                Jvisto = true;
                target = jugador.position; // new Vector3(jugador.position.x, this.transform.position.y, jugador.position.z);
            }
            

            float fixedSpeed = Velocidad * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);

            Debug.DrawLine(transform.position, target, Color.green);
            
        }
        else if (!Jvisto&&!stop)      //no vemos jugador
        {
            target = Destino;
            float fixedSpeed = Velocidad * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);
            Debug.DrawLine(transform.position, target, Color.green);
        }

        else if (Salto)
        {
            float fixedSpeed = Velocidad * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);

            Debug.DrawLine(transform.position, target, Color.green);
        }
        
	}

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
                vidas.value--;

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
        switch (other.tag)
        {
            case "jump":
                print("jump");
                stop = true;
                Debug.DrawLine(transform.position, target, Color.green);
                Invoke("saltar", tiempoDeEspera);
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, vision);
    }

    void saltar()
    {
        if (dist < vision)
        {
           target = new Vector3(jugador.position.x, jugador.position.y, jugador.position.z);
        }
        else
        {
            target = new Vector3(target.x, jugador.position.y, jugador.position.z);
        }
        //rb.velocity = Vector3.up* FuerzaSalto ;
        rb.AddForce(Vector3.up * FuerzaSalto, ForceMode.Impulse);
        //rb.AddForce(Vector3.forward * FuerzaSalto, ForceMode.Force);
        Salto = true;
        //stop = false;
        Debug.DrawLine(transform.position, target, Color.green);
        
        print("saltamos");
    }
}
