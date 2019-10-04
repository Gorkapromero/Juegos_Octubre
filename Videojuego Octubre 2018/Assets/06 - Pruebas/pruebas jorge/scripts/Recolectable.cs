using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolectable : MonoBehaviour {
    public float DestroyTime;
    public float Velocidad;
    //public GameObject ParticulasDestruccion;
    public GameObject ParticulasVida;

    movimiento_personaje Jugador;

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
            Jugador.Vidas++;
            Jugador.ActualizarVidas();
            Vector3 PosicionParticulas = new Vector3(transform.position.x, -24f, transform.position.z);
            Instantiate(ParticulasVida, PosicionParticulas, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Destruir()
    {
        Destroy(gameObject);
        //Instantiate(ParticulasDestruccion, transform.position, Quaternion.identity);

    }
}
