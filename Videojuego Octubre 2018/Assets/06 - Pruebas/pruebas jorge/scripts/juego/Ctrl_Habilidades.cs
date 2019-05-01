using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_Habilidades : MonoBehaviour
{
    public bool AtaqueBasico;
    

    [System.Serializable]
    public class Habilidades
    {
        public string nombre;
        public GameObject habilidad;
        public float T_enfriamiento;
        public GameObject tiempo;
        public Text tiempo_texto;
    }

    public List<Habilidades> tablahabilidades = new List<Habilidades>();

    public float tiempoEscudo;
    public GameObject tiempo;
    //public GameObject ParticulasExplosion; 
    public GameObject ParticulasSalpicadura;


    bool t_basico = false;
    bool t_chorro = false;
    bool t_escudo = false;
    bool t_explosion = false;

    float t1;
    float t2;
    float t3;
    float t0;




	// Use this for initialization
	void Start ()
    {
        t0 = tablahabilidades[0].T_enfriamiento;
        t1 = tablahabilidades[1].T_enfriamiento;
        t2 = tablahabilidades[2].T_enfriamiento;
        t3 = tablahabilidades[3].T_enfriamiento;
        tablahabilidades[1].tiempo.SetActive(false);
        tablahabilidades[2].tiempo.SetActive(false);
        tablahabilidades[3].tiempo.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (t_basico == true)
        {
            print("tiempo basico on");
            /*if (tablahabilidades[0].tiempo.activeSelf == false)
            {
                print("activamos");
                tablahabilidades[0].tiempo.SetActive(true);
            }
            tablahabilidades[1].tiempo_texto.text = t1.ToString("f0");*/
            t0 -= Time.deltaTime;
            //print(t1);
            if (t0 <= 0)
            {
                //t_chorro = false;
                t0 = tablahabilidades[0].T_enfriamiento;
                //tablahabilidades[0].tiempo.SetActive(false);
                GameObject.Find("A_Basico").GetComponent<Button>().enabled = true;
		
            }

        }
        if (t_chorro == true)
        {
            print("tiempo chorro on");
            if(tablahabilidades[1].tiempo.activeSelf == false)
            {
                print("activamos");
                tablahabilidades[1].tiempo.SetActive(true);
            }
            tablahabilidades[1].tiempo_texto.text = t1.ToString("f0");
            t1 -= Time.deltaTime;
            //print(t1);
            if (t1 <= 0)
            {
                t_chorro = false;
                t1 = tablahabilidades[1].T_enfriamiento;
                tablahabilidades[1].tiempo.SetActive(false);
                GameObject.Find("A_chorro").GetComponent<Button>().enabled = true;
            }
            
        }
        if (t_explosion == true)
        {
		 
            print("tiempo explosion on");
            if (tablahabilidades[2].tiempo.activeSelf == false)
            {
                print("activamos");
                tablahabilidades[2].tiempo.SetActive(true);

            }
            tablahabilidades[2].tiempo_texto.text = t2.ToString("f0");
            t2 -= Time.deltaTime;
            //print(t1);
            if (t2 <= 0)
            {
				
                t_explosion = false;
                t2 = tablahabilidades[2].T_enfriamiento;
                tablahabilidades[2].tiempo.SetActive(false);
                GameObject.Find("A_Explosion").GetComponent<Button>().enabled = true;


            }

        }

        if (t_escudo == true)
        {
            print("tiempo escudo on");
            if (tablahabilidades[3].tiempo.activeSelf == false)
            {
                print("activamos");
                tablahabilidades[3].tiempo.SetActive(true);
            }
            tablahabilidades[3].tiempo_texto.text = t3.ToString("f0");
            t3 -= Time.deltaTime;
            //print(t1);
            if (t3 <= 0)
            {
                t_escudo = false;
                t3 = tablahabilidades[1].T_enfriamiento;
                tablahabilidades[3].tiempo.SetActive(false);
                GameObject.Find("Escudo").GetComponent<Button>().enabled = true;
            }

        }
    }

    public void Ataque_Basico()
    {
        t_basico = true;
        AtaqueBasico = true;
        GameObject.Find("A_Basico").GetComponent<Button>().enabled = false;
    }

    public void chorro()
    {
        t_chorro = true;
        GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>().enabled = false;
        Vector3 SpawnPosition = new Vector3(0, 0, 0);
        SpawnPosition = this.transform.position;
        //SpawnPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        GameObject Objeto = Instantiate(tablahabilidades[1].habilidad, SpawnPosition, tablahabilidades[1].habilidad.transform.rotation);
        GameObject.Find("A_chorro").GetComponent<Button>().enabled = false;

        Invoke("move", 1f);
    }

    public void Explosion()
    {
        t_explosion = true;

        Vector3 SpawnPosition = new Vector3(0, 0, 0);
        SpawnPosition = this.transform.position;
        SpawnPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        GameObject Objeto = Instantiate(tablahabilidades[2].habilidad, SpawnPosition, Quaternion.identity);
        GameObject.Find("A_Explosion").GetComponent<Button>().enabled = false;

		//GameObject ParticulasExplosionInstancia = Instantiate(ParticulasExplosion, transform.position, transform.rotation);//
    }

    public void escudo()
    {
        t_escudo = true;
        tablahabilidades[3].habilidad.SetActive(true);
        Invoke("escudooff",tiempoEscudo);
        GameObject.Find("Escudo").GetComponent<Button>().enabled = false;
    }
    void escudooff()
    {
        tablahabilidades[3].habilidad.SetActive(false);
    }

    void move()
    {
        GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>().enabled = true;
    }
}
