﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class movimiento_objetos : MonoBehaviour
{

    public float vision;
    public float Velocidad;
    public float FuerzaSalto = 10.0f;
    public float tiempoDeEspera;
    public float tiempoExplosion;

    float dist;

    Transform jugador;
    Vector3 Destino;
    public Rigidbody rb;
    NavMeshAgent nav;
    //public Transform destino;
    public GameObject MalvaPoff;

    Animator animatorEnemigo;

    Slider vidas;
    Ctrl_Puntuacion Puntuacion;

    bool Jvisto = false;
    bool Salto = false;
    bool stop = false;
    bool suelo = false;

    Vector3 target;

    CapsuleCollider colBomb;

    public GameObject Particulasboom;
    public GameObject ParticulasMuerte;

    // Use this for initialization
    void Start ()
    {
        Puntuacion = GameObject.Find("C_Puntuacion").GetComponent<Ctrl_Puntuacion>();
        jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
        Destino = GameObject.Find("destino").GetComponent<Transform>().position;//new Vector3(0, jugador.position.y, this.transform.position.z);
        nav = GetComponent<NavMeshAgent>();
        colBomb = GetComponent<CapsuleCollider>();

        if (gameObject.name != "E_Normal(Clone)")
        {
            print(gameObject.name); 
            animatorEnemigo = gameObject.transform.GetChild(0).GetComponent<Animator>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //ANIMACIONES**********//
        if (gameObject.name != "E_Normal(Clone)")
        {
            if (nav.velocity.x != 0)
            {
                animatorEnemigo.SetBool("andando", true);
            }
            else
            {
                animatorEnemigo.SetBool("andando", false);
            }
        }
        //*********************//

        //nav.SetDestination(Destino);
        //FuerzaSalto = Velocidad + 10;
        //Velocidad = nav.speed;
        dist = Vector3.Distance(jugador.position, transform.position);
        if ((dist < vision|| Jvisto==true)&&!stop&&!Salto)   //vemos al jugador antes de saltar
        {
            if (!Jvisto)
            {
                Jvisto = true;
                target = new Vector3(jugador.position.x, jugador.position.y, jugador.position.z); //jugador.position;
            }
            

            //float fixedSpeed = Velocidad * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);
            nav.SetDestination(target);

            Debug.DrawLine(transform.position, target, Color.green);
            
        }
        else if (!Jvisto&&!stop&&!Salto)      //no vemos jugador
        {
            //print("no vemos jugador");
            target = new Vector3(Destino.x,Destino.y,jugador.position.z);
            //float fixedSpeed = Velocidad * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);
            nav.SetDestination(Destino);
            Debug.DrawLine(transform.position, target, Color.green);
        }

        else if (Salto)  //movimiento despues del salto
        {
            //CONTROL DEL AGENTE
            Invoke("velDespuesSalto", 0.10f);
            if (dist < vision && gameObject.name == "E_Bomb(Clone)")
            {
                target = new Vector3(jugador.position.x, jugador.position.y, jugador.position.z); //jugador.position;
                Debug.DrawLine(transform.position, target, Color.green);
            }
            float fixedSpeed = Velocidad * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);
            //nav.SetDestination(target);
            Debug.DrawLine(transform.position, target, Color.green);


            //ANIMACIONES
            if (gameObject.name != "E_Normal(Clone)")
            {

                print("ENEMIGO "+gameObject.name+" en el aire");

                //enElAire = true;
                animatorEnemigo.SetBool("enElAire",true);


            }

        }

        else if (suelo)   //movimiento en el suelo de bomba
        {


            if (dist < vision)
            {
                //nav.speed = 50;
                //nav.acceleration = 20;
                target = new Vector3(jugador.position.x, this.transform.position.y, jugador.position.z); //jugador.position;
                nav.SetDestination(target);
                Debug.DrawLine(transform.position, target, Color.green);
                Invoke("explosion", tiempoExplosion);
            }
        }
        
	}
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Suelo":                           //objeto toca suelo

                //ANIMACIONES
                if (gameObject.name == "E_Bomb(Clone)")
                {
                    animatorEnemigo.SetBool("enElAire", false);
                }

                //CONTROL DEL AGENTE
                suelo = true;
                Salto = false;
                nav.enabled = true;
 
                print("chocamos con suelo");
                switch (gameObject.name)
                {
                    case "E_Normal(Clone)":
                        Muerte(); //PARTÍCULAS TEMPORALES ...
                        break;
                    case "E_Bomb(Clone)":
                        break;
                    case "E_Pega(Clone)":
                        Vector3 PosPoff = new Vector3(transform.position.x, -54f, transform.position.z);
                        vision = 0;
                        GameObject poff = Instantiate(MalvaPoff, PosPoff, Quaternion.identity);
                        Destroy(this.gameObject);
                        break;
                }
                break;

            case "Jugador":                       //objeto toca personaje
                switch (gameObject.name)
                {
                    case "E_Normal(Clone)":
                        print("quitamos vida");
                        vidas = GameObject.Find("Vida").GetComponent<Slider>();
                        vidas.value--;

                        Destroy(this.gameObject);
                        break;
                    case "E_Bomb(Clone)":
                        vidas = GameObject.Find("Vida").GetComponent<Slider>();
                        vidas.value--;
                        explosion();
                        Destroy(this.gameObject);
                        break;
                    case "E_Pega(Clone)":
                        Vector3 PosPoff = new Vector3(transform.position.x, -61.5f, transform.position.z);
                        vision = 0;
                        GameObject poff = Instantiate(MalvaPoff, jugador.position, Quaternion.identity);
                        Destroy(this.gameObject);
                        break;
                }
                break;
                

            case "A_Basico":
                print("desruimos enemigo");
                Puntuacion.Enemigos_Eliminados++;
                Puntuacion.Actualizar_enemigos();
                Muerte();
                break;

            case "A_chorro":
                Puntuacion.Enemigos_Eliminados++;
                Puntuacion.Actualizar_enemigos();
                Muerte();
                break;

            case "escudo":
                Muerte();
                break;

            case "explosion":
                Puntuacion.Enemigos_Eliminados++;
                Puntuacion.Actualizar_enemigos();
                Destroy(this.gameObject);
                break;

            case "jump":
                print("tocamos estanteria");
                rb.isKinematic = true;
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
        nav.enabled = false;
        rb.isKinematic = false;
        if (dist < vision)
        {
            //target = new Vector3(jugador.position.x, this.transform.position.y, jugador.position.z);  //vemos al jugador
            target = new Vector3(jugador.position.x, jugador.position.y, jugador.position.z);
        }
        else
        {
            //target = new Vector3(0, this.transform.position.y, jugador.position.z);                 //ultima direccion
        }
        rb.AddForce(Vector3.up * FuerzaSalto, ForceMode.Impulse);
        if(gameObject.name == "E_Bomb(Clone)")
        {
            Velocidad = 100f;
        }
        Salto = true;
        //stop = false;
        Debug.DrawLine(transform.position, target, Color.green);
        
        print("saltamos");
    }

    void explosion()
    {
        colBomb.radius += 25f * Time.deltaTime;
        GameObject ParticulasExplosion = Instantiate(Particulasboom, transform.position, Quaternion.identity);
        if (colBomb.radius >= 5f)
        {
            Destroy(this.gameObject);
        }
    }

    void velDespuesSalto()
    {
        if (gameObject.name == "E_Bomb(Clone)")
        {
            Velocidad = 15f;
        }
    }

    void Muerte()
    {
        GameObject ParticulasDead = Instantiate(ParticulasMuerte, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
