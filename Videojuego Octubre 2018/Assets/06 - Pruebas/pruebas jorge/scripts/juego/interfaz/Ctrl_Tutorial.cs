using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_Tutorial : MonoBehaviour
{
    public int faseActual = 1;
    public bool[] Fases;

    public Transform[] PosicionEnemigos;
    public GameObject particulasEnemigo;
    public GameObject particulasmuerte;
    public GameObject enemigo;
    public GameObject enemigo2;
    public GameObject[] TextosInfo;
    public GameObject botonFinTutorial;
    public GameObject parche;

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
            TextosInfo[0].SetActive(true);
            //marcamos botones
            GameObject.Find("Canvas").GetComponent<Animator>().Play("Animboton"+faseActual);
            break;

            case 2: //salto+doble
            TextosInfo[0].SetActive(false);
            TextosInfo[1].SetActive(true);
            //marcamos boton
            GameObject.Find("Canvas").GetComponent<Animator>().Play("Animboton"+faseActual);
            //activamos collider salto
            break;

            case 3: //Dash
            //texto
            TextosInfo[1].SetActive(false);
            TextosInfo[2].SetActive(true);
            //boton
            GameObject.Find("Canvas").GetComponent<Animator>().Play("Animboton"+faseActual);
            //particulas aparece enemigo
            Instantiate(particulasEnemigo,PosicionEnemigos[0].position,Quaternion.identity);
            //enemigo
            Instantiate(enemigo,PosicionEnemigos[0].position,Quaternion.identity);
            break;

            case 4: //Disparo
            TextosInfo[2].SetActive(false);
            TextosInfo[3].SetActive(true);
            GameObject.Find("Canvas").GetComponent<Animator>().Play("Animboton"+faseActual);
            //particulas aparece enemigo
            Instantiate(particulasEnemigo,PosicionEnemigos[1].position,Quaternion.identity);
            //enemigo
            Instantiate(enemigo,PosicionEnemigos[1].position,Quaternion.identity);
            break;

            case 5: //escudo
            TextosInfo[3].SetActive(false);
            TextosInfo[4].SetActive(true);
            GameObject.Find("Elementos_Escenario").GetComponent<Enegia>().AñadirEnergia(1000);
            parche.SetActive(true);
            GameObject.Find("Canvas").GetComponent<Animator>().Play("Animboton"+faseActual);
            //particulas aparece enemigo
            Instantiate(particulasEnemigo,PosicionEnemigos[0].position,Quaternion.identity);
            //enemigo
            Instantiate(enemigo2,PosicionEnemigos[0].position,Quaternion.identity);
            break;

            case 6: //explosion
            parche.SetActive(false);
            GameObject.Find("CTRL_Habilidades").GetComponent<Ctrl_Habilidades>().escudooff();
            GameObject.Find("Elementos_Escenario").GetComponent<Enegia>().Energia = 0;
            GameObject.Find("Elementos_Escenario").GetComponent<Enegia>().AñadirEnergia(1000);
            TextosInfo[4].SetActive(false);
            TextosInfo[5].SetActive(true);
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
            TextosInfo[5].SetActive(false);
            TextosInfo[6].SetActive(true);
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
        if(Fases[fase]==false&&Fases[fase-1]==true)
        {
            //TextoInfo.text = "Fase "+fase+" Completada!";
            Fases[fase] = true;
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
        if(other.tag == "Jugador"&&faseActual == 2)
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
        if(GameObject.FindGameObjectWithTag("Enemigo"))
        {
            Instantiate(particulasmuerte,GameObject.FindGameObjectWithTag("Enemigo").GetComponent<Transform>().position,Quaternion.identity);
            Destroy(GameObject.FindGameObjectWithTag("Enemigo"));
        }
        ControlFase();
    }
}
