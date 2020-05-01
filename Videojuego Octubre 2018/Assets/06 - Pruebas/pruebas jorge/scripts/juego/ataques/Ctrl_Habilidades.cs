using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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

    //public float tiempoEscudo;
    //public GameObject tiempo;
    //public GameObject ParticulasExplosion; 
    public GameObject ParticulasSalpicadura;

    public int DañoBasico;

    public bool escudoActivado = false;
    Enegia Energia_Total;

    public float FuerzaSprint;
    public bool sprint;
    Rigidbody RBplayer;
    GameObject Player;

    protected Joystick joystick;

    public Material MaterialPajarita;
    Color ColorPajarita;
    //VariablesSonido
    AudioSource sonidoDisparo;

    public float tiempoDañoExtra;
    public bool DañoExtra;
    float tiempo;

    BoxCollider colliderPajarita;

    public GameObject particulasSuperSayan_Cuerpo;
    public GameObject particulasSuperSayan_Pajarita;
    public GameObject particulasBarraEnergia;


    // Use this for initialization
    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Jugador");
        RBplayer = Player.GetComponent<Rigidbody>();
        Energia_Total = GameObject.Find("Elementos_Escenario").GetComponent<Enegia>();
        ColorPajarita = MaterialPajarita.color;
        //joystick = FindObjectOfType<Joystick>();
        /*t0 = tablahabilidades[0].T_enfriamiento;
        t1 = tablahabilidades[1].T_enfriamiento;
        t2 = tablahabilidades[2].T_enfriamiento;
        t3 = tablahabilidades[3].T_enfriamiento;*/
        tablahabilidades[1].tiempo.SetActive(true);
        tablahabilidades[2].tiempo.SetActive(true);
        tablahabilidades[3].tiempo.SetActive(true);

        tiempo=tiempoDañoExtra;

        //VariablesSonido
        sonidoDisparo = GameObject.Find("SonidoDisparo").GetComponent<AudioSource>();

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
        //ACTIVAMOS CHORRO SI TENEMOS ENERGIA
        if (tablahabilidades[1].Energia_necesaria <= Energia_Total.Energia)
        {
            //print("tiempo chorro on");
            if(tablahabilidades[1].tiempo.activeSelf == true)
            {
                //print("activamos sprint");
                tablahabilidades[1].tiempo.SetActive(false);
                tablahabilidades[1].Boton.enabled = true;
            }           
        }
        else
        {
            tablahabilidades[1].tiempo.SetActive(true);
            tablahabilidades[1].Boton.enabled = false;
        }


        //ACTIVAMOS EXPLOSION SI TENEMOS ENERGIA
        if (tablahabilidades[2].Energia_necesaria <= Energia_Total.Energia)
        {
		 
            //print("tiempo explosion on");
            if (tablahabilidades[2].tiempo.activeSelf == true)
            {
                //print("activamos");
                tablahabilidades[2].tiempo.SetActive(false);
                tablahabilidades[2].Boton.enabled = true;

            }

        }
        else
        {
            tablahabilidades[2].tiempo.SetActive(true);
            tablahabilidades[2].Boton.enabled = false;
        }

        //ACTIVAMOS ESCUDO SI TENEMOS ENERGIA
        if (tablahabilidades[3].Energia_necesaria <= Energia_Total.Energia)
        {
            //print("tiempo escudo on");
            if (tablahabilidades[3].tiempo.activeSelf == true)
            {
                //print("activamos");
                tablahabilidades[3].tiempo.SetActive(false);
                tablahabilidades[3].Boton.enabled = true;
            }

        }
        else
        {
            tablahabilidades[3].tiempo.SetActive(true);
            tablahabilidades[3].Boton.enabled = false;
        }

        if(sprint)
        {
            if(Player.GetComponent<movimiento_personaje>().DireccionProta == -1)
            {
                //print("saltamos izquierda");
                RBplayer.velocity = Vector3.right * -FuerzaSprint;  
            }
            else if (Player.GetComponent<movimiento_personaje>().DireccionProta == 1)
            {
                //print("saltamos derecha");
                RBplayer.velocity = Vector3.right * FuerzaSprint;
            }
            GameObject.FindGameObjectWithTag("Jugador").GetComponent<Animator>().Play("Salto+Adelante");
            Invoke("move", 0.5f);
        }

        if(DañoExtra)
        {
            if(tiempo>0)
            {
                tiempo -= Time.deltaTime;
            }
            else
            {
                quitarDañoExtra();
            }
        }
    }

    public void Ataque_Basico()
    {
        //Reproducimos el sonido del disparo
        sonidoDisparo.Play();

        //t_basico = true;
        AtaqueBasico = true;
        GameObject.FindGameObjectWithTag("Jugador").GetComponent<Animator>().SetBool("LanzarPajarita",true);

        //Aumentamos el tamaño del collider de la pajarita
        colliderPajarita = GameObject.Find("Rigging_Mug_v1_1_ctl_pajarita").GetComponent<BoxCollider>();
        colliderPajarita.size = new Vector3(colliderPajarita.size.x, colliderPajarita.size.y, 3.0f);

        Invoke("ActivarColliderPajarita", 0.2f);

        
    }
    void ActivarColliderPajarita()
    {

        colliderPajarita.enabled = true;
    }


    public void chorro()
    {
        //t_chorro = true;
        GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>().enabled = false;
        sprint = true;

        //gastamos energia
        Energia_Total.RestarEnergia(tablahabilidades[1].Energia_necesaria);
    }

    public void Explosion()
    {
        //t_explosion = true;

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
        if(escudoActivado==false)
        {
            if(GameObject.Find("Analytics"))
                GameObject.Find("Analytics").GetComponent<AnaliticsEvents>().AnalyticsHabilidades("Escudo On");
            escudoActivado = true;
            tablahabilidades[3].habilidad.SetActive(true);
            particulasBarraEnergia.SetActive(true);
        }
        else
        {
            if (GameObject.Find("Analytics"))
                GameObject.Find("Analytics").GetComponent<AnaliticsEvents>().AnalyticsHabilidades("Escudo Off");
            escudooff();
        }

        
    }
    public void escudooff()
    {
        escudoActivado = false;
        tablahabilidades[3].habilidad.SetActive(false);
        GameObject.Find("Escudo").GetComponent<Button>().enabled = true;
        particulasBarraEnergia.SetActive(false);
        Energia_Total.EnergiaFinal = Energia_Total.Energia;
        if(SceneManager.GetActiveScene().name == "02_escenario_tutorial")
        {
            Time.timeScale = 1;
            GameObject.Find("Control_Tutorial").GetComponent<Ctrl_Tutorial>().FaseCompletada(5);
        }
    }

    void move()
    {
        GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>().enabled = true;
        sprint = false;
    }

    public void quitarDañoExtra()
    {
        DañoExtra=false;
        DañoBasico = 100;
        MaterialPajarita.color = ColorPajarita;
        tiempo=tiempoDañoExtra;

        //Desactivamos las particulas del Prota
        particulasSuperSayan_Cuerpo.SetActive(false);
        particulasSuperSayan_Pajarita.SetActive(false);
    }
}
