using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_Recolectables : MonoBehaviour 
{
    public Vector3 RandomizePosition = new Vector3(0, 0, 0);
    public float CreationTimeMax;
    public float CreationTimeMin;
    public GameObject Objeto;
    public float tiempo;

    public GameObject ParticulasAparece;
    Window_IndicadorRecolectables Indicadores;

    movimiento_personaje Jugador;
    public bool SiguienteCaja = true;

    public GameObject MenuPremios;
    // Use this for initialization
    void Start () 
    {
        
        Indicadores = GameObject.Find("Window_recolectablePoint").GetComponent<Window_IndicadorRecolectables>();
        Jugador = GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>();
        TiempoSiguienteCaja();
    }
	
	// Update is called once per frame
	void Update () 
    {
        if(tiempo > 0)
        {
            tiempo -= Time.deltaTime;
        }
        else if(tiempo <= 0&&SiguienteCaja)
        {
            //print("sacamos caja");43-443
            Vector3 PosicionObjeto =new Vector3(0,0,0);
            int ladoFregadero = Random.Range(0,4);
            if(ladoFregadero<=1)
            { 
                PosicionObjeto = new Vector3(Random.Range(-700f, -306f), RandomizePosition.y , transform.position.z);
                GameObject Recolectable = Instantiate(Objeto, PosicionObjeto, Quaternion.identity, transform);
                Indicadores.Target = Recolectable;
                //print("posicion 1: " + PosicionObjeto);
            }
            if(ladoFregadero == 2)
            {
                PosicionObjeto = new Vector3(Random.Range(-306f,-70f), RandomizePosition.y, transform.position.z);
                GameObject Recolectable = Instantiate(Objeto, PosicionObjeto, Quaternion.identity, transform);
                Indicadores.Target = Recolectable;
                //print("posicion 2: " + PosicionObjeto);
            }
            if (ladoFregadero >= 3)
            {
                PosicionObjeto = new Vector3(Random.Range(50f, 455.0f), RandomizePosition.y, transform.position.z);
                GameObject Recolectable = Instantiate(Objeto, PosicionObjeto, Quaternion.identity, transform);
                Indicadores.Target = Recolectable;
                //print("posicion 3: " + PosicionObjeto);
            } 

            Vector3 PosicionParticulas = new Vector3(PosicionObjeto.x, -54f, transform.position.z);
            Instantiate(ParticulasAparece, PosicionParticulas, Quaternion.Euler(-90f,0,0));   
            SiguienteCaja=false;
        }
		
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, RandomizePosition*2);
    }

    public void TiempoSiguienteCaja()
    {
        if(Jugador.Vidas<5)
        {
           tiempo = CreationTimeMax-(((CreationTimeMax-CreationTimeMin)/4)*(5-Jugador.Vidas));
        }
        else if(Jugador.Vidas>=5)
        {
            tiempo = CreationTimeMax;
        }
        SiguienteCaja = true;
    }

    public void reducirTiempo()
    {
        if(Jugador.Vidas<5)
        {
            tiempo -= (CreationTimeMax-CreationTimeMin)/4;
        }
    }

    public void Premios()
    {
        MenuPremios.SetActive(true);
        GameObject.Find("Premio_Normal").GetComponent<Button>().enabled = true;

        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
        }
    }
}
