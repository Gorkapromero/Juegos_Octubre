using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movimiento_personaje : MonoBehaviour
{
    public int Vidas;
    public GameObject[] G_Vidas;

    protected Joystick joystick;
    //public float velocidad_inicial = 100f;
    //public Slider vidas;
    public float velocidad;
    public float velocidad_fin;
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
    //public SkinnedMeshRenderer[] modelRender;
    //public Mesh[] Geometrias;
    public GameObject Geometria;
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

    public GameObject panelBloqueoControles;

    //VariablesSonido
    AudioSource sonidoMuerte;
    AudioSource sonidoMuerte_02;
    AudioSource musicaDeFondo;
    AudioSource sonidoQuitarVida;
    AudioSource sonidoDisparo;

    public int resucitado;
    public bool resucitadoAnuncio;

    void Start()
    {
        //SkinnedMeshRenderer render = GetComponent<SkinnedMeshRenderer>();
        //model.sharedMesh = null;
        //model.SetActive(!model.activeSelf);
        ActualizarVidasInicio();
        
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

            
        }
        else
        {
            //print("movimiento desactivado");

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
                    //print("Doble Salto");
                    animatorProta.Play("DobleSalto",-1,0);
                    GameObject ParticulasDobleSalto = Instantiate(ParticulasAterrizaje, transform.position, Quaternion.identity);

                    rb.velocity = Vector3.up * (FuerzaSalto * 1.2f);
                    Invoke("caidaDobleSalto", 0.5f);
                }

                activarSalto = false;
                DobleSalto = true;

            }
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
                Geometria.SetActive(!Geometria.activeSelf);
                
                blinkTime = blink;
            }
            if(immunedTime <=0)
            {
                Geometria.SetActive(true);
            }
        }

    }

    public void saltoAdelante()
    {
        //Saltando = true;
        //print("Saltando = TRUE");

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
        colliderPajarita.enabled = false;

    }

    public void bloquearControles()
    {
        bloquearControl = true;

        panelBloqueoControles.SetActive(true);


    }

    public void desbloquearControles()
    {
        panelBloqueoControles.SetActive(false);

        bloquearControl = false;
        velocidad_fin = 80;
        recibiendoGolpe = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "agua":
                Vector3 PosicionParticulas = new Vector3(transform.position.x, -56f, transform.position.z);
                Instantiate(ParticulasTeHundes, PosicionParticulas, Quaternion.identity);
                quitarvida_Vida();
                Reaparecr();
                break;

        }
    }

    public void quitarvida_Vida()
    {
        if (immunedTime <= 0)
        {
            quitarVida();

            //Reproducimos el sonido de quitar vida
            sonidoQuitarVida.Play();

            //Y Bloqueamos los controles para ejecutar la animacion de recibir daño
            GetComponent<Animator>().SetFloat("Speed", 0.0f);
            //GetComponent<Rigidbody>().isKinematic = true;
            velocidad_fin = 0;
            recibiendoGolpe = true;

            bloquearControles();


            if (Vidas == 0)
            {                   
                //Reproducimos el sonido de "MUERTE"
                sonidoMuerte.Play();
                sonidoMuerte_02.Play();

                //ejecutamos la animacion de muerteCamara
                animatorCamara.Play("animMuerteCamara");


                //Y bajamos el sonido de la musica de fondo
                musicaDeFondo.volume = 0.0f;

                animatorProta.Play("Muerte",-1,0);
                velocidad_fin = 0;
                GetComponent<Animator>().SetFloat("Speed", 0.0f);
                bloquearControles();
                panelBloqueoControles.SetActive(true);

                //Destruimos enemigos
                GameObject.Find("creador_objetos").GetComponent<Ctrl_oleadas>().PlayerState = EstadoJugador.muerto;

                //quitamos cajas y fuego
                Recolectables.enabled = false;
                GameObject.Find("Elementos_Escenario").GetComponent<Ctrl_Fuego>().enabled = false;

                //menu fin de partida
                Invoke("ActivarFinPartida",2);
                
            }
            else
            {
                Invoke("desbloquearControles", 1.0f);

                animatorProta.Play("RecibirDaño");
                //print("blink");
                immunedTime = immuned;
               blinkTime = blink;
               Instantiate(FloatingLive, transform.position, Quaternion.identity);
               Recolectables.reducirTiempo();
            }
        }
    }

    public void ActualizarVidasInicio()
    {
        for(int i = 0;i<G_Vidas.Length;i++)
        {
            if (i < Vidas)
            {
                G_Vidas[i].SetActive(true);
            }
            else
            {
                G_Vidas[i].SetActive(false);
            }
        }
    }
    public void quitarVida()
    {
        if (Vidas > 0)
        {
            Vidas--;
            G_Vidas[Vidas].SetActive(false);
        }
    }

    public void GanarVida(int vida)
    {
        for (int i = 0; i < vida; i++)
        {
            if (Vidas < G_Vidas.Length)
            {
                Vidas++;
                G_Vidas[Vidas - 1].SetActive(true);
            }
        }
        
    }

    void Reaparecr()
    {
        transform.position = puntoReaparicion.position;
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
        animatorCamara.Play("animShake_DobleSalto");
    }

    public void IrDerecha()
    {
        if (!bloquearControl)
        {
            movimiento = 1;
            Irderecha.color = tempColoropac;
            Irizquierda.color = tempColortrans;
        }
    }

    public void IrIzquierda()
    {
        if (!bloquearControl)
        {
            movimiento = -1;
            Irderecha.color = tempColortrans;
            Irizquierda.color = tempColoropac;
        }
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
        if(resucitadoAnuncio)
        {
            GameObject.Find("B_Anuncio").SetActive(false);
        }
    }
}
