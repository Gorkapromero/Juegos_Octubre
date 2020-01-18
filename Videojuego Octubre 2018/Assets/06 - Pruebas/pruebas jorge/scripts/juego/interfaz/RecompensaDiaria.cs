using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class RecompensaDiaria : MonoBehaviour
{
    DatosGuardados DatosGuardar;
    public Button BotonRecompensa;
    public Text TextTiempoRecompensa;
    public ulong chestOpen;

    public float TiempoEnHoras = 12.0f;
    public float msToWait;
    public GameObject RecompensaRecivida;
    public Text TRecompensa;
    public int recompensa;

    private void Start() 
    {
        DatosGuardar = GameObject.Find("Datosguardados").GetComponent<DatosGuardados>();

        msToWait = TiempoEnHoras * 3600000.0f;

        Invoke("ComprobarRecompensa",0.00001f);
    }
    private void Update() 
    {
        if(!BotonRecompensa.IsInteractable())
        {
            if(RecompensaLista())
            {
                BotonRecompensa.interactable = true;
                return;
            }

            //ponemos tiempo
            ulong dif = ((ulong)DateTime.Now.Ticks - chestOpen);
            ulong m = dif / TimeSpan.TicksPerMillisecond;

            float secondsLeft = (float)(msToWait -m) / 1000.0f;

            string r = "";
            //horas
            r += ((int)secondsLeft / 3600).ToString() + "h ";
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;
            //minutes
            r += ((int)secondsLeft / 60).ToString("00") + "m ";
            //segundos
            r += (secondsLeft % 60).ToString("00") + "s ";

            TextTiempoRecompensa.text = r;
        }
    }
    public void ClickRecompensa()
    {
        chestOpen = (ulong)DateTime.Now.Ticks;
        DatosGuardar.lastrewardOpen = chestOpen;
        BotonRecompensa.interactable = false;
        TextTiempoRecompensa.enabled = true;

        //dar recompensa
        RecompensaRecivida.SetActive(true);
        TRecompensa.text = recompensa.ToString();
        DatosGuardar.Monedas += recompensa;
    }

    bool RecompensaLista()
    {
        ulong dif = ((ulong)DateTime.Now.Ticks - chestOpen);
        ulong m = dif / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (float)(msToWait - m) / 1000.0f;
        if(secondsLeft<0)
        {
            //TextTiempoRecompensa.text = "Ready!";
            TextTiempoRecompensa.enabled = false;
            return true;
        }
            
        return false;
    }

    public void ComprobarRecompensa()
    {
                
        chestOpen = DatosGuardar.lastrewardOpen;      

        if(!RecompensaLista())
            BotonRecompensa.interactable = false;
    }

    public void recompensaOK()
    {
        RecompensaRecivida.SetActive(false);
    }
}
