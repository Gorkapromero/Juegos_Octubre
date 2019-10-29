using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 3f;
    public Vector3 offset = new Vector3(0, 2, 0);
    public Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 0);
    public float Velocidad;

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, DestroyTime);

        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
            Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
            Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position += transform.up * Velocidad * Time.deltaTime;
    }
}
