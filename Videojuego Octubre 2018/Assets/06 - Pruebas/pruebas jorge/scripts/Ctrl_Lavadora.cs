using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Lavadora : MonoBehaviour
{
    float TiempoLavadora = 5f;
    public float TiempoActivacion = 5f;
    public float TiempoMinimo;
    public float TiempoMax;
    public bool LavadoraActivada;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (TiempoActivacion > 0)
        {
            TiempoActivacion -= Time.deltaTime;
        }
        else if (!LavadoraActivada && TiempoActivacion <= 0)
        {
            ActivarLavadora();
            LavadoraActivada = true;
        }
        if (TiempoLavadora <= 0)
        {
            TiempoLavadora = 5.0f;
            GetComponent<Animator>().Play("Reposo");
            //.Play("Anim_FuegoOff");
            TiempoActivacion = Random.Range(TiempoMinimo, TiempoMax);
            LavadoraActivada = false;
        }
        else if (LavadoraActivada)
        {
            TiempoLavadora -= Time.deltaTime;
        }
    }

    void ActivarLavadora()
    {
        GetComponent<Animator>().Play("Anim_Lavadora");
    }
}
