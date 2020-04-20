using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ctrl_AtaquePegajoso : MonoBehaviour 
{
	Vector3 PosicionAtaque;
	public GameObject PrefabAtaque;
	public float FrecuenciaAtaque;

	float tiempo;
    public float tiempoCargaAtaque;

    Transform jugador;

	public bool PersonajeVisto;
	NavMeshAgent agente;

	public bool DañoRecibido;

    // Use this for initialization
	void Start () 
	{
		jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
		tiempo = FrecuenciaAtaque;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(tiempo>0)
		{
			tiempo -= Time.deltaTime;
		}

		else if(tiempo<=0&&PersonajeVisto)
		{

            Invoke("lanzarPelota", tiempoCargaAtaque);

            //Paramos al personaje desde el rigidbody
            gameObject.GetComponent<NavMeshAgent>().speed = 0;

            tiempo = FrecuenciaAtaque;
            gameObject.GetComponent<Ctrl_AtaquePegajoso>().PersonajeVisto = false;

            //Ejecutamos la animacion de "cargar ataque"
                    //"Faltaría hacer una animacion"
        }
    }

    void lanzarPelota()
    {
		if(!DañoRecibido)
		{ 
			//instanciar ataque
			PosicionAtaque = new Vector3(transform.position.x, -34.9f, transform.position.z);
			Instantiate(PrefabAtaque, PosicionAtaque, Quaternion.identity);

			//reanudamos el tiempo y volvemos a hacer andar al personaje
			gameObject.GetComponent<NavMeshAgent>().speed = 15;

		}
		else
		{
			DañoRecibido = false;
		}
    }
}
