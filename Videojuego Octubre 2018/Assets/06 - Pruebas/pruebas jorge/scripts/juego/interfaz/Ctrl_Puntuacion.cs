using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_Puntuacion : MonoBehaviour
{

    public int Enemigos_Eliminados = 0;
    int P_Final;

    //public int Tiempo;
    //public int Total;

    public Text Texto_Tiempo;
    public Text Texto_Oleadas;
    public Text Texto_Enemigos;
    public Text Texto_Score;
    public Text Texto_Monedas;
    public GameObject NewScore;

    public Text MonedasRecojidas;

    Timer tiempo;
    Ctrl_oleadas Oleadas;
    DatosGuardados DatosGuardar;

    public GameObject PantallaGuardar;

    public InputField Nombre;
    //public RankingManager Ranking;

    int monedas;

    public float VelocidadAnimacion;
    float Valor;
    float ValorFinal;
    float ValorInicial = 0;
    Text Texto;

    bool B_tiempo = true;
    bool FinTextos = true;
    bool restarmonedas = false;
    float ValorMonedas;
    int MonedasInicio;
    public int texto = 1;

    public int monedasRecojidas;
    public GameObject moneda;
    public Transform start;
    public Transform Parent;

    public GameObject CuadroContinue;
    public int CrearMonedas;

    // Use this for initialization
    void Start ()
    {
        DatosGuardar =GameObject.Find("Datosguardados").GetComponent<DatosGuardados>();
        tiempo = GameObject.Find("Timer").GetComponent<Timer>();
        if(GameObject.Find("creador_objetos"))
        {
            Oleadas = GameObject.Find("creador_objetos").GetComponent<Ctrl_oleadas>();
        }
        
        //Actualizar_enemigos();
        ApagarTextos();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(restarmonedas)
        {
            ValorMonedas -= (VelocidadAnimacion * Time.deltaTime)*(monedasRecojidas);
            //Invoke("CrearMoneda",(VelocidadAnimacion * Time.deltaTime)*(monedasRecojidas));
            if(ValorMonedas<=0)
            {
                ValorMonedas = 0;
                restarmonedas=false;
                Invoke("ActivarBotones",1f);
            }
            MonedasRecojidas.text = ValorMonedas.ToString("f0");
        }
        if (Valor != ValorFinal)
        {
            if (ValorInicial < ValorFinal)
            {
                Valor += (VelocidadAnimacion * Time.deltaTime) * (ValorFinal - ValorInicial);
                if(B_tiempo)
                {
                    string Minutos = ((int)Valor / 59).ToString("f0");
                    string Segundos = (Valor % 59).ToString("f0");
                    if (Valor % 59 < 9.5f)
                    {
                        Texto.text = Minutos + ":" + 0 + Segundos;
                    }
                    else
                    {
                        Texto.text = Minutos + ":" + Segundos;
                    }
                }
                else
                {
                    Texto.text = Valor.ToString("f0");
                }
                
                if (Valor >= ValorFinal)
                {
                    Valor = ValorFinal;
                    if (B_tiempo)
                    {
                        string Minutos = ((int)Valor / 59).ToString("f0");
                        string Segundos = (Valor % 59).ToString("f0");
                        if (Valor % 59 < 9.5f)
                        {
                            Texto.text = Minutos + ":" + 0 + Segundos;
                        }
                        else
                        {
                            Texto.text = Minutos + ":" + Segundos;
                        }
                    }
                    else
                    {
                        Texto.text = Valor.ToString("f0");
                    }

                    texto++;
                    MostrarTexto(texto);
                }

            }
        }
        else if(Valor==ValorFinal&&!FinTextos)
        {
            Valor = ValorFinal;
            Texto.text = Valor.ToString();

            texto++;
            MostrarTexto(texto);
        }

    }

    public void Mostrar_Textos()
    {
        FinTextos = false;
        Puntuacion_final();
        MostrarTexto(texto);
        //MonedasTotales.text = "TIENES " + DatosGuardar.Monedas.ToString() + " MONEDAS";
    }

    void Puntuacion_final()
    {
        tiempo.PararTiempo();
        P_Final = (Enemigos_Eliminados * 10) + (Oleadas.ContadorOleadas*100);
        if(P_Final>DatosGuardar.puntuacion)
        {
            Guardar();
        }
    }

    void Guardar()
    {
        DatosGuardar.puntuacion = P_Final;
        /*Ranking.BorrarPuntos(1);
        Ranking.InsertarMejorPuntuacion(1, P_Final);*/
    }

    void Mostrartiempo()
    {
        print("tiempo");
        B_tiempo = true;
        Valor = ValorInicial;
        Texto = Texto_Tiempo;
        ValorFinal = tiempo.Tiempo;
        //Texto_Tiempo.text = tiempo.T_Timer.text;
    }
    void Mostraroleadas()
    {
        print("oleadas");
        B_tiempo = false;
        FinTextos = false;
        Valor = ValorInicial;
        Texto = Texto_Oleadas;
        ValorFinal = Oleadas.ContadorOleadas;
        //Texto_Oleadas.text = Oleadas.ContadorOleadas.ToString();
    }
    void MostrarEnemigos()
    {
        print("Enemigos");
        B_tiempo = false;
        FinTextos = false;
        Valor = ValorInicial;
        Texto = Texto_Enemigos;
        ValorFinal = Enemigos_Eliminados;
        //Texto_Enemigos.text = Enemigos_Eliminados.ToString();
    }
    void Mostrarscore()
    {
        print("score");
        B_tiempo = false;
        FinTextos = false;
        Valor = ValorInicial;
        Texto = Texto_Score;
        ValorFinal = P_Final;
        //Texto_Score.text = P_Final.ToString("f0");
    }
    void MostrarMonedas()
    {
        ValorInicial = DatosGuardar.Monedas;
        print("monedas");
        //B_tiempo = false;
        //FinTextos = false;
        Valor = ValorInicial;
        Texto = Texto_Monedas;
        ValorMonedas = monedasRecojidas;
        restarmonedas = true;
        ValorFinal = DatosGuardar.Monedas + monedasRecojidas;
        StartCoroutine("CrearMoneda");
    }

    public void ApagarTextos()
    {
        Texto_Tiempo.text = null;
        Texto_Oleadas.text = null;
        Texto_Enemigos.text = null;
        Texto_Score.text = null;
        Texto_Monedas.text = DatosGuardar.Monedas.ToString();
        NewScore.SetActive(false);
        texto = 1;
        Valor = ValorInicial;
        ValorFinal = 0;
        MonedasRecojidas.text = monedasRecojidas.ToString();
        CuadroContinue.SetActive(false);
    }

    void MostrarTexto(int x)
    {
        switch(x)
        {
            case 1:
                Mostrartiempo();
                break;
            case 2:
                FinTextos = true;
                Invoke("Mostraroleadas", 0.5f);
                //Mostraroleadas();
                break;
            case 3:
                FinTextos = true;
                Invoke("MostrarEnemigos", 0.5f);
                //MostrarEnemigos();
                break;
            case 4:
                FinTextos = true;
                Invoke("Mostrarscore", 0.5f);
                //Mostrarscore();
                break;
            case 5:
                FinTextos = true;
                Invoke("MostrarMonedas", 0.5f);
                break;
        }
    }

    public void ActualizarMonedas()
    {
        monedasRecojidas++;
        MonedasRecojidas.text = monedasRecojidas.ToString();
    }

    IEnumerator CrearMoneda()
    {
        if(monedasRecojidas>CrearMonedas)
        {
            for (int i = 0; i < CrearMonedas; i++)
            {
                Instantiate(moneda,start.position,Quaternion.identity,Parent);
                yield return new WaitForSeconds(0.2f);
            }
            
        }
        else
        {
            for (int i = 0; i < monedasRecojidas; i++)
            {
                Instantiate(moneda,start.position,Quaternion.identity,Parent);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    void ActivarBotones()
    {
        CuadroContinue.SetActive(true);
    }
}
