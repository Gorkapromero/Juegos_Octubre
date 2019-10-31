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
	public float Altura;
	public float Velocidad;
	public Vector3 OffSetPosFinal;

	// Use this for initialization
	IEnumerator Start () 
	{
		Personaje = GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>();
		Rb=GetComponent<Rigidbody>();
		jugador = GameObject.FindGameObjectWithTag("Jugador").transform;

		/*Vector3 moveDirection = (jugador.position-transform.position).normalized * FuerzaLanzamiento;
		Rb.velocity = new Vector3(moveDirection.x,FuerzaLanzamiento,0);*/
		while(true)
		{
			yield return StartCoroutine(ataque(jugador,Altura,Velocidad));
		}
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
			Vector3 PosPoff = new Vector3(transform.position.x, transform.position.y+15.8f, transform.position.z);
            GameObject poff = Instantiate(prefabPoff, PosPoff, Quaternion.identity);
            Destroy(this.gameObject);
			break;
		}
	}

	IEnumerator ataque(Transform objetivo,float altura,float velocidad)
	{
		Vector3 StartPos = transform.position;
		Vector3 endPos = objetivo.transform.position+OffSetPosFinal;
		float normalizedTime = 0.0f;
		while(normalizedTime < 1.0f)
		{
			float yoffset = altura * 4.0f * (normalizedTime - normalizedTime*normalizedTime);
			transform.position = Vector3.Lerp(StartPos,endPos,normalizedTime)+yoffset*Vector3.up;
			normalizedTime += Time.deltaTime/velocidad;
			yield return null;
		}
	}
}
