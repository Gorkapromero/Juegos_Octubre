using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Anim_Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void _ActivarAnim()
    {
        StartCoroutine(AnimMenuTaza());
    }


    IEnumerator AnimMenuTaza()
    {
        yield return new WaitForSeconds(1.7f);
        GameObject.Find("Taza").GetComponent<Animator>().Play("Caida_Atras 0");
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("Taza").GetComponent<Animator>().CrossFade("Movimiento",0.5f);
    }     
}
