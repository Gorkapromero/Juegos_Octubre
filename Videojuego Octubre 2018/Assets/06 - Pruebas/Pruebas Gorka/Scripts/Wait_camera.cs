using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wait_camera : MonoBehaviour
{
    public float wait_time = 30f;
    DatosGuardados datosGuardados;

    public GameObject BotonSkip;
    // Start is called before the first frame update
    void Start()
    {
        datosGuardados = GameObject.Find("Datosguardados").GetComponent<DatosGuardados>();
        Invoke("activarSkip", 1.0f);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(wait_time);

        datosGuardados.videovisto = true;
        if(datosGuardados.tutorialCompletado)
        {
            GameObject.Find("LevelLoader").GetComponent<Loader>().LoadLevel(1);
            //SceneManager.LoadScene(1);
        }
        else
        {
            GameObject.Find("LevelLoader").GetComponent<Loader>().LoadLevel(3);
            //SceneManager.LoadScene(3);
        }
    }

    public void Skip()
    {
        if(datosGuardados.tutorialCompletado)
        {
            GameObject.Find("LevelLoader").GetComponent<Loader>().LoadLevel(1);
            //SceneManager.LoadScene(1);
        }
        else
        {
            GameObject.Find("LevelLoader").GetComponent<Loader>().LoadLevel(3);
            //SceneManager.LoadScene(3);
        }
    }

    void activarSkip()
    {
        if(datosGuardados.videovisto)
        {
            BotonSkip.SetActive(true);
        }
        else
        {
            BotonSkip.SetActive(false);
        }
    }
}
