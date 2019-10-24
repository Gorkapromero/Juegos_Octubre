using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Recolectables : MonoBehaviour 
{
    public Vector3 RandomizePosition = new Vector3(0, 0, 0);
    public float CreationTime;
    public GameObject Objeto;
    float tiempo;

    public GameObject ParticulasAparece;
    Window_IndicadorRecolectables Indicadores;
    
    movimiento_personaje Jugador;

    // Use this for initialization
    void Start () 
    {
        tiempo = CreationTime;
        Indicadores = GameObject.Find("Window_recolectablePoint").GetComponent<Window_IndicadorRecolectables>();
        Jugador= GameObject.FindGameObjectWithTag("Jugador").GetComponent<movimiento_personaje>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        if(tiempo > 0)
        {
            tiempo -= Time.deltaTime;
        }
        else if(tiempo <= 0)
        {
            Vector3 PosicionObjeto = new Vector3(Random.Range(-RandomizePosition.x, RandomizePosition.x), RandomizePosition.y , transform.position.z);
            GameObject Recolectable = Instantiate(Objeto, PosicionObjeto, Quaternion.identity, transform);
            Indicadores.Target = Recolectable;

            Vector3 PosicionParticulas = new Vector3(PosicionObjeto.x, -54f, transform.position.z);
            Instantiate(ParticulasAparece, PosicionParticulas, Quaternion.Euler(-90f,0,0));
            
            if(Jugador.Vidas<5)
            {
                
            }
            tiempo = CreationTime;
        }
		
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, RandomizePosition*2);
    }
}
