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
	//public float AlturaMax;
	public float Velocidad;
	//public Vector3 distancia;

	// Use this for initialization
	void Start () 
	{
		Personaje = GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>();
		Rb=GetComponent<Rigidbody>();
		jugador = GameObject.FindGameObjectWithTag("Jugador").transform;

		/*Vector3 moveDirection = (jugador.position-transform.position).normalized * FuerzaLanzamiento;
		Rb.velocity = new Vector3(moveDirection.x,FuerzaLanzamiento,0);*/
		/*while(true)
		{
			yield return StartCoroutine(ataque(jugador.position,transform.position,AlturaMax,Velocidad));
		}*/
		Vector3 Vo = CalculateVelocity(jugador.position,transform.position, Velocidad);
		Rb.velocity = Vo;
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
			Vector3 PosPoff = new Vector3(transform.position.x, transform.position.y+3.2f, transform.position.z);
            GameObject poff = Instantiate(prefabPoff, PosPoff, Quaternion.identity);
            Destroy(this.gameObject);
			break;
		}
	}

	IEnumerator ataque(Vector3 objetivo,Vector3 origen,float altura,float velocidad)
	{
		Vector3 StartPos = transform.position;
		Vector3 endPos = objetivo;
		float Dist = Vector3.Distance(StartPos,endPos);
		float _altura = velocidad*(Dist/velocidad)-0.5f*Mathf.Abs(Physics.gravity.y)*((Dist/velocidad)*(Dist/velocidad));
		float normalizedTime = 0.0f;
		while(normalizedTime < 1.0f)
		{
			
			Vector3 distancia = objetivo - origen;
			float DistY = distancia.y;
			float DistXZ = distancia.magnitude;
			float tiempo = DistXZ/velocidad;
			float yoffset = _altura * 4.0f * (normalizedTime - normalizedTime*normalizedTime);
			transform.position = Vector3.Lerp(StartPos,endPos,normalizedTime)+yoffset*Vector3.up;
			
			normalizedTime += Time.deltaTime/(DistXZ/velocidad);
			yield return null;
		}
	}

	Vector3 CalculateVelocity(Vector3 objetivo, Vector3 origen, float _velocidad)
	{
		//definimos destancia x e y
		Vector3 distancia = objetivo - origen;
		Vector3 distanciaXZ = distancia;
		distanciaXZ.y = 0f;

		//creamos float representado la siatncia
		float Sy = distancia.y;
		
		float Sxz = distanciaXZ.magnitude;
		float tiempo = Sxz/_velocidad;

		float Vxz = Sxz / tiempo;
		float Vy = Sy/tiempo + 0.5f * Mathf.Abs(Physics.gravity.y)*tiempo;

		Vector3 result = distanciaXZ.normalized;
		result *= Vxz;
		result.y = Vy;

		return result;
	}
}
