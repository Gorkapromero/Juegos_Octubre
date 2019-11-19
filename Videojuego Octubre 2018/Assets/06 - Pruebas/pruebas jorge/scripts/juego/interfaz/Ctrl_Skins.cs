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
	}
	public List <Skins> TablaSkins = new List<Skins>();

	public string SkinActivada;

	DatosGuardados DatosGuardados;

	public Material MaterialPajarita;

	bool drop;
	float posfinal;
	float posinicial;
	public float velocidad;

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
			print("posicion inicial: " + posinicial);
			print("posicion final: " + posfinal);
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
		}
	}

	public void Scroll()
	{
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
	}

	public void Drop()
	{
		int NumeroSkin = Mathf.Abs(Mathf.RoundToInt(GrupoSkins.transform.position.x/12));

		//Posicion.x = -NumeroSkin*12;
		posinicial = GrupoSkins.transform.position.x;
		posfinal = -NumeroSkin*12;
		Posicion.x=posinicial;
		drop = true;
		//GrupoSkins.transform.position = Posicion;
		SkinActivada = TablaSkins[NumeroSkin].Nombre;
		DatosGuardados.Skin = SkinActivada;
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

		for (int i = 0; i < TablaSkins.Count; i++)
		{
			if(TablaSkins[i].Nombre == SkinActivada)
			{
				GrupoSkins.transform.position = new Vector3((-i*12),GrupoSkins.transform.position.y,GrupoSkins.transform.position.z);
				return;
			}
		}
	}

	public void CambiarColor()
	{
		if(SkinActivada == "Normal")
		{
			Color ColorElejido = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;

			MaterialPajarita.color = ColorElejido;
		}
		

	}
}
