using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Premios : MonoBehaviour
{
    DatosGuardados datosGuardados;
    movimiento_personaje Jugador;
    Ctrl_Habilidades Habilidades;

    [System.Serializable]
    public class PremiosNormales
    {
        public string Nombre;
        public GameObject objeto;
        [Range(0,100)]
        public int Porcentaje;
    }

    public List <PremiosNormales> LootTableNormales = new List<PremiosNormales>();

    [System.Serializable]
    public class PremiosPremium
    {
        public string Nombre;
        public GameObject objeto;
        [Range(0,100)]
        public int Porcentaje;
        public bool x2;
    }

    public List <PremiosPremium> LootTablePremium = new List<PremiosPremium>();

    [Range(0,100)]
    public int dropChance;

    public int Monedas;

    public int VidasGanadas;
    public Text TextoVidasRecojidas;
    public int PowerUpsGanados;
    public Text TextoPowerRecojidos;

    public Transform Parent;
    // Start is called before the first frame update
    void Start()
    {
        datosGuardados = GameObject.Find("Datosguardados").GetComponent<DatosGuardados>();
        Jugador = GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>();
        Habilidades = GameObject.Find("CTRL_Habilidades").GetComponent<Ctrl_Habilidades>();

        ActualizarPremios();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NormalLoot()
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

            for(int i=0; i<LootTableNormales.Count;i++)
            {
                itemWeight += LootTableNormales[i].Porcentaje;
            }

            int randomValue = Random.Range(0, itemWeight);

            for(int j=0; j<LootTableNormales.Count;j++)
            {
                if(randomValue <= LootTableNormales[j].Porcentaje)
                {
                    Instantiate(LootTableNormales[j].objeto,Parent);
                    //sacamositem
                    switch(LootTableNormales[j].Nombre.ToString())
                    {
                        case "Vida":
                            //guardamos vida
                            VidasGanadas++;
                            ActualizarPremios();
                            break;
                        case "Daño":
                            //guardamos daño
                            PowerUpsGanados++;
                            ActualizarPremios();
                            break;
                        case "Monedas":
                            //guardamos monedas
                            datosGuardados.Monedas += Monedas;
                            break;
                        case "Default":
                            break;
                    }
                    return;
                }
                randomValue -= LootTableNormales[j].Porcentaje;

            }
        }
    }

    public void PremiumLoot()
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

            for(int i=0; i<LootTablePremium.Count;i++)
            {
                itemWeight += LootTablePremium[i].Porcentaje;
            }

            int randomValue = Random.Range(0, itemWeight);

            for(int j=0; j<LootTablePremium.Count;j++)
            {
                if(randomValue <= LootTablePremium[j].Porcentaje)
                {
                    //sacamositem
                    switch(LootTablePremium[j].Nombre.ToString())
                    {
                        case "Vida":
                            if (LootTablePremium[j].x2)
                            {
                                VidasGanadas += 2;
                            }
                            else
                            {
                                VidasGanadas++;
                            }
                            break;
                        case "Daño":
                        if (LootTablePremium[j].x2)
                            {
                                PowerUpsGanados += 2;
                            }
                            else
                            {
                                PowerUpsGanados++;
                            }
                            break;
                        case "Monedas":
                            if (LootTablePremium[j].x2)
                            {
                                datosGuardados.Monedas += Monedas*2;
                            }
                            else
                            {
                                datosGuardados.Monedas += Monedas;
                            }
                            break;
                        case "Default":
                            break;
                    }
                    return;
                }
                randomValue -= LootTablePremium[j].Porcentaje;

            }
        }
    }

    void ActualizarPremios()
    {
        TextoVidasRecojidas.text = VidasGanadas.ToString();
        TextoPowerRecojidos.text = PowerUpsGanados.ToString();
    }

    public void GastarVida()
    {
        if (Jugador.Vidas<5&&VidasGanadas>0)
        {
            VidasGanadas--;
            Jugador.GanarVida();
            ActualizarPremios();
        }
    }

    public void GastarPowerUp()
    {
        if (PowerUpsGanados>0)
        {
            PowerUpsGanados--;
            //aumentamos daño
            Habilidades.DañoBasico += 50;
            //cambiamos color pajarita
            //Habilidades.MaterialPajarita.color = ColorMasDaño;

            //Y activamos las partículas de "Prota super sayan"
            Habilidades.particulasSuperSayan_Cuerpo.SetActive(true);
            Habilidades.particulasSuperSayan_Pajarita.SetActive(true);

            //indicamos que hemos conseguido mas daño
            //Vector3 PosicionParticulasDaño = new Vector3(transform.position.x, -54f, transform.position.z);
            //Instantiate(particulasDaño, PosicionParticulasDaño, Quaternion.Euler(-90f,0,0));
            //activamos tiempo daño extra
            Habilidades.DañoExtra = true;
            ActualizarPremios();
        }
    }
}
