using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_AtaquePegajoso : MonoBehaviour 
{
	Vector3 PosicionAtaque;
	public GameObject PrefabAtaque;
	public float FrecuenciaAtaque;

	float tiempo;

	Transform jugador;
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
		else
		{
			//instanciar ataque
			PosicionAtaque = new Vector3(transform.position.x, -34.9f, transform.position.z);
			Instantiate(PrefabAtaque,PosicionAtaque,Quaternion.identity);
			//reanudamos el tiempo
			tiempo=FrecuenciaAtaque;
		}
	}
}
