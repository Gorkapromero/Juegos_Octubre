using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_AExplosion : MonoBehaviour
{
    SphereCollider colisionador;

    public float tiempo;
    public float RadioMax;
	 


	// Use this for initialization
	void Start ()
    {
        colisionador = GetComponent<SphereCollider>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        float velocidad = 150 / tiempo;
        colisionador.radius += velocidad * Time.deltaTime;

        if(colisionador.radius >= RadioMax)
        {
            Destroy(this.gameObject);
        }
	}
}
