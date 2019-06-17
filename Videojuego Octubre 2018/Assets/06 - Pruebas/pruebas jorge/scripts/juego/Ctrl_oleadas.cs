using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_oleadas : MonoBehaviour
{
    public Transform[] posiciones;
    //public GameObject Objetos;
    public float tiempoCreacion = 2f;

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class oleada
    {
        public string nombre;
        public int numeroOleada;
        //public GameObject enemigo;
        [System.Serializable]
        public class Enemigo
        {
            public string nombre;
            public GameObject enemigo;
            public int rareza;
        }
        public Enemigo[] enemigos;
        //public int NumeroEnemigos;
    }

    public oleada[] oleadas;

    public int NumeroEnemigos;

    int ContadorOleadas = 0;

    public float TiempoDescanso;
    float WaveCountdown;

    private SpawnState Estado = SpawnState.COUNTING;
    public SpawnState estado
    {
        get { return Estado; }
    }

    //crar_objeto Enemigos;
    float total = 0;

    public Text TextoOleadas;
    // Use this for initialization
    void Start()
    {
        //Enemigos = gameObject.GetComponent<crar_objeto>();
        //WaveCountdown = TiempoDescanso;
        ActualizarTextoOleadas();
    }

    // Update is called once per frame
    void Update()
    {
        if (Estado == SpawnState.WAITING)
        {
            if(GameObject.FindGameObjectWithTag("Enemigo") == null)//comprobar si el enemigo esta vivo
            {
                OleadaCompletada();
            }
            else
            {
                return;
            }
        }

        if (WaveCountdown <= 0)
        {
            if(Estado != SpawnState.SPAWNING)
            {
                if (oleadas.Length > 1)  //comprobamos si hay oleadas personalizadas
                {
                    for (int s = 1; s < oleadas.Length; s++)
                    {
                        if (oleadas[s].numeroOleada == ContadorOleadas + 1)  //miramos si alguna coincide con la oleada actual
                        {
                            print("oleada personalizada");
                            StartCoroutine(SpawnOleada(oleadas[s]));
                        }
                        else
                        {
                            print("oleada normal");
                            StartCoroutine(SpawnOleada(oleadas[0]));
                        }
                    }
                }
                else
                {
                    print("oleada normal");
                    StartCoroutine(SpawnOleada(oleadas[0]));
                }

            }
        }
        else
        {
            WaveCountdown -= Time.deltaTime;
        }

    }


    IEnumerator SpawnOleada(oleada _oleada)
    {
        Debug.Log("Spawn Oleada: " + (ContadorOleadas+1));
        Estado = SpawnState.SPAWNING;

        for (int i = 0; i < NumeroEnemigos; i++)
        {
            crear(_oleada);//ir a crear enemigo del otro script
            print("enemigo: " + i);
            yield return new WaitForSecondsRealtime(tiempoCreacion);
        }

        Estado = SpawnState.WAITING;

        yield break;
    }

    void OleadaCompletada()
    {
        Debug.Log("Oleada completada!!");

        Estado = SpawnState.COUNTING;
        WaveCountdown = TiempoDescanso;

        ContadorOleadas++;
        NumeroEnemigos = NumeroEnemigos + 3;
        ActualizarTextoOleadas();
    }

    public void crear(oleada _Oleada)
    {
        print("enemigo");
        total = 0;
        for (int i = 0; i < _Oleada.enemigos.Length; i++)
        {
            total += _Oleada.enemigos[i].rareza;
        }
        print("total " + total);

        float randomPoint = Random.value * total;

        for (int j = 0; j < _Oleada.enemigos.Length; j++)
        {
            if (randomPoint < _Oleada.enemigos[j].rareza)
            {
                int spawnPoint = Random.Range(0, posiciones.Length);
                GameObject Objeto = Instantiate(_Oleada.enemigos[j].enemigo, posiciones[spawnPoint].position, Quaternion.identity);
                return;
            }
            else
            {
                randomPoint -= _Oleada.enemigos[j].rareza;
            }
        }

    }

    public void ActualizarTextoOleadas()
    {
        TextoOleadas.text = (ContadorOleadas + 1).ToString();
    }

}
