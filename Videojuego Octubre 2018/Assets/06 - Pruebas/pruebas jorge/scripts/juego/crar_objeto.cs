using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crar_objeto : MonoBehaviour
{
    public Transform[] posiciones;
    public GameObject Objetos;
    public float tiempoCreacion = 2f, RangoCreacion = 2f;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("crear", 0.0f, tiempoCreacion);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void crear()
    {
        int spawnPoint = Random.Range(0, posiciones.Length);
        //Vector3 SpawnPosition = new Vector3(0, 0, 0);
        //SpawnPosition = posiciones[0].transform.position;//this.transform.position + Random.onUnitSphere * RangoCreacion;
        //SpawnPosition = new Vector3(SpawnPosition.x, this.transform.position.y, 0);

        GameObject Objeto = Instantiate(Objetos, posiciones[spawnPoint].position, Quaternion.identity);
    }
}
