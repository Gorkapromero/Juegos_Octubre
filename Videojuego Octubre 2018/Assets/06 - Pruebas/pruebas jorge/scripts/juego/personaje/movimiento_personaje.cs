﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movimiento_personaje : MonoBehaviour
{
    public int Vidas;
    public Text T_Vidas;

    protected Joystick joystick;
    //public float velocidad_inicial = 100f;
    //public Slider vidas;
    public float velocidad;
    float velocidad_fin;
    public GameObject Finpartida;
    Rigidbody rb;

    public bool atacando;
    public bool isGrounded;
    public bool activarSalto = false;
    //public Collider Col_Personaje;
    //public Transform feetPos;
    public float checkRadius;
    public LayerMask groundLayers;
    public float FuerzaSalto = 100f;

    public bool DobleSalto;

    public bool DentroFuego;

    //Ctrl_Lavadora Lavadora;
    // Use this for initialization

    public Transform puntoReaparicion;

    Animator animatorProta;

    public float blink;
    public float immuned;
    public SkinnedMeshRenderer[] modelRender;
    public Mesh[] Geometrias;
    //public SkinnedMeshRenderer model;
    //public Mesh mesh;
    private float blinkTime = 0.1f;
    public float immunedTime;

    public bool bloquearControl;

    public GameObject ParticulasAterrizaje;
    public GameObject ParticulasAtaqueAereo;
    public GameObject ParticulasCaidaDobleSalto;
    public GameObject ParticulasTeHundes;
    
    Ctrl_Habilidades script_ctl_habilidades;
    Ctrl_Recolectables Recolectables;
    //Ctrl_Estropajo1 Ctrl_Estropajo;

    public Collider CollDobleSalto;

    Animator animatorCamara;

    public GameObject FloatingLive;

    bool EnMiel;
    bool recibiendoGolpe;

    int movimiento;
    public int DireccionProta;

    public Image Irderecha;
    public Image Irizquierda;
    Color tempColortrans;
    Color tempColoropac;


    //VariablesSonido
    AudioSource sonidoMuerte;
    AudioSource sonidoMuerte_02;
    AudioSource musicaDeFondo;
    AudioSource sonidoQuitarVida;
    AudioSource sonidoDisparo;

    void Start()
    {
        //SkinnedMeshRenderer render = GetComponent<SkinnedMeshRenderer>();
        //model.sharedMesh = null;
        //model.SetActive(!model.activeSelf);
        ActualizarVidas();
        
        rb = GetComponent<Rigidbody>();
        /*if (GameObject.FindGameObjectWithTag("lavadora")) {
            Lavadora = GameObject.FindGameObjectWithTag("lavadora").GetComponent<Ctrl_Lavadora>();
        }*/
        velocidad_fin = velocidad;
        //Col_Personaje = GetComponent<BoxCollider>();
        //var rigidbody = GetComponent<Rigidbody>();
        animatorProta = gameObject.GetComponent<Animator>();

        script_ctl_habilidades = GameObject.Find("CTRL_Habilidades").GetComponent<Ctrl_Habilidades>();
      //  Ctrl_Estropajo = GameObject.Find("Elementos_Escenario").GetComponent<Ctrl_Estropajo1>();

        animatorCamara = GameObject.FindGameObjectWithTag("ShakeCamara").GetComponent<Animator>();
        //joystick = FindObjectOfType<Joystick>();

        Recolectables = GameObject.Find("Recolctables").GetComponent<Ctrl_Recolectables>();

        tempColortrans = Irderecha.color;
        tempColoropac = Irizquierda.color;
        tempColortrans.a = 0.5f;
        tempColoropac.a = 1f;

        //Variables Sonido
        musicaDeFondo = GameObject.Find("MusicaFondo").GetComponent<AudioSource>();
        sonidoMuerte = GameObject.Find("SonidoMuerte").GetComponent<AudioSource>();
        sonidoMuerte_02 = GameObject.Find("SonidoMuerte_02").GetComponent<AudioSource>();
        sonidoQuitarVida = GameObject.Find("SonidoQuitarVida").GetComponent<AudioSource>();

    }


    // Update is called once per frame
    void Update()
    {
        //Para las animaciones de movimiento
        if (script_ctl_habilidades.AtaqueBasico == false && bloquearControl == false)
        {
            //velocidad_fin = vidas.value * 20;
            rb.velocity = new Vector3(/*joystick.Horizontal*/movimiento * velocidad_fin,
                                         rb.velocity.y,
                                         0);
            GetComponent<Animator>().SetFloat("Speed", movimiento);
            //GetComponent<Rigidbody>().isKinematic = false;

            /*//***Corrección "temporal" del solapado de animaciones de andar y correr ********
            if (joystick.Horizontal < 0.62 && joystick.Horizontal > 0.34)
            {
                GetComponent<Animator>().SetFloat("Speed", 0.34f);
            }
            else if (joystick.Horizontal > -0.62 && joystick.Horizontal < -0.34)
            {
                GetComponent<Animator>().SetFloat("Speed", -0.34f);
            }
            //*********************************************************************************
            else
            {
                GetComponent<Animator>().SetFloat("Speed", (float)System.Math.Round(joystick.Horizontal, 2));
            }*/
        }
        else
        {
            print("movimiento desactivado");

            GetComponent<Animator>().SetFloat("Speed", 0.0f);

        }


        //Para el salto
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, checkRadius, groundLayers);

        if (isGrounded == true) //Mientras el prota está en el suelo...
        {
            if (Input.GetKeyDown(KeyCode.Space) || (activarSalto)) {
                //particulas de salto
                GameObject ParticulasDobleSalto = Instantiate(ParticulasAterrizaje, transform.position, Quaternion.identity);

                rb.velocity = Vector3.up * FuerzaSalto;
                animatorProta.SetBool("Salto", true);

                activarSalto = false;
                animatorProta.SetBool("AtaqueAereo", false);
            }

            DobleSalto = false;

            
        }
       
        //Si el prota no está en el suelo...
        if (isGrounded == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || (activarSalto))
            {

                if (!DobleSalto)
                {
                    print("Doble Salto");
                    animatorProta.Play("DobleSalto");
                    GameObject ParticulasDobleSalto = Instantiate(ParticulasAterrizaje, transform.position, Quaternion.identity);

                    rb.velocity = Vector3.up * (FuerzaSalto * 1.2f);
                    Invoke("caidaDobleSalto", 0.5f);
                }

                activarSalto = false;
                DobleSalto = true;

            }
            //ATAQUE AEREO
            /*if (Input.GetKeyDown(KeyCode.LeftControl) || script_ctl_habilidades.AtaqueBasico)
            {
                GameObject P_AtaqueAereo = Instantiate(ParticulasAtaqueAereo, transform.position, Quaternion.identity);
                animatorProta.Play("AtaqueAereo");

                script_ctl_habilidades.AtaqueBasico = false;

            }*/
            animatorProta.SetBool("Salto", false);

        }


        //Para la orientacion del Prota
        if (isGrounded == true && !script_ctl_habilidades.AtaqueBasico)
        {
            if (movimiento==-1)
            {
                transform.eulerAngles = new Vector3(0, -90, 0);
                DireccionProta = -1;
            }
            if (movimiento==1)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                DireccionProta = 1;
            }

            /*if (joystick.Horizontal == 0)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                //GetComponent<Animator>().Play("Take 001");
            }*/

        }
        
        //BLINK
        if (immunedTime > 0)
        {
            immunedTime -= Time.deltaTime;

            blinkTime -= Time.deltaTime;

            if (blinkTime <= 0)
            {
                //model.enabled = !model.enabled;
                //model.SetActive(!model.activeSelf);
                for (int i = 0; i < modelRender.Length; i++)
                {
                    if (modelRender[i].sharedMesh != null)
                    {
                        modelRender[i].sharedMesh = null;
                    }
                    else
                    {
                        modelRender[i].sharedMesh = Geometrias[i];
                    }
                }
                blinkTime = blink;
            }
            if(immunedTime <=0)
            {
                for (int i = 0; i < modelRender.Length; i++)
                {
                    modelRender[i].sharedMesh = Geometrias[i];
                }
                //model.enabled = true;
            }
        }

        if(!EnMiel&&bloquearControl&&!recibiendoGolpe)
        {
            desbloquearControles();

        }
    }

    public void saltoAdelante()
    {
        //Saltando = true;
        //print("Saltando = TRUE");

    }

    public void lanzarCafé()
    {
        animatorProta.Play("Habilidad_LanzarCafé");

    }

    public void finSaltoAdelante()
    {
        //Saltando = false;
        //print("Saltando = FALSE");
        rb.velocity = new Vector3(0, 0, 0);
        animatorProta.SetBool("Salto", false);
        activarSalto = false;
    }

    public void Stop()
    {
        velocidad_fin=0;
        bloquearControles();
        //animatorProta.Play("Caida_Atras");
    }

    public void saltar()
    {
        activarSalto = true;
    }

    public void desactivarAtaqueBasico()
    {
        animatorProta.SetBool("LanzarPajarita", false);
        script_ctl_habilidades.AtaqueBasico = false;

        //Aumentamos el tamaño del collider de la pajarita
        BoxCollider colliderPajarita = GameObject.Find("Rigging_Mug_v1_1_ctl_pajarita").GetComponent<BoxCollider>();
        colliderPajarita.size = new Vector3(colliderPajarita.size.x, colliderPajarita.size.y, 1.0f);

    }

    public void bloquearControles()
    {
       bloquearControl = true;

    }

    public void desbloquearControles()
    {
        bloquearControl = false;
        velocidad_fin = 80;
        recibiendoGolpe = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Fuego":
                if (!DentroFuego)
                {
                    //quitarvida_Vida();
                }
                break;

            /*case "lavadora":
                if (Lavadora.LavadoraActivada)
                {
                    velocidad_fin = 50;
                }
                break;*/

            case "miel":
                //bloquearControles();
                EnMiel = true;
                animatorProta.SetBool("AndarPegajoso", true);
                velocidad_fin = 30;
                bloquearControles();
                break;

            case "agua":
                Vector3 PosicionParticulas = new Vector3(transform.position.x, -56f, transform.position.z);
                Instantiate(ParticulasTeHundes, PosicionParticulas, Quaternion.identity);
                quitarvida_Vida();
                Reaparecr();
                break;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "lavadora":
                velocidad_fin = velocidad;
                break;

            case "miel":
                EnMiel = false;
                animatorProta.SetBool("AndarPegajoso", false);
                velocidad_fin = velocidad;
                //desbloquearControles();
                //Invoke("desbloquearControles", 0.15f);

                break;

        }
    }

    public void quitarvida_Vida()
    {
        if (immunedTime <= 0)
        {
            Vidas--;

            //Reproducimos el sonido de quitar vida
            sonidoQuitarVida.Play();

            //Y Bloqueamos los controles para ejecutar la animacion de recibir daño
            GetComponent<Animator>().SetFloat("Speed", 0.0f);
            //GetComponent<Rigidbody>().isKinematic = true;
            velocidad_fin = 0;
            recibiendoGolpe = true;

            bloquearControles();
            Invoke("desbloquearControles",1.0f);


            if (Vidas == 0)
            {
                //Desactivamos controles
                bloquearControles();
                
                //Reproducimos el sonido de "MUERTE"
                sonidoMuerte.Play();
                sonidoMuerte_02.Play();

                //Y bajamos el sonido de la musica de fondo
                musicaDeFondo.volume = 0.05f;

                animatorProta.Play("Muerte");
                velocidad_fin = 0;

                //Destruimos enemigos
                GameObject.Find("creador_objetos").GetComponent<Ctrl_oleadas>().PlayerState = EstadoJugador.muerto;

                //menu fin de partida
                Invoke("ActivarFinPartida",3);
                
            }
            else
            {

                animatorProta.Play("RecibirDaño");
                print("blink");
                immunedTime = immuned;
                //model.enabled = false;
               //modelRender1.enabled = false;
               for (int i = 0; i < modelRender.Length; i++)
               {
                    modelRender[i].sharedMesh = null;
               }
               blinkTime = blink;
               Instantiate(FloatingLive, transform.position, Quaternion.identity);
               Recolectables.reducirTiempo();
            }

            ActualizarVidas();
        }
    }

    public void ActualizarVidas()
    {
        T_Vidas.text = "HP." + Vidas.ToString();
    }

    void Reaparecr()
    {
        transform.position = puntoReaparicion.position;

        //resetearElEstropajo
        //Ctrl_Estropajo.resetearEstropajo();

    }

    void caidaDobleSalto()
    {
        CollDobleSalto.enabled = true;
        rb.velocity = Vector3.up * (-FuerzaSalto * 4);
        
        
        Invoke("apagarColliderSalto", 0.1f);
    }

    void apagarColliderSalto()
    {
        GameObject ParticulasCaidadoblesalto = Instantiate(ParticulasCaidaDobleSalto, transform.position, Quaternion.identity);
        CollDobleSalto.enabled = false;
        //animatorCamara.Play("animShake_DobleSalto");
    }

    public void IrDerecha()
    {
        movimiento = 1;
        Irderecha.color = tempColoropac;
        Irizquierda.color = tempColortrans;
    }

    public void IrIzquierda()
    {
        movimiento = -1;
        Irderecha.color = tempColortrans;
        Irizquierda.color = tempColoropac;
    }
    public void Parar()
    {
        movimiento = 0;
        Irderecha.color = tempColortrans;
        Irizquierda.color = tempColortrans;
    }

    void ActivarFinPartida()
    {
        Finpartida.SetActive(true);
        GameObject.Find("C_Puntuacion").GetComponent<Ctrl_Puntuacion>().Mostrar_Textos();
    }
}