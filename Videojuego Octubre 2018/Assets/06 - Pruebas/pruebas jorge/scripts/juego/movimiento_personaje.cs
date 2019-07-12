using System.Collections;
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

    public bool isGrounded;
    //public Collider Col_Personaje;
    //public Transform feetPos;
    public float checkRadius;
    public LayerMask groundLayers;
    public float FuerzaSalto;

    bool Saltando;
    bool salto;

    public bool DentroFuego;

    Ctrl_Lavadora Lavadora;
    // Use this for initialization

    public Transform puntoReaparicion;

    Animator Animacion;

    public float blink;
    public float immuned;
    public SkinnedMeshRenderer[] modelRender;
    public Mesh[] Geometrias;
    //public SkinnedMeshRenderer model;
    //public Mesh mesh;
    private float blinkTime = 0.1f;
    public float immunedTime;

    void Start()
    {
        //SkinnedMeshRenderer render = GetComponent<SkinnedMeshRenderer>();
        //model.sharedMesh = null;
        //model.SetActive(!model.activeSelf);
        ActualizarVidas();
        joystick = FindObjectOfType<Joystick>();
        rb = GetComponent<Rigidbody>();
        if (GameObject.FindGameObjectWithTag("lavadora")) {
            Lavadora = GameObject.FindGameObjectWithTag("lavadora").GetComponent<Ctrl_Lavadora>();
        }
        velocidad_fin = velocidad;
        //Col_Personaje = GetComponent<BoxCollider>();
        //var rigidbody = GetComponent<Rigidbody>();
        Animacion = gameObject.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {

        //velocidad_fin = vidas.value * 20;
        rb.velocity = new Vector3(joystick.Horizontal * velocidad_fin,
                                         rb.velocity.y,
                                         0);
        /*if (Saltando) //La variable "Saltando" se activa y desactiva en un evento de la animación de saltar adelante
        {
            rb.velocity = new Vector3(joystick.Horizontal * vidas.value * 35, rb.velocity.y, 0);
        }*/

        //Para las animaciones de movimiento
        //***Corrección "temporal" del solapado de animaciones de andar y correr ********
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
        }

        //Para el salto
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, checkRadius, groundLayers);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.up * FuerzaSalto;

        }
        bool salto = Input.GetKeyDown(KeyCode.Space);

        GetComponent<Animator>().SetBool("Salto", salto);


        //Para la orientacion del Prota
        if (!Saltando)
        {
            if (joystick.Horizontal < 0)
            {
                transform.eulerAngles = new Vector3(0, -90, 0);
                //GetComponent<Animator>().Play("Take 001 (1)");
            }
            if (joystick.Horizontal > 0)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                //GetComponent<Animator>().Play("Take 001 (1)");
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
    }

    public void saltoAdelante()
    {
        Saltando = true;
        print("Saltando = TRUE");

    }

    public void lanzarCafé()
    {
        GetComponent<Animator>().Play("Habilidad_LanzarCafé");

    }

    public void finSaltoAdelante()
    {
        Saltando = false;
        print("Saltando = FALSE");
        rb.velocity = new Vector3(0, 0, 0);
        GetComponent<Animator>().SetBool("Salto", false);

    }

    public void Stop()
    {
        rb.velocity = new Vector3(0, 0, 0);
        GetComponent<Animator>().Play("Caida_Atras");
    }

    public void saltar()
    {
        Animacion.SetBool("Salto", true);
        if (isGrounded == true)
        {
            rb.velocity = Vector3.up * FuerzaSalto;
            //bool salto = true;
            
        }

        
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

            case "lavadora":
                if (Lavadora.LavadoraActivada)
                {
                    velocidad_fin = 50;
                }
                break;

            case "Pega":
                velocidad_fin = 30;
                break;

            case "agua":
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

            case "Pega":
                velocidad_fin = velocidad;
                break;

        }
    }

    public void quitarvida_Vida()
    {
        if (immunedTime <= 0)
        {
            Vidas--;

            if (Vidas == 0)
            {
                Finpartida.SetActive(true);
                GameObject.Find("C_Puntuacion").GetComponent<Ctrl_Puntuacion>().Puntuacion_final();
                GameObject.Find("creador_objetos").GetComponent<Ctrl_oleadas>().enabled = false;
                Destroy(GameObject.FindGameObjectWithTag("Enemigo"));
            }
            else
            {
                print("blink");
                immunedTime = immuned;
                //model.enabled = false;
               //modelRender1.enabled = false;
               for (int i = 0; i < modelRender.Length; i++)
               {
                    modelRender[i].sharedMesh = null;
                }
               blinkTime = blink;
            }

            ActualizarVidas();
        }
    }

    void ActualizarVidas()
    {
        T_Vidas.text = "HP." + Vidas.ToString();
    }

    void Reaparecr()
    {
        transform.position = puntoReaparicion.position;
    }
}
