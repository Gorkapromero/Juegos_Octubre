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
    ulong LastRewardOpen;

    public float TiempoEnHoras = 12.0f;
    private float msToWait;

    private void Start() 
    {
        DatosGuardar = GameObject.Find("Datosguardados").GetComponent<DatosGuardados>();
        LastRewardOpen = ulong.Parse(DatosGuardar.lastrewardOpen);

        msToWait = TiempoEnHoras * 3600000.0f;

        if(!RecompensaLista())
            BotonRecompensa.interactable = false;
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
            ulong dif = ((ulong)DateTime.Now.Ticks - LastRewardOpen);
            ulong m = dif / TimeSpan.TicksPerMillisecond;

            float secondsLeft = (float)(msToWait -m) / 1000.0f;

            string r = "";
            //horas
            r += ((int)secondsLeft / 3600).ToString() + "h ";
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;
            //minutes
            r += (secondsLeft / 60).ToString("00") + "m ";
            //segundos
            r += (secondsLeft % 60).ToString("00") + "m ";

            TextTiempoRecompensa.text = r;
        }
    }
    public void ClickRecompensa()
    {
        LastRewardOpen = (ulong)DateTime.Now.Ticks;
        DatosGuardar.lastrewardOpen = DateTime.Now.Ticks.ToString();
        BotonRecompensa.interactable = false;
        TextTiempoRecompensa.enabled = true;

        //iniciar anuncio
    }

    public bool RecompensaLista()
    {
        ulong dif = ((ulong)DateTime.Now.Ticks - LastRewardOpen);
        ulong m = dif / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (float)(msToWait -m) / 1000.0f;

        if(secondsLeft<0)
        {
            //TextTiempoRecompensa.text = "Ready!";
            TextTiempoRecompensa.enabled = false;
            return true;
        }
            
        return false;
    }

    public void VerHora()
    {

    }
}
