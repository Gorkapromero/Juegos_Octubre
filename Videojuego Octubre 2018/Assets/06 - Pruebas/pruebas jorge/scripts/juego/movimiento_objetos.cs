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
    AgentJumpToTarget Jump;

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
        Jump = GetComponent<AgentJumpToTarget>();

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

        //target = new Vector3(jugador.position.x,jugador.position.y,this.transform.position.z);
        target = jugador.position;
        if (nav.enabled)
        {
            nav.SetDestination(target);
        }
        Debug.DrawLine(transform.position, target, Color.green);

        if (gameObject.name == "E_Bomb(Clone)")
        {
            gObj_ParticulasMecha.SetActive(true);
        }

        dist = Vector3.Distance(jugador.position, transform.position);

        if (gameObject.name == "E_Bomb(Clone)")   //movimiento en el suelo de bomba
        {
            if (dist < vision)
            {
                                
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

                nav.enabled = true;
 
                print("chocamos con suelo");
                switch (gameObject.name)
                {
                    case "E_Normal(Clone)":
                        //Muerte(); //PARTÍCULAS TEMPORALES ...
                        break;
                    case "E_Bomb(Clone)":
                        break;
                    case "E_Pega(Clone)":
                        /*Vector3 PosPoff = new Vector3(transform.position.x, -54f, transform.position.z);
                        //vision = 0;
                        GameObject poff = Instantiate(MalvaPoff, PosPoff, Quaternion.identity);
                        //Destroy(this.gameObject);*/
                        break;
                }
                break;

            case "Jugador":                       //objeto toca personaje
                switch (gameObject.name)
                {
                    case "E_Normal(Clone)":
                        print("quitamos vida");
                        Vidas.quitarvida_Vida();

                        Destroy(this.gameObject);
                        break;
                    case "E_Bomb(Clone)":
                        Vidas.quitarvida_Vida();
                        explosion();
                        Destroy(this.gameObject);
                        break;
                    case "E_Pega(Clone)":
                        Vector3 PosPoff = new Vector3(transform.position.x, -61.5f, transform.position.z);
                        GameObject poff = Instantiate(MalvaPoff, jugador.position, Quaternion.identity);
                        Destroy(this.gameObject);
                        break;
                }
                break;
                

            case "A_Basico":
                print("desruimos enemigo");
                Puntuacion.Enemigos_Eliminados++;
                Muerte();
                //sumamos energia
                energia.AñadirEnergia(50);
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

            /*case "Fuego":
                Destroy(this.gameObject);
                break;*/

            case "caida":
                Muerte();
                break;
        }
    }
 
    private void OnTriggerExit(Collider other)
    {
        print("salimos coll");
        switch (other.tag)
        {
            case "jump":
                if(!Salto)
                {
                    GetComponent<Jump>().Salto(other.name);
                    Salto = true;
                }
               
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
            //Velocidad = 15f;
        }
    }

    void Muerte()
    {
        Vector3 PosicionParticulas = new Vector3(transform.position.x, -52.77079f, transform.position.z);
        GameObject ParticulasDead = Instantiate(ParticulasMuerte, PosicionParticulas, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
