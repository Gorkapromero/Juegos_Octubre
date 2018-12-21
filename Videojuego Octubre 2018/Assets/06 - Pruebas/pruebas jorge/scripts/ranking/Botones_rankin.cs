using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones_rankin : MonoBehaviour
{
    public GameObject warning;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void borrartodo()
    {
        warning.SetActive(true);
    }

    public void no()
    {
        warning.SetActive(false);
    }

    public void volver()
    {
        SceneManager.LoadScene("Menu");
    }

}
