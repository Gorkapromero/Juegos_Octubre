using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_ABasico : MonoBehaviour
{
    public float Velocidad = 500f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.up * Velocidad * Time.deltaTime;
	}

    private void OnTriggerEnter(Collider coli)
    {
        switch (coli.gameObject.name)
        {
            case "Top":                           //objeto toca suelo
                print("chocamos con " + coli);
                Destroy(this.gameObject);
                break;

            case "objeto(Clone)":
                print("desruimos enemigo");
                
                Destroy(this.gameObject);
                break;

        }
    }
}
