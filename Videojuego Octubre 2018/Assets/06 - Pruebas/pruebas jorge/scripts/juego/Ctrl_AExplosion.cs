using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_AExplosion : MonoBehaviour
{
    SphereCollider colisionador;

    public float velocidad;
	// Use this for initialization
	void Start ()
    {
        colisionador = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //colisionador.radius += velocidad * Time.deltaTime;
        //transform.localScale += transform.localScale * velocidad * Time.deltaTime;
	}

    private void OnTriggerEnter(Collider coli)
    {
        print(coli.gameObject.name);
        switch (coli.gameObject.name)
        {
            case "Top":                           //objeto toca techo
                print("chocamos con " + coli);
                Destroy(this.gameObject);
                break;

        }
    }
}
