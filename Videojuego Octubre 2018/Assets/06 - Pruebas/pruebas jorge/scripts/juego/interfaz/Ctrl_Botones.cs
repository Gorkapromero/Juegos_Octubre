using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ctrl_Botones : MonoBehaviour
{
    //public InputField Nombre;
    //public RankingManager Ranking;
    Ctrl_Puntuacion Puntuacion;

    public static bool GamePaused = false;

    public GameObject pauseMenuUI;
	// Use this for initialization
	void Start ()
    {
        Puntuacion = GameObject.Find("C_Puntuacion").GetComponent<Ctrl_Puntuacion>();
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

    public void pausa()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }
}
