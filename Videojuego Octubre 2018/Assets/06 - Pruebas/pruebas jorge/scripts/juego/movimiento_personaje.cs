using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movimiento_personaje : MonoBehaviour
{

    protected Joystick joystick;
    //public float velocidad_inicial = 100f;
    public Slider vidas;
    float velocidad_fin;
    public GameObject Finpartida;
    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        joystick = FindObjectOfType<Joystick>();
        rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
       
    }

    // Update is called once per frame
    void Update ()
    {

        var rigidbody = GetComponent<Rigidbody>();
        velocidad_fin = vidas.value * 20;
        rigidbody.velocity = new Vector3(joystick.Horizontal * velocidad_fin,
                                         rigidbody.velocity.y,
                                         0);

        if(joystick.Horizontal<0)
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
            GetComponent<Animation>().Play("Take 001 (1)");
        }
        if (joystick.Horizontal>0)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            GetComponent<Animation>().Play("Take 001 (1)");
        }
        if (joystick.Horizontal==0)
        {
            transform.eulerAngles = new Vector3(0,-180, 0);
            GetComponent<Animation>().Play("Take 001");
        }

        if (vidas.value == vidas.minValue)
        {
            Finpartida.SetActive(true);
            GameObject.Find("C_Puntuacion").GetComponent<Ctrl_Puntuacion>().Puntuacion_final();
            GameObject.Find("creador_objetos").GetComponent<crar_objeto>().enabled = false;
            Destroy(GameObject.FindGameObjectWithTag("Enemigo"));
        }
    }

    public void Stop()
    {
        rb.velocity = new Vector3(0,0,0);
        GetComponent<Animation>().Play("Take 001");
    }
}
