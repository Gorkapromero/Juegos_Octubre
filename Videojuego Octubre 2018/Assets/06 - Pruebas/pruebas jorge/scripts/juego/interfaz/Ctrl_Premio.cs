using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Premio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CerrarPremio()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        if(GameObject.Find("Panel_Premios"))
            GameObject.Find("Panel_Premios").SetActive(false);
        Destroy(gameObject);
    }
}
