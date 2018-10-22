using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingScript : MonoBehaviour {

    public GameObject posicion;
    public GameObject Nombre;
    public GameObject Puntos;

    public void PonerPuntos(string pos, string nombre, string puntos)
    {
        this.posicion.GetComponent<Text>().text = pos;
        this.posicion.GetComponent<Text>().text = nombre;
        this.posicion.GetComponent<Text>().text = puntos;
    }

}
