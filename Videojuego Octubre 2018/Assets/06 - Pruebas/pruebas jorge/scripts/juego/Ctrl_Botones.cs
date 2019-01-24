using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ctrl_Botones : MonoBehaviour
{
    //public InputField Nombre;
    //public RankingManager Ranking;
	// Use this for initialization
	void Start ()
    {
		
	}
	


    public void volverJugar()
    {
        SceneManager.LoadScene(1);
    }

    public void pantallaAjustes()
    {
        SceneManager.LoadScene(2);
    }

    public void pantallaRanking()
    {
        SceneManager.LoadScene(3);
    }

    public void CargarMenu()
    {
        SceneManager.LoadScene(0);
    }

}
