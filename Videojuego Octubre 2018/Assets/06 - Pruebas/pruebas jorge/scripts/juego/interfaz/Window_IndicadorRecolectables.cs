using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;


public class Window_IndicadorRecolectables : MonoBehaviour
{
    public GameObject Target;
    public GameObject Target2;
    public Camera Camara;
    public Vector3 offset = new Vector3(0,0,0);

    public GameObject IndicadorDer;
    public GameObject IndicadorIzq;

    public GameObject IndicadorCajaIzq;
    public GameObject IndicadorPremioIzq;
    public GameObject IndicadorCajaDer;
    public GameObject IndicadorPremioDer;

    private void awake()
    {

    }

    private void Update()
    {
        if (Target)
        {
            /*Vector3 toPosition = TargetPosition.position;
            Vector3 fromPosition = Camara.transform.position;
            fromPosition.z = 0f;
            Vector3 dir = (toPosition - fromPosition).normalized;*/

            Vector3 targetPositionScreenPoint = Camara.WorldToScreenPoint(Target.transform.position);
            float posicionIzq = targetPositionScreenPoint.x + offset.x;
            float PosicionDer = targetPositionScreenPoint.x - offset.x;
            bool isOffScreen = posicionIzq <= 0 || PosicionDer >= Screen.width;
            //Debug.Log(isOffScreen + " " + (targetPositionScreenPoint.x + offset.x) + "," + (targetPositionScreenPoint.x - offset.x));

            if (isOffScreen)
            {
                //activamos indicador
                if (posicionIzq <= 0)
                {
                    IndicadorIzq.SetActive(true);
                    IndicadorDer.SetActive(false);
                    IndicadorCajaDer.SetActive(false);
                    IndicadorCajaIzq.SetActive(true);
                }
                else if (PosicionDer >= 0)
                {
                    IndicadorIzq.SetActive(false);
                    IndicadorDer.SetActive(true);
                    IndicadorCajaDer.SetActive(true);
                    IndicadorCajaIzq.SetActive(false);
                }
            }
            else
            {
                if (Target2)
                {
                    //desactivamos indicador
                    IndicadorCajaIzq.SetActive(false);
                    IndicadorCajaDer.SetActive(false);
                }
                else
                {
                    IndicadorIzq.SetActive(false);
                    IndicadorDer.SetActive(false);
                }

            }
        }
        else if (IndicadorDer || IndicadorIzq && !Target)
        {
            IndicadorCajaDer.SetActive(false);
            IndicadorCajaIzq.SetActive(false);
        }

        if (Target2)
        {
            Vector3 targetPositionScreenPoint = Camara.WorldToScreenPoint(Target2.transform.position);
            float posicionIzq = targetPositionScreenPoint.x + offset.x;
            float PosicionDer = targetPositionScreenPoint.x - offset.x;
            bool isOffScreen = posicionIzq <= 0 || PosicionDer >= Screen.width;
            if (isOffScreen)
            {
                //activamos indicador
                if (posicionIzq <= 0)
                {
                    IndicadorIzq.SetActive(true);
                    IndicadorPremioIzq.SetActive(true);
                    IndicadorDer.SetActive(false);
                    IndicadorPremioDer.SetActive(false);
                }
                else if (PosicionDer >= 0)
                {
                    IndicadorIzq.SetActive(false);
                    IndicadorPremioIzq.SetActive(false);
                    IndicadorDer.SetActive(true);
                    IndicadorPremioDer.SetActive(true);
                }
            }
            else
            {
                if(Target)
                {
                    //desactivamos indicador
                    IndicadorPremioIzq.SetActive(false);
                    IndicadorPremioDer.SetActive(false);
                }
                else
                {
                    IndicadorIzq.SetActive(false);
                    IndicadorDer.SetActive(false);
                }

            }
        }
        else if(IndicadorDer||IndicadorIzq&&!Target2)
        {
            IndicadorPremioIzq.SetActive(false);
            IndicadorPremioDer.SetActive(false);
        }

        if(!Target&&!Target2)
        {
            IndicadorIzq.SetActive(false);
            IndicadorDer.SetActive(false);
        }
    }
}
