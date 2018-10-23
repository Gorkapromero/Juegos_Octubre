using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colisiones_objetos : MonoBehaviour
{
    Slider vidas;

	// Use this for initialization
	void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider coli)
    {
        switch(coli.gameObject.name)                
        {
            case "suelo":                           //objeto toca suelo
                print("chocamos con "+coli);
                Destroy(this.gameObject);
                break;

            case "personaje":                       //objeto toca personaje
                print("quitamos vida");
                vidas = GameObject.Find("Vida").GetComponent<Slider>();
                vidas.value++;
                
                Destroy(this.gameObject);
                break;
        }
    }
}
