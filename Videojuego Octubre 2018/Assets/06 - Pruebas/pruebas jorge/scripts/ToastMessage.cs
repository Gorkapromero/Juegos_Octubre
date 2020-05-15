using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastMessage : MonoBehaviour
{
    public GameObject prefabText;
    public Transform padre;

    public float tiempoDestroy;
    // Start is called before the first frame update

    public void crearTexto(String _texto)
    {
        GameObject toastText = Instantiate(prefabText, padre);
        toastText.transform.GetChild(0).GetComponent<Text>().text = _texto;
        Destroy(toastText, tiempoDestroy);
    }
}
