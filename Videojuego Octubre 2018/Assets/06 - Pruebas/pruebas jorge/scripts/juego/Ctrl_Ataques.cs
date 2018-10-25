using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Ataques : MonoBehaviour
{

    public GameObject A_Basico;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Ataque_Basico()
    {
        Vector3 SpawnPosition = new Vector3(0, 0, 0);
        SpawnPosition = this.transform.position;
        SpawnPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        GameObject Objeto = Instantiate(A_Basico, SpawnPosition, Quaternion.identity);
    }
}
