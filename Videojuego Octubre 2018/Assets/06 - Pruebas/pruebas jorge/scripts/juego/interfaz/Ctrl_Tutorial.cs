using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Tutorial : MonoBehaviour
{
    public bool TutorialCompletado;
    int fase;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ControlFase()
    {
        if(Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
        }
        switch(fase)
        {
            case 1:
            //marcamos botones
            break;
            case 2:
            //marcamos boton
            break;
            case 3:
            break;
            case 4:
            break;
            case 5:
            break;
            case 6:
            break;
        }
    }

    public void FaseCompletada()
    {
        fase++;
        ControlFase();
    }
}
