using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class movimiento_objetos : MonoBehaviour
{
    public float vision;
    //public float Velocidad;
    //public float FuerzaSalto = 10.0f;
    //public float tiempoDeEspera;
    public float tiempoExplosion;

    float dist;

    Transform jugador;
    Vector3 Destino;
    public Rigidbody rb;
    NavMeshAgent nav;
    //public Transform destino;
    public GameObject MalvaPoff;

    Animator animatorEnemigo;

    movimiento_personaje Vidas;
    //Slider vidas;
    Ctrl_Puntuacion Puntuacion;
    Ctrl_oleadas Oleadas;
    Ctrl_Habilidades Habilidades;

    bool Salto = false;
    /*bool Jvisto = false;
    
    bool stop = false;
    bool suelo = false;
    bool grounded = false;*/

    Vector3 target;

    CapsuleCollider colBomb;

    public GameObject gObj_ParticulasMecha;
    public GameObject Particulasboom;
    public GameObject ParticulasMuerte;

    Enegia energia;
    

    public int VidaEnemigo;

    public float Velocidad_caidaBomba;

    public GameObject FloatingEnergy;
    public GameObject FloatingGhost;
    public GameObject FloatingLifePega;

    public GameObject particulasHitEnemigos;

    public GameObject Moneda;
    public int NumeroMonedas;

    bool Ataque = false;

    public Vector3 OffsetBomba;

    //Variables Sonidos
    AudioSource SonidoRecibirPajaritazoEnemigo;


    // Use this for initialization
    void Start ()
    {
        
        Puntuacion = GameObject.Find("C_Puntuacion").GetComponent<Ctrl_Puntuacion>();
        jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
        //Destino = GameObject.Find("destino").GetComponent<Transform>().position;//new Vector3(0, jugador.position.y, this.transform.position.z);
        nav = GetComponent<NavMeshAgent>();
        colBomb = GetComponent<CapsuleCollider>();
        energia = GameObject.Find("Elementos_Escenario").GetComponent<Enegia>();
        Vidas = GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>();
        //Velocidad = nav.speed;
        animatorEnemigo = gameObject.transform.GetChild(0).GetComponent<Animator>();
        Oleadas = GameObject.Find("creador_objetos").GetComponent<Ctrl_oleadas>();
        Habilidades = GameObject.Find("CTRL_Habilidades").GetComponent<Ctrl_Habilidades>();
        
        if (gameObject.name == "E_Bomb(Clone)")
        {
            print(gameObject.name); 
            animatorEnemigo = gameObject.transform.GetChild(0).GetComponent<Animator>();
            animatorEnemigo.SetBool("enElAire", true);
        }

        VidaEnemigo += (Oleadas.ContadorOleadas * 10);

        //Variables Sonidos
        SonidoRecibirPajaritazoEnemigo = GameObject.Find("SonidoRecibirPajaritazoEnemigo").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update ()
    {
        //ANIMACIONES**********//
        //if (gameObject.name != "E_Normal(Clone)")
        //{
            if (nav.velocity.x != 0)
            {
                animatorEnemigo.SetBool("andando", true);
            }
            else
            {
                animatorEnemigo.SetBool("andando", false);
            }
        //}
        //*********************//

        //target = new Vector3(jugador.position.x,jugador.position.y,this.transform.position.z);
        target = jugador.position;
        dist = Vector3.Distance(jugador.position, transform.position);
        if (nav.enabled)
        {
            if (gameObject.name == "E_Bomb(Clone)")
            {
                nav.SetDestination(target+OffsetBomba);
                if (dist < vision)
                {
                    Invoke("explosion", tiempoExplosion);
                }
            }
            else
            {
                nav.SetDestination(target);
            }

            if(gameObject.name == "E_Pega(Clone)"&&dist<vision)
            {
                gameObject.GetComponent<Ctrl_AtaquePegajoso>().PersonajeVisto=true;
            }
        }
        else if(!nav.enabled&&gameObject.name == "E_Bomb(Clone)")
        {
            float fixedSpeed = Velocidad_caidaBomba * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,jugador.position,fixedSpeed);
        }
        Debug.DrawLine(transform.position, target, Color.green);

        if (gameObject.name == "E_Bomb(Clone)")
        {
            gObj_ParticulasMecha.SetActive(true);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Suelo":                           //objeto toca suelo

                //ANIMACIONES
                /*if (gameObject.name == "E_Bomb(Clone)")
                {
                    animatorEnemigo.SetBool("enElAire", false);
                }*/

                nav.enabled = true;
 
                print("chocamos con suelo");
                switch (gameObject.name)
                {
                    case "E_Normal(Clone)":
                        //Muerte(); //PARTÍCULAS TEMPORALES ...
                        break;
                    case "E_Bomb(Clone)":
                        animatorEnemigo.SetBool("enElAire", false);
                        break;
                    case "E_Pega(Clone)":
                        /*Vector3 PosPoff = new Vector3(transform.position.x, -54f, transform.position.z);
                        //vision = 0;
                        GameObject poff = Instantiate(MalvaPoff, PosPoff, Quaternion.identity);
                        //Destroy(this.gameObject);*/
                        break;
                }
                break;

            case "agua":
                Destroy(this.gameObject);
                break;

            case "Jugador":                       //objeto toca personaje
                if (!Habilidades.sprint)
                {
                    switch (gameObject.name)
                    {
                        case "E_Normal(Clone)":
                            print("quitamos vida");
                            Vidas.quitarvida_Vida();
                            Muerte();
                            //Destroy(this.gameObject);
                            break;

                        case "E_Bomb(Clone)":
                            Vidas.quitarvida_Vida();
                            explosion();
                            //Destroy(this.gameObject);
                            break;

                        case "E_Pega(Clone)":
                            Vidas.quitarvida_Vida();
                            Muerte();
                            break;
                    }
                }
                break;

            case "A_Basico":
            switch (gameObject.name)
                {
                    case "E_Bomb(Clone)":
                        print("desruimos enemigo");


                        //Sonido de recibir daño para el enemigo
                        SonidoRecibirPajaritazoEnemigo.Play();

                        Vector3 posicionParticulasHit = new Vector3(transform.position.x, -47.6f, transform.position.z);
                        Instantiate(particulasHitEnemigos, posicionParticulasHit, Quaternion.identity);


                        Puntuacion.Enemigos_Eliminados++;
                        Muerte();
                        //sumamos energia
                        energia.AñadirEnergia(50);
                        break;

                    default:
                        if (Ataque == false&&Habilidades.AtaqueBasico==true)
                        {
                            Ataque = true;
                            VidaEnemigo -= Habilidades.DañoBasico;

                            //Animacion de recibir daño para el enemigo
                            animatorEnemigo.Play("RecibirGolpe",-1,0);

                            //Sonido de recibir daño para el enemigo
                            SonidoRecibirPajaritazoEnemigo.Play();

                            posicionParticulasHit = new Vector3(transform.position.x, -47.6f, transform.position.z);
                            Instantiate(particulasHitEnemigos, posicionParticulasHit, Quaternion.identity);

                            nav.speed = 0;
                            //GetComponent<Rigidbody>().AddForce(transform.forward * -30, ForceMode.VelocityChange);

                            Invoke("recuperarVelocidadMalvaNormal", 1.5f);

                            if (VidaEnemigo <= 0)
                            {
                                print("desruimos enemigo");
                                Puntuacion.Enemigos_Eliminados++;
                                Muerte();
                                textoEnergia();
                                //sumamos energia
                                energia.AñadirEnergia(50);
                            }
                            else
                            {
                                Vector3 Posiciontextos = new Vector3(transform.position.x, -60f, transform.position.z);
                                var text = Instantiate(FloatingLifePega, Posiciontextos, Quaternion.identity);
                                text.GetComponent<TextMesh>().text = VidaEnemigo.ToString();
                            }
                            Invoke("ActivarAtaque", 0.5f);
                        }
                        break;
                    /*case "E_Bomb(Clone)":
                        print("desruimos enemigo");
                        Puntuacion.Enemigos_Eliminados++;
                        Muerte();
                        //sumamos energia
                        energia.AñadirEnergia(50);
                        break;
                    case "E_Normal(Clone)":
                        print("desruimos enemigo");
                        Puntuacion.Enemigos_Eliminados++;
                        Muerte();
                        //sumamos energia
                        energia.AñadirEnergia(50);
                        break;*/

                }
                break;

            case "A_chorro":
                Puntuacion.Enemigos_Eliminados++;
                Muerte();
                break;

            case "escudo":
                Muerte();
                break;

            case "explosion":
                Puntuacion.Enemigos_Eliminados++;
                Destroy(this.gameObject);
                break;

            case "caida":
                Puntuacion.Enemigos_Eliminados++;
                Muerte();
                break;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, vision);
    }

    void recuperarVelocidadMalvaNormal()
    {
        nav.speed = 25;
    }

    void saltar()
    {
        Debug.DrawLine(transform.position, target, Color.green);
        
        print("saltamos");
    }

    void explosion()
    {
        //animacion de Shake de la camara
        GameObject.FindGameObjectWithTag("ShakeCamara").GetComponent<Animator>().Play("animShake_DobleSalto");


        colBomb.radius += 25f * Time.deltaTime;
        GameObject ParticulasExplosion = Instantiate(Particulasboom, transform.position, Quaternion.identity);
        if (colBomb.radius >= 12f)
        {
            Destroy(this.gameObject);
        }
    }

    void velDespuesSalto()
    {
        if (gameObject.name == "E_Bomb(Clone)")
        {
            //Velocidad = 15f;
        }
    }

    void Muerte()
    {
        //float text
        Vector3 Posiciontextos = new Vector3(transform.position.x, -40f, transform.position.z);
        Instantiate(FloatingGhost, Posiciontextos, Quaternion.identity);

        Vector3 PosicionParticulas = new Vector3(transform.position.x, -52.77079f, transform.position.z);
        GameObject ParticulasDead = Instantiate(ParticulasMuerte, PosicionParticulas, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void ActivarAtaque()
    {
        Ataque = false;
    }

    void textoEnergia()
    {
        Vector3 Posiciontextos = new Vector3(transform.position.x, -40f, transform.position.z);
        Instantiate(FloatingEnergy, Posiciontextos, Quaternion.identity);

        CrearMoneda();
    }

    void CrearMoneda()
    {
        for (int i = 1; i <= NumeroMonedas; i++)
        {
            Instantiate(Moneda,transform.position,Quaternion.identity);
        }
    }
}
