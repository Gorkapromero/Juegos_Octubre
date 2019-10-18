using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_oleadas : MonoBehaviour
{
    public Transform PosicionParacas;
    public Transform[] posiciones;
    //public Transform[] posicionesActivas;
    public List<Transform> PosicionesActivas;
    //int pos;
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

    public int ContadorOleadas = 0;

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
    public Transform jugador;

    public float rangoDistancia;

    public GameObject TextoOleadaCompletada;

    // Use this for initialization
    void Start()
    {
        //Enemigos = gameObject.GetComponent<crar_objeto>();
        //WaveCountdown = TiempoDescanso;
        //jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
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
            crear(_oleada);
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
        TextoOleadaCompletada.SetActive(true);
        Invoke("DesactibarTexto", 3);
    }

    public void crear(oleada _Oleada)
    {
        PosicionesActivas = new List<Transform>();
        PosicionesActivas.Add(PosicionParacas);
        for (int x = 0; x < posiciones.Length;x++)
        {
            float dist = Vector3.Distance(posiciones[x].position, jugador.position);
            if(x<=4&&rangoDistancia>dist)//encimera
            {
                /*posicionesActivas[pos] = posiciones[x];
                pos++;*/
                PosicionesActivas.Add(posiciones[x]);
            }
            /*else if(x>4&&rangoDistancia<dist)
            {
                /*posicionesActivas[pos] = posiciones[x];
                pos++;
                PosicionesActivas.Add(posiciones[x]);
            }*/
        }
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
                if(_Oleada.enemigos[j].nombre=="Enemigo Bomba")
                {
                    Vector3 SpawnPosition = new Vector3(0, 0, 0);
                    //SpawnPosition = this.transform.position + Random.onUnitSphere * RangoCreacion;
                    SpawnPosition = new Vector3(jugador.position.x, PosicionParacas.position.y, jugador.position.z);

                    GameObject Objeto = Instantiate(_Oleada.enemigos[j].enemigo, SpawnPosition, Quaternion.identity);
                }
                else
                {
                    int spawnPoint = Random.Range(0, PosicionesActivas.Count);
                    if(spawnPoint==0)
                    {
                        Vector3 SpawnPosition = new Vector3(0, 0, 0);
                        SpawnPosition = new Vector3(jugador.position.x, PosicionParacas.position.y, jugador.position.z);
                        GameObject Objeto = Instantiate(_Oleada.enemigos[j].enemigo, SpawnPosition, Quaternion.identity);
                    }
                    else
                    {
                        GameObject Objeto = Instantiate(_Oleada.enemigos[j].enemigo, PosicionesActivas[spawnPoint].position, Quaternion.identity);
                    }
                }
                    //float dist = Vector3.Distance(posiciones[spawnPoint].position, jugador.position);
                    
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(jugador.position, rangoDistancia);
    }

    void DesactibarTexto()
    {
        TextoOleadaCompletada.SetActive(false);
    }
}
