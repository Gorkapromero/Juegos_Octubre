using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movimiento_personaje : MonoBehaviour
{

    protected Joystick joystick;
    public float velocidad_inicial = 100f;
    public Slider vidas;
    public float velocidad_fin;
    public GameObject Finpartida;

	// Use this for initialization
	void Start ()
    {
        joystick = FindObjectOfType<Joystick>();

	}

    private void Awake()
    {
       
    }

    // Update is called once per frame
    void Update ()
    {
        var rigidbody = GetComponent<Rigidbody>();

        velocidad_fin = velocidad_inicial - (vidas.value * 20);
        rigidbody.velocity = new Vector3(joystick.Horizontal * velocidad_fin,
                                         rigidbody.velocity.y,
                                         0);

        if (vidas.value == vidas.maxValue)
        {
            Finpartida.SetActive(true);
            GameObject.Find("creador_objetos").GetComponent<crar_objeto>().enabled = false;
            Destroy(GameObject.FindGameObjectWithTag("Enemigo"));
        }
    }
}
