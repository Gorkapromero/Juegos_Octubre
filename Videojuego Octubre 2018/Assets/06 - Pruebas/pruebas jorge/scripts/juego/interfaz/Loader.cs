﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressTxt;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
	
    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(operation.progress);

            slider.value = progress;
            progressTxt.text = (progress * 100f).ToString("f0") + "%";

            yield return null;
        }
    }

    public void MenuSkins()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().Play("AbrirSkins");
        GameObject.Find("GrupoAnimacion").GetComponent<Animator>().Play("VerSkins");

        GameObject.Find("gpr_estrellas").GetComponent<Animator>().Play("Standby");

        //Invoke("cargarSkin",0.3f);
    }
    public void abrirRateUs()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().Play("anim_RateUs");
        GameObject.Find("gpr_estrellas").GetComponent<Animator>().Play("animator_estrellas_rateUs");
        //GameObject.Find("GrupoAnimacion").GetComponent<Animator>().Play("VerSkins");
        //Invoke("cargarSkin",0.3f);
    }

    public void VolverMenu()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().Play("VolverMenu");
        GameObject.Find("GrupoAnimacion").GetComponent<Animator>().Play("CerrarSkins");
        GameObject.Find("Datosguardados").GetComponent<DatosGuardados>().Save();
    }

    void cargarSkin()
    {
        GameObject.Find("Skins").GetComponent<Ctrl_Skins>().buscarSkin();
    }

    public void IrTienda()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().Play("AbrirTienda");
        GameObject.Find("GrupoAnimacion").GetComponent<Animator>().Play("VerTienda");
    }
    public void volverSkins()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().Play("CerrarTienda");
        GameObject.Find("GrupoAnimacion").GetComponent<Animator>().Play("VolverSkins");
    }
}
