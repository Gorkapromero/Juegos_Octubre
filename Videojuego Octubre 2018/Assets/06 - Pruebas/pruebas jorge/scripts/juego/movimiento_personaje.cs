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

    bool Saltando;
    bool salto;

    // Use this for initialization
    void Start ()
    {
        joystick = FindObjectOfType<Joystick>();
        rb = GetComponent<Rigidbody>();
        //var rigidbody = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update ()
    {

        
        velocidad_fin = vidas.value * 20;
        rb.velocity = new Vector3(joystick.Horizontal * velocidad_fin,
                                         rb.velocity.y,
                                         0);
        if (Saltando)
        {
            rb.velocity = new Vector3(joystick.Horizontal* vidas.value * 70, rb.velocity.y, 0);
        }

        //Para el movimiento
        GetComponent<Animator>().SetFloat("Speed", joystick.Horizontal);

        //Para el salto
        bool salto = Input.GetKeyDown(KeyCode.Space);
        
            GetComponent<Animator>().SetBool("Salto", salto);
        

        //Para la orientacion del Prota
        if (joystick.Horizontal<0)
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
            //GetComponent<Animator>().Play("Take 001 (1)");
        }
        if (joystick.Horizontal>0)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            //GetComponent<Animator>().Play("Take 001 (1)");
        }
        if (joystick.Horizontal==0)
        {
            transform.eulerAngles = new Vector3(0,-180, 0);
            //GetComponent<Animator>().Play("Take 001");
        }
        

        if (vidas.value == vidas.minValue)
        {
            Finpartida.SetActive(true);
            GameObject.Find("C_Puntuacion").GetComponent<Ctrl_Puntuacion>().Puntuacion_final();
            GameObject.Find("creador_objetos").GetComponent<crar_objeto>().enabled = false;
            Destroy(GameObject.FindGameObjectWithTag("Enemigo"));
        }
    }

    public void saltoAdelante()
    {
        Saltando = true;
        print("Saltando = TRUE");

    }

    public void finSaltoAdelante()
    {
        Saltando = false;
        print("Saltando = FALSE");
        rb.velocity = new Vector3(0, 0, 0);
        GetComponent<Animator>().SetBool("Salto", false);

    }

    public void Stop()
    {
        rb.velocity = new Vector3(0,0,0);
        GetComponent<Animator>().Play("Caida_Atras");
    }
}
