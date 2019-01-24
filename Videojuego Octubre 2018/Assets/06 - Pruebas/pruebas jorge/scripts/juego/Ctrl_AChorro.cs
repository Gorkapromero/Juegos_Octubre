using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_AChorro : MonoBehaviour {

    //public float Velocidad;
    //public GameObject chorro;
    float y;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<BoxCollider>().size = new Vector3(2, transform.localScale.y,20);
        y = (gameObject.GetComponent<BoxCollider>().size.y - 1) / 2;
        gameObject.GetComponent<BoxCollider>().center = new Vector3(0, y, -6);

    }

    private void OnTriggerEnter(Collider coli)
    {
        print(coli.gameObject.name);
        switch (coli.gameObject.name)
        {
            /*case "Top":                           //ataque llega al techo
                print("chocamos con " + coli);
                GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>().enabled = true;
                Destroy(this.gameObject);
                break;*/

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
