using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_Tutorial : MonoBehaviour
{
    public int faseActual = 1;
    public bool[] Fases;

    public GameObject MarcadorBoton;
    public Transform[] PosicionEnemigos;
    public GameObject particulasEnemigo;
    public GameObject particulasmuerte;
    public GameObject enemigo;
    public Text TextoInfo;
    public GameObject botonFinTutorial;

    // Start is called before the first frame update
    void Start()
    {
        ControlFase();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ControlFase()
    {
        /*if(Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
        }*/
        print(faseActual);
        switch(faseActual)
        {
            case 1: //movimiento
            //texto
            TextoInfo.text = "Utiliza las flechas de movimiento para desplazarte";
            //marcamos botones
            GameObject.Find("Canvas").GetComponent<Animator>().Play("Animboton"+faseActual);
            break;

            case 2: //salto+doble
            TextoInfo.text = "realiza un salto y un doble salto";
            //marcamos boton
            GameObject.Find("Canvas").GetComponent<Animator>().Play("Animboton"+faseActual);
            //activamos collider salto
            break;

            case 3: //Dash
            //texto
            TextoInfo.text = "utiliza el boton para moverte rapidamente hacia delante";
            //boton
            GameObject.Find("Canvas").GetComponent<Animator>().Play("Animboton"+faseActual);
            //particulas aparece enemigo
            Instantiate(particulasEnemigo,PosicionEnemigos[0].position,Quaternion.identity);
            //enemigo
            Instantiate(enemigo,PosicionEnemigos[0].position,Quaternion.identity);
            break;

            case 4: //Disparo
            TextoInfo.text = "elimina al enemigo lanzando tu pajarita";
            GameObject.Find("Canvas").GetComponent<Animator>().Play("Animboton"+faseActual);
            //particulas aparece enemigo
            Instantiate(particulasEnemigo,PosicionEnemigos[1].position,Quaternion.identity);
            //enemigo
            Instantiate(enemigo,PosicionEnemigos[1].position,Quaternion.identity);
            break;

            case 5: //escudo
            TextoInfo.text = "activa el escudo para protejerte";
            GameObject.Find("Elementos_Escenario").GetComponent<Enegia>().AñadirEnergia(1000);
            GameObject.Find("Canvas").GetComponent<Animator>().Play("Animboton"+faseActual);
            //particulas aparece enemigo
            Instantiate(particulasEnemigo,PosicionEnemigos[0].position,Quaternion.identity);
            //enemigo
            Instantiate(enemigo,PosicionEnemigos[0].position,Quaternion.identity);
            break;

            case 6: //explosion
            GameObject.Find("CTRL_Habilidades").GetComponent<Ctrl_Habilidades>().escudooff();
            GameObject.Find("Elementos_Escenario").GetComponent<Enegia>().Energia = 0;
            GameObject.Find("Elementos_Escenario").GetComponent<Enegia>().AñadirEnergia(1000);
            TextoInfo.text = "Elimina a todos los enemigos con el ataque especial";
            GameObject.Find("Canvas").GetComponent<Animator>().Play("Animboton"+faseActual);
            //particulas aparece enemigo
            Instantiate(particulasEnemigo,PosicionEnemigos[0].position,Quaternion.identity);
            //enemigo
            Instantiate(enemigo,PosicionEnemigos[0].position,Quaternion.identity);
            //particulas aparece enemigo
            Instantiate(particulasEnemigo,PosicionEnemigos[1].position,Quaternion.identity);
            //enemigo
            Instantiate(enemigo,PosicionEnemigos[1].position,Quaternion.identity);
            break;

            case 7: 
            GameObject.Find("Datosguardados").GetComponent<DatosGuardados>().tutorialCompletado = true;
            TextoInfo.text = "Enhorabuena! \n has completado el tutorial";
            //activar boton empezar partida
            botonFinTutorial.SetActive(true);
            break;
        }
    }

    /*completar fases:
        -fase 1: presionar boton
        -fase 2: entrar en triguer doble salto
        -fase 3: presionar boton
        -fase 4: matar enemigo
        -fase 5: boton
        -fase 6: boton
    */
        
    public void FaseCompletada(int fase)
    {
        if(Fases[fase-1]==true&&Fases[fase])
        {
            Fases[fase-1] = true;
            Invoke("SiguienteFase",2f);
        }
    }

    void SiguienteFase()
    {
        if(GameObject.FindGameObjectWithTag("Enemigo"))
        {
            Instantiate(particulasmuerte,GameObject.FindGameObjectWithTag("Enemigo").GetComponent<Transform>().position,Quaternion.identity);
            Destroy(GameObject.FindGameObjectWithTag("Enemigo"));
        }
        faseActual++;
        ControlFase();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Jugador")
        {
            FaseCompletada(2);
        }
    }

    public void EsperarRespawn()
    {
        Invoke("Respawn",1f);
    }
    void Respawn()
    {
        //particulas aparece enemigo
        Instantiate(particulasEnemigo,PosicionEnemigos[1].position,Quaternion.identity);
        //enemigo
        Instantiate(enemigo,PosicionEnemigos[1].position,Quaternion.identity);
    }
}
