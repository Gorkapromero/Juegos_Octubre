using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ctrl_Habilidades : MonoBehaviour
{
    public bool AtaqueBasico;
    public bool AtaqueAereo;
    public float Tiempo_ABasico;
    

    [System.Serializable]
    public class Habilidades
    {
        public string nombre;
        public GameObject habilidad;
        public float Energia_necesaria;
        public GameObject tiempo;
        public EventTrigger Boton;
        //public Text tiempo_texto;
    }

    public List<Habilidades> tablahabilidades = new List<Habilidades>();

    public float tiempoEscudo;
    public GameObject tiempo;
    //public GameObject ParticulasExplosion; 
    public GameObject ParticulasSalpicadura;


    public bool t_basico = false;
    bool t_chorro = false;
    bool t_escudo = false;
    bool t_explosion = false;

    float t1;
    float t2;
    float t3;
    float t0;

    Enegia Energia_Total;

    public float FuerzaSalto;
    public bool sprint;
    Rigidbody RBplayer;

    protected Joystick joystick;

    // Use this for initialization
    void Start ()
    {
        RBplayer = GameObject.FindGameObjectWithTag("Jugador").GetComponent<Rigidbody>();
        Energia_Total = GameObject.Find("Elementos_Escenario").GetComponent<Enegia>();
        joystick = FindObjectOfType<Joystick>();
        /*t0 = tablahabilidades[0].T_enfriamiento;
        t1 = tablahabilidades[1].T_enfriamiento;
        t2 = tablahabilidades[2].T_enfriamiento;
        t3 = tablahabilidades[3].T_enfriamiento;*/
        tablahabilidades[1].tiempo.SetActive(true);
        tablahabilidades[2].tiempo.SetActive(true);
        tablahabilidades[3].tiempo.SetActive(true);

    }


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Ataque_Basico();
        }
    }
    // Update is called once per frame
    void Update ()
    {

                    /*
                            if (t_basico == true)
                            {
                                print("tiempo basico on");
                                /*if (tablahabilidades[0].tiempo.activeSelf == false)
                                {
                                    print("activamos");
                                    tablahabilidades[0].tiempo.SetActive(true);
                                }
                                tablahabilidades[1].tiempo_texto.text = t1.ToString("f0");
                                t0 -= Time.deltaTime;
                                //print(t1);
                                if (t0 <= 0)
                                {
                                    //t_chorro = false;
                                    t0 = Tiempo_ABasico;
                                    //tablahabilidades[0].tiempo.SetActive(false);
                                    GameObject.Find("A_Basico").GetComponent<Button>().enabled = true;
                                    print("FIN ATAQUE BASICO");
		
                                }

                            }

                    */

        //ACTIVAMOS CHORRO SI TENEMOS ENERGIA
        if (tablahabilidades[1].Energia_necesaria <= Energia_Total.Energia)
        {
            //print("tiempo chorro on");
            if(tablahabilidades[1].tiempo.activeSelf == true)
            {
                print("activamos");
                tablahabilidades[1].tiempo.SetActive(false);
                tablahabilidades[1].Boton.enabled = true;
            }
            /*
            tablahabilidades[1].tiempo_texto.text = t1.ToString("f0");
            t1 -= Time.deltaTime;
            //print(t1);
            if (t1 <= 0)
            {
                t_chorro = false;
                t1 = tablahabilidades[1].T_enfriamiento;
                tablahabilidades[1].tiempo.SetActive(false);
                GameObject.Find("A_chorro").GetComponent<Button>().enabled = true;
            }*/
            
        }
        else
        {
            tablahabilidades[1].tiempo.SetActive(true);
            tablahabilidades[1].Boton.enabled = false;
        }


        //ACTIVAMOS EXPLOSION SI TENEMOS ENERGIA
        if (tablahabilidades[2].Energia_necesaria <= Energia_Total.Energia)
        {
		 
            print("tiempo explosion on");
            if (tablahabilidades[2].tiempo.activeSelf == true)
            {
                print("activamos");
                tablahabilidades[2].tiempo.SetActive(false);
                tablahabilidades[2].Boton.enabled = true;

            }
            /*
            tablahabilidades[2].tiempo_texto.text = t2.ToString("f0");
            t2 -= Time.deltaTime;
            //print(t1);
            if (t2 <= 0)
            {
				
                t_explosion = false;
                t2 = tablahabilidades[2].T_enfriamiento;
                tablahabilidades[2].tiempo.SetActive(false);
                GameObject.Find("A_Explosion").GetComponent<Button>().enabled = true;


            }*/

        }
        else
        {
            tablahabilidades[2].tiempo.SetActive(true);
            tablahabilidades[2].Boton.enabled = false;
        }

        //ACTIVAMOS ESCUDO SI TENEMOS ENERGIA
        if (tablahabilidades[3].Energia_necesaria <= Energia_Total.Energia)
        {
            print("tiempo escudo on");
            if (tablahabilidades[3].tiempo.activeSelf == true)
            {
                print("activamos");
                tablahabilidades[3].tiempo.SetActive(false);
                tablahabilidades[3].Boton.enabled = true;
            }
            /*
            tablahabilidades[3].tiempo_texto.text = t3.ToString("f0");
            t3 -= Time.deltaTime;
            //print(t1);
            if (t3 <= 0)
            {
                t_escudo = false;
                t3 = tablahabilidades[1].T_enfriamiento;
                tablahabilidades[3].tiempo.SetActive(false);
                GameObject.Find("Escudo").GetComponent<Button>().enabled = true;
            }*/

        }
        else
        {
            tablahabilidades[3].tiempo.SetActive(true);
            tablahabilidades[3].Boton.enabled = false;
        }

        if(sprint)
        {
            if(joystick.Horizontal<0)
            {
                RBplayer.velocity = Vector3.right * -FuerzaSalto;  
            }
            else if (joystick.Horizontal>0)
            {
                RBplayer.velocity = Vector3.right * FuerzaSalto;
            }
            RBplayer.velocity = Vector3.right * -FuerzaSalto;
            GameObject.FindGameObjectWithTag("Jugador").GetComponent<Animator>().Play("Salto+Adelante");
            Invoke("move", 0.5f);
        }
        //print("Joystick: " + joystick.Horizontal);
    }

    public void Ataque_Basico()
    {
        t_basico = true;
        AtaqueBasico = true;
        GameObject.FindGameObjectWithTag("Jugador").GetComponent<Animator>().SetBool("LanzarPajarita",true);


        //Aumentamos el tamaño del collider de la pajarita
        BoxCollider colliderPajarita = GameObject.Find("Rigging_Mug_v1_1_ctl_pajarita").GetComponent<BoxCollider>();
        colliderPajarita.size = new Vector3(colliderPajarita.size.x, colliderPajarita.size.y, 3.0f);

    }

 

    public void chorro()
    {
        t_chorro = true;
        GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>().enabled = false;
        sprint = true;

        //gastamos energia
        Energia_Total.RestarEnergia(tablahabilidades[1].Energia_necesaria);
    }

    public void Explosion()
    {
        t_explosion = true;

        Vector3 SpawnPosition = new Vector3(0, 0, 0);
        SpawnPosition = this.transform.position;
        SpawnPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        GameObject Objeto = Instantiate(tablahabilidades[2].habilidad, SpawnPosition, Quaternion.identity);
        //GameObject.Find("A_Explosion").GetComponent<Button>().enabled = false;

        //GameObject ParticulasExplosionInstancia = Instantiate(ParticulasExplosion, transform.position, transform.rotation);
        //gastamos energia
        Energia_Total.RestarEnergia(tablahabilidades[2].Energia_necesaria);
    }

    public void escudo()
    {
        t_escudo = true;
        tablahabilidades[3].habilidad.SetActive(true);
        Invoke("escudooff",tiempoEscudo);
        GameObject.Find("Escudo").GetComponent<Button>().enabled = false;
        //gastamos energia
        Energia_Total.RestarEnergia(tablahabilidades[3].Energia_necesaria);
    }
    void escudooff()
    {
        tablahabilidades[3].habilidad.SetActive(false);
        GameObject.Find("Escudo").GetComponent<Button>().enabled = true;
    }

    void move()
    {
        GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>().enabled = true;
        sprint = false;
    }
}
