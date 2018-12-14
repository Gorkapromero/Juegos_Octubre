using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_AChorro : MonoBehaviour {

    public float Velocidad;
    public GameObject chorro;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.forward* (Velocidad*5) * Time.deltaTime;
        transform.localScale += transform.up * (-Velocidad) * Time.deltaTime;
        //transform.position += transform.up * (Velocidad * 5) * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider coli)
    {
        print(coli.gameObject.name);
        switch (coli.gameObject.name)
        {
            case "Top":                           //ataque llega al techo
                print("chocamos con " + coli);
                Destroy(this.gameObject);
                break;

            /*case "objeto(Clone)":
                print("desruimos enemigo");

                Destroy(this.gameObject);
                break;*/

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
    }

}
