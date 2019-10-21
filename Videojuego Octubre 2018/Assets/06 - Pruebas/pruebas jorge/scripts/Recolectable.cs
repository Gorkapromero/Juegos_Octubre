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

    [System.Serializable]
    public class objeto
    {
        public Objetonombre Nombre = Objetonombre.Default;
        public GameObject Particulas;
        public int Porcentaje;
    }

    public objeto[] objetos;

    // Use this for initialization
    void Start()
    {
        Invoke("Destruir", DestroyTime);
        Jugador = GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>();
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
            
        }
    }

    void Destruir()
    {
        Destroy(gameObject);
        //Instantiate(ParticulasDestruccion, transform.position, Quaternion.identity);

    }

    void CogerObjeto()
    {
        int total = 0;
        for (int i = 0; i < objetos.Length; i++)
        {
            total += objetos[i].Porcentaje;
        }

        float randomObject = Random.value * total;

        for (int j = 0; j < objetos.Length; j++)
        {
            if (randomObject < objetos[j].Porcentaje)
            {
                switch(objetos[j].Nombre.ToString())
                {
                    case "Vida":
                        Jugador.Vidas++;
                        Jugador.ActualizarVidas();
                        Vector3 PosicionParticulas = new Vector3(transform.position.x, -24f, transform.position.z);
                        Instantiate(objetos[j].Particulas, PosicionParticulas, Quaternion.identity);
                        Destroy(gameObject);
                        break;
                    case "Daño":
                        break;
                    case "Default":
                        break;
                }
            }
        }
    }
}
