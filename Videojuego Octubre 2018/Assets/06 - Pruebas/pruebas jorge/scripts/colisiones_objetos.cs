using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisiones_objetos : MonoBehaviour {

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
                break;
        }
    }
}
