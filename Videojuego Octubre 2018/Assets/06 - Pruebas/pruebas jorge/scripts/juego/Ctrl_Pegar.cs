using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Pegar : MonoBehaviour
{
    movimiento_personaje move;
    public float TiempoPeg;
	// Use this for initialization
	void Start ()
    {
		move = GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Jugador":                       //personaje toca pegajoso
                Invoke("Pegar", 0.2f);
                break;
        }
    }

    void Despegar()
    {
        Destroy(this.gameObject);
        move.enabled = true;
    }

    void Pegar()
    {
        print("Nos quedamos pegados");
        move.Stop();
        move.enabled = false;
        Invoke("Despegar", TiempoPeg);
    }
}
