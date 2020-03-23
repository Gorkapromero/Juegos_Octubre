using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monedas : MonoBehaviour 
{
	DatosGuardados DatosGuardar;
	Ctrl_Puntuacion Puntuacion;
	Rigidbody RBMoneda;

	public float FuerzaExplosion;
    public GameObject particulasRecogerMoneda;
	public float TiempoDestruir;

    AudioSource audioMoneda;

	// Use this for initialization
	void Start () 
	{
		DatosGuardar=GameObject.Find("Datosguardados").GetComponent<DatosGuardados>();
		Puntuacion = GameObject.Find("C_Puntuacion").GetComponent<Ctrl_Puntuacion>();
        
		Vector3 Vo = new Vector3(Random.Range(-FuerzaExplosion,FuerzaExplosion),FuerzaExplosion*3f,0);
		//RBMoneda.velocity = Vo;
        gameObject.GetComponent<Rigidbody>().AddForce(Vo, ForceMode.Impulse);

        audioMoneda = GameObject.Find("SonidoMoneda").GetComponent<AudioSource>();

		Invoke("hacerCollider",0.4f);
		Invoke("DestruirMoneda", TiempoDestruir);
    }

 
	private void OnCollisionEnter(Collision other) 
	{
		//print("colision");
		if(other.gameObject.tag == "Jugador")
		{
			//sumamos moneda
			//DatosGuardar.Monedas++;

			Puntuacion.ActualizarMonedas();

            //Ejecutamos la animacion de partículas correspondiente
            GameObject ParticulasRecogerMoneda = Instantiate(particulasRecogerMoneda, transform.position, Quaternion.identity);

            //Y hacemos sonar el sonido de "recogerMoneda"
            audioMoneda.Play();

            //destruimos moneda
            Destroy(gameObject);
		}
	}

	void hacerCollider()
	{
		gameObject.GetComponent<Collider>().isTrigger = false;
	}

	void DestruirMoneda()
	{
		Destroy(gameObject);
	}
}
