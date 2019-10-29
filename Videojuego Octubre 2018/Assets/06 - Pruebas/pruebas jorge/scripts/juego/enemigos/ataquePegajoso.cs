using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ataquePegajoso : MonoBehaviour 
{
	public float FuerzaLanzamiento;

	Rigidbody Rb;

	movimiento_personaje Personaje;
	Transform jugador;

	public GameObject prefabPoff;

	// Use this for initialization
	void Start () 
	{
		Personaje = GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>();
		Rb=GetComponent<Rigidbody>();
		jugador = GameObject.FindGameObjectWithTag("Jugador").transform;

		Vector3 moveDirection = (jugador.position-transform.position).normalized * FuerzaLanzamiento;
		Rb.velocity = new Vector3(moveDirection.x,FuerzaLanzamiento,0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) 
	{
		switch(other.gameObject.tag)
		{
			case "Jugador":
			Personaje.quitarvida_Vida();
			Destroy(this.gameObject);
			break;

			case "Suelo":
			Vector3 PosPoff = new Vector3(transform.position.x, transform.position.y+3.62f, transform.position.z);
            GameObject poff = Instantiate(prefabPoff, PosPoff, Quaternion.identity);
            Destroy(this.gameObject);
			break;
		}
	}
}
