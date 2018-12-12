using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ctrl_Botones : MonoBehaviour
{
    public InputField Nombre;
    public RankingManager Ranking;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void volverJugar()
    {
        SceneManager.LoadScene("juego_def");
    }

    public void salir()
    {
        SceneManager.LoadScene("Menu");
    }

}
