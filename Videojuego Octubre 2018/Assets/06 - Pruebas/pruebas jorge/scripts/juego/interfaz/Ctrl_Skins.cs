using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ctrl_Skins : MonoBehaviour 
{
	public float roty;
	public float VelocidadRotacion;
	public Camera camara;
	public GameObject GrupoSkins;

	float ScrollMax;

	private Vector3 mOffset;
	private float mzCoors;
	private float myCoors;

	Vector3 Posicion;

	[System.Serializable]
	public class Skins
	{
		public string Nombre;
		public GameObject Skin;
		public GameObject TextoNombre;
		public int Precio;


        public GameObject imagenTarjeta;
    }
	public List <Skins> TablaSkins = new List<Skins>();

	public string SkinActivada;
	string SkinSeleccionada;

    public int offsetScroll;


    DatosGuardados DatosGuardados;

	public Material MaterialPajarita;
	public GameObject colores;

	bool drop;
	bool scroll;
	float posfinal;
	float posinicial;
	public float velocidad;

	public int NumeroSkin;
    float posicionOnMouseDown;

    public GameObject BotonSelec;
	public GameObject BotonComprar;
    public GameObject Boton_SkinSeleccionada;

    public Text T_Precio;
	
    // Use this for initialization
	void Start () 
	{
		ScrollMax = (TablaSkins.Count-1)*12;
		DatosGuardados = GameObject.Find("Datosguardados").GetComponent<DatosGuardados>();
		
		Invoke("buscarSkin",0.05f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(drop)
		{
			//print("posicion inicial: " + posinicial);
			//print("posicion final: " + posfinal);
			if(posfinal>posinicial)
			{
				Posicion.x += (velocidad *Time.deltaTime);
				GrupoSkins.transform.position = Posicion;
				if(Posicion.x>=posfinal)
				{
					Posicion.x = posfinal;
					GrupoSkins.transform.position = Posicion;
					drop = false;
				}
			}
			else
			{
				Posicion.x -= (velocidad *Time.deltaTime);
				GrupoSkins.transform.position = Posicion;
				if(Posicion.x<posfinal)
				{
					Posicion.x = posfinal;
					GrupoSkins.transform.position = Posicion;
					drop = false;
				}
			}

			Desbloqueo();
		}

		if(SkinSeleccionada == "Normal")
		{
			colores.SetActive(true);
		}
		else if(SkinSeleccionada!="Normal"&&colores)
		{
			colores.SetActive(false);
		}
	}

	public void Scroll()
	{
		scroll=true;
		Posicion = GetMouseAsWorldPoint()+mOffset;
		if(Posicion.x<0&&Posicion.x>-ScrollMax)
		{
			GrupoSkins.transform.position = Posicion;
		}
	}

	public void ScrollCircular()
	{
		Posicion = GetMouseAsWorldPoint()+mOffset;
		roty = Input.GetAxis("Mouse X")*VelocidadRotacion*Mathf.Deg2Rad;
		if(GrupoSkins.transform.eulerAngles.y-180>=0&&GrupoSkins.transform.eulerAngles.y-180<=270f)
		{
			GrupoSkins.transform.Rotate(Vector3.up, -roty);
		}
	}

	public void mouseDown()
	{
		mzCoors = camara.WorldToScreenPoint(GrupoSkins.transform.position).z;
		myCoors = camara.WorldToScreenPoint(GrupoSkins.transform.position).y;

		mOffset = GrupoSkins.transform.position - GetMouseAsWorldPoint();

        posicionOnMouseDown = GrupoSkins.transform.position.x;
	}

	public void Drop()
	{
		if(scroll)
		{
			scroll=false;

            if(posicionOnMouseDown >= GrupoSkins.transform.position.x)
            {
                NumeroSkin = Mathf.Abs(Mathf.RoundToInt((GrupoSkins.transform.position.x - offsetScroll) / 12));
            }
            else
            {
                NumeroSkin = Mathf.Abs(Mathf.RoundToInt((GrupoSkins.transform.position.x + offsetScroll) / 12));
            }

            //Posicion.x = -NumeroSkin*12;
            posinicial = GrupoSkins.transform.position.x;
			posfinal = -NumeroSkin*12;
			Posicion.x= posinicial;
			drop = true;
			//GrupoSkins.transform.position = Posicion;
			
            //CAMBIAMOS TEXTO SKIN
			for(int x = 0; x<TablaSkins.Count;x++)
			{
				TablaSkins[x].TextoNombre.SetActive(false);
			}
			TablaSkins[NumeroSkin].TextoNombre.SetActive(true);


			SkinSeleccionada = TablaSkins[NumeroSkin].Nombre;
                        
        }
		
       // print(SkinActivada);
		
	}

	private Vector3 GetMouseAsWorldPoint()
	{
		Vector3 mousePoint=Input.mousePosition;

		mousePoint.z = mzCoors;
		mousePoint.y = myCoors;

		return camara.ScreenToWorldPoint(mousePoint);
	}

	public void buscarSkin()
	{
		if(DatosGuardados.Skin!=null)
		{
			SkinActivada = DatosGuardados.Skin;
		}
		SkinSeleccionada = SkinActivada;

		ActualizarNombre();

        for (int i = 0; i < TablaSkins.Count; i++)
		{
			if(TablaSkins[i].Nombre == SkinActivada)
			{
				GrupoSkins.transform.position = new Vector3((-i*12),GrupoSkins.transform.position.y,GrupoSkins.transform.position.z);
				NumeroSkin = i;

                cambiarTarjetaMenu();
                Desbloqueo();

                return;
			}
		}
	}

	public void CambiarColor()
	{
		
		Color ColorElejido = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;

		MaterialPajarita.color = ColorElejido;
		
	}

	public void seleccionarSkin()
	{		   
        //TablaSkins[NumeroSkin].Skin.GetComponent<Animator>().Play("LevantarDeCaidaAtras");
		SkinActivada = TablaSkins[NumeroSkin].Nombre;

		DatosGuardados.Skin = SkinActivada;
        
        cambiarTarjetaMenu();
	}

    void cambiarTarjetaMenu()
    {
        //Desactivamos todas las imagenes de la tarjeta de Skins
        for (int i = 0; i < TablaSkins.Count; i++)
        {
            TablaSkins[i].imagenTarjeta.SetActive(false);
        }

        //Activamos la imagen de la skin seleccionada
        TablaSkins[NumeroSkin].imagenTarjeta.SetActive(true);

        //Desactivamos el boton de seleccionar a traves del void "Desbloqueo"
        Invoke("Desbloqueo",0.5f);
    }


    void ActualizarNombre()
	{
		for(int i = 0;i<TablaSkins.Count;i++)
		{
			TablaSkins[i].TextoNombre.SetActive(false);
			if(TablaSkins[i].Nombre == SkinActivada)
			{
				TablaSkins[i].TextoNombre.SetActive(true);
			}
		}
	}

	void Desbloqueo()
	{

        for (int i = 0; i < DatosGuardados.skinsdesbloqueadas.Length; i++)
        {
            if (DatosGuardados.skinsdesbloqueadas[i])
            {
                //Activamos la geometría de la skin correspondiente
                //print("Desactivar Skin OFF : " + TablaSkins[i].Nombre);
                if (GameObject.Find(TablaSkins[i].Nombre + "_OFF"))
                {
                    GameObject.Find(TablaSkins[i].Nombre + "_OFF").SetActive(false);
                }

                TablaSkins[i].Skin.SetActive(true);
            }

            else
            {
                if (GameObject.Find(TablaSkins[i].Nombre + "_OFF"))
                {
                    GameObject.Find(TablaSkins[i].Nombre + "_OFF").SetActive(true);
                }

                TablaSkins[i].Skin.SetActive(false);
            }
        }

		if(DatosGuardados.skinsdesbloqueadas[NumeroSkin])
		{
            BotonComprar.SetActive(false);

            if (SkinSeleccionada == SkinActivada)
            {
                BotonSelec.SetActive(false);
                Boton_SkinSeleccionada.SetActive(true);
            }
            else
            {
                BotonSelec.SetActive(true);
                Boton_SkinSeleccionada.SetActive(false);
            }
        }
		else
		{
            Boton_SkinSeleccionada.SetActive(false);

            T_Precio.text = TablaSkins[NumeroSkin].Precio.ToString() + "\ncoins";
			
            if (DatosGuardados.Monedas < TablaSkins[NumeroSkin].Precio)
            {
                T_Precio.color = new Color(0.5f, 0.0f, 0.0f);
            }
            else
            {
                T_Precio.color = new Color(0.3f, 0.3f, 0.3f);

            }
            BotonSelec.SetActive(false);
			BotonComprar.SetActive(true);
		}

        print("Skin Activada: " + SkinActivada);
        print("Skin Seleccionada: " + SkinSeleccionada);

        DatosGuardados.Save();
	}

	public void ComprarSkin()
	{
		if(DatosGuardados.Monedas>=TablaSkins[NumeroSkin].Precio)
		{
			DatosGuardados.Monedas-=TablaSkins[NumeroSkin].Precio;
			DatosGuardados.skinsdesbloqueadas[NumeroSkin] = true;

            GameObject.Find("Controlador").GetComponent<ControlBotonesMenu>().VerMonedas();

            Desbloqueo();

           TablaSkins[NumeroSkin].Skin.GetComponent<Animator>().Play("AnimDesbloqueoSkin_Tienda");

        }
    }


}
