using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;


public class Window_IndicadorRecolectables : MonoBehaviour
{
    public GameObject Target;
    public Camera Camara;
    public Vector3 offset = new Vector3(0,0,0);

    public GameObject IndicadorDer;
    public GameObject IndicadorIzq;

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
                }
                else if (PosicionDer >= 0)
                {
                    IndicadorIzq.SetActive(false);
                    IndicadorDer.SetActive(true);
                }
            }
            else
            {
                //desactivamos indicador
                IndicadorIzq.SetActive(false);
                IndicadorDer.SetActive(false);

            }
        }
        else if(IndicadorDer||IndicadorIzq)
        {
            IndicadorIzq.SetActive(false);
            IndicadorDer.SetActive(false);
        }
    }
}
