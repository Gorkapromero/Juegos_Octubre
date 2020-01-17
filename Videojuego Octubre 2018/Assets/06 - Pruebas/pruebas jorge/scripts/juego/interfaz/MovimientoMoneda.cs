using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoMoneda : MonoBehaviour
{
    Transform Destino;
    public float speed;
    public Vector3 minScale;
    Vector3 maxScale;
    public float ScaleSpeed;
    float i = 0.0f;
    float rate;
    // Start is called before the first frame update
    void Start()
    {
        Destino = GameObject.Find("G_Monedas").GetComponent<Transform>();
        maxScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(i<1.0f)
        {
            i+=Time.deltaTime * ScaleSpeed;
            transform.localScale = Vector3.Lerp(maxScale,minScale,i);
        }
        transform.position = Vector3.MoveTowards(gameObject.transform.position,Destino.position,Time.deltaTime*speed);
        if(Vector3.Distance(gameObject.transform.position, Destino.position)<=0)
        {
            Destroy(gameObject);
        }
    }
}
