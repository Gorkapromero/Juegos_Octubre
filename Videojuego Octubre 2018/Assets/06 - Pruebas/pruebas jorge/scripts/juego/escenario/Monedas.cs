using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monedas : MonoBehaviour 
{
	DatosGuardados DatosGuardar;
	Rigidbody RBMoneda;

	public float FuerzaExplosion;

	// Use this for initialization
	void Start () 
	{
		DatosGuardar=GameObject.Find("Datosguardados").GetComponent<DatosGuardados>();

		Vector3 Vo = new Vector3(Random.Range(-FuerzaExplosion,FuerzaExplosion),FuerzaExplosion*20f,0);
		RBMoneda.velocity = Vo;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision other) 
	{
		print("colision");
		if(other.gameObject.tag == "Jugador")
		{
			//sumamos moneda
			DatosGuardar.Monedas++;
			//destruimos moneda
			Destroy(gameObject);
		}
	}
}
