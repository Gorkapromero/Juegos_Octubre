using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Objetonombre
{
    Default,
    Vida,
    Daño
}
public class Recolectable : MonoBehaviour {
    public float DestroyTime;
    public float Velocidad;
    //public GameObject ParticulasDestruccion;
    //public GameObject ParticulasVida;

    movimiento_personaje Jugador;
    Ctrl_Habilidades Habilidades;
    Ctrl_Recolectables Recolectables;

    [System.Serializable]
    public class objeto
    {
        public Objetonombre Nombre = Objetonombre.Default;
        public GameObject Particulas;
        [Range(0,100)]
        public int Porcentaje;
    }

    public List <objeto> LootTable = new List<objeto>();
    [Range(0,100)]
    public int dropChance;

    public Color ColorMasDaño;

    //public GameObject MenuPremios;

    // Use this for initialization
    void Start()
    {
        if(DestroyTime>0)
        {
            Invoke("Destruir", DestroyTime);
        }
            
        Jugador = GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>();
        Habilidades = GameObject.Find("CTRL_Habilidades").GetComponent<Ctrl_Habilidades>();
        Recolectables = GameObject.Find("Recolctables").GetComponent<Ctrl_Recolectables>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Jugador")
        {
            if(gameObject.name == "Recolectable_Caja")
            {
                claculateLoot();
            }
            else
            {
                premio();
                Destroy(gameObject);
            }
        }
    }

    void Destruir()
    {
        Recolectables.TiempoSiguienteCaja();
        Destroy(gameObject);
        
        //Instantiate(ParticulasDestruccion, transform.position, Quaternion.identity);

    }

    void claculateLoot()
    {
        int calc_dropChance = Random.Range(0,101);

        if(calc_dropChance > dropChance)
        {
            //no Loot
            return;
        }

        if(calc_dropChance <= dropChance)//looteamos
        {
            int itemWeight = 0;

            for(int i=0; i<LootTable.Count;i++)
            {
                itemWeight += LootTable[i].Porcentaje;
            }

            int randomValue = Random.Range(0, itemWeight);

            for(int j=0; j<LootTable.Count;j++)
            {
                if(randomValue <= LootTable[j].Porcentaje)
                {
                    //sacamositem
                    switch(LootTable[j].Nombre.ToString())
                    {
                        case "Vida":
                            Jugador.GanarVida();
                            Vector3 PosicionParticulasVida = new Vector3(transform.position.x, -24f, transform.position.z);
                            Instantiate(LootTable[j].Particulas, PosicionParticulasVida, Quaternion.identity);
                            Destruir();
                            break;
                        case "Daño":
                            //aumentamos daño
                            Habilidades.DañoBasico += 50;
                            //cambiamos color pajarita
                            Habilidades.MaterialPajarita.color = ColorMasDaño;

                            //Y activamos las partículas de "Prota super sayan"
                            Habilidades.particulasSuperSayan_Cuerpo.SetActive(true);
                            Habilidades.particulasSuperSayan_Pajarita.SetActive(true);

                            //indicamos que hemos conseguido mas daño
                            Vector3 PosicionParticulasDaño = new Vector3(transform.position.x, -54f, transform.position.z);
                            Instantiate(LootTable[j].Particulas, PosicionParticulasDaño, Quaternion.Euler(-90f,0,0));
                            //activamos tiempo daño extra
                            Habilidades.DañoExtra = true;
                            Destruir();
                            break;
                        case "Default":
                            break;
                    }
                    return;
                }
                randomValue -= LootTable[j].Porcentaje;

            }
        }

    }

    void premio()
    {
        Recolectables.Premios();
    }

    void DañoExtra()
    {
        
    }
}
