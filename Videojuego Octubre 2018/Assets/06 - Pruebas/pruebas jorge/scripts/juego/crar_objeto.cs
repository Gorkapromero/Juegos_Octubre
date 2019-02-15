using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crar_objeto : MonoBehaviour
{
    public Transform[] posiciones;
    //public GameObject Objetos;
    public float tiempoCreacion = 2f;// RangoCreacion = 2f;

	[System.Serializable]
    public class Enemigos
    {
        public string nombre;
        public GameObject enemigo;
        public int rareza;
    }

    public List<Enemigos> TablaEnemigos = new List<Enemigos>();
    float total = 0;

	void Start ()
    {
        //InvokeRepeating("crear", 0.0f, tiempoCreacion);
	}
	

    public void crear()
    {
        print("enemigo");
        total = 0;
        for (int i = 0; i < TablaEnemigos.Count; i++)
        {
            total += TablaEnemigos[i].rareza;
        }
        print("total " + total);

        float randomPoint = Random.value * total;

        for (int j = 0; j < TablaEnemigos.Count; j++)
        {
            if (randomPoint < TablaEnemigos[j].rareza)
            {
                int spawnPoint = Random.Range(0, posiciones.Length);
                GameObject Objeto = Instantiate(TablaEnemigos[j].enemigo, posiciones[spawnPoint].position, Quaternion.identity);
                return;
            }
            else
            {
                randomPoint -= TablaEnemigos[j].rareza;
            }
        }

    }
}
