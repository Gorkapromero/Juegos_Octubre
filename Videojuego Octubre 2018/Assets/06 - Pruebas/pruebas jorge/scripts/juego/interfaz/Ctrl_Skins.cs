using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Skins : MonoBehaviour 
{
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

	// Use this for initialization
	void Start () 
	{
		ScrollMax = (TablaSkins.Count-1)*15;
		DatosGuardados = GameObject.Find("Datosguardados").GetComponent<DatosGuardados>();
		
		Invoke("buscarSkin",0.05f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Scroll()
	{
		Posicion = GetMouseAsWorldPoint()+mOffset;
		if(Posicion.x<0&&Posicion.x>-90f)
		{
			GrupoSkins.transform.position = Posicion;
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
		int NumeroSkin = Mathf.Abs(Mathf.RoundToInt(GrupoSkins.transform.position.x/15));

		Posicion.x = -NumeroSkin*15;
		GrupoSkins.transform.position = Posicion;
		SkinActivada = TablaSkins[NumeroSkin].Nombre;
		DatosGuardados.Skin = SkinActivada;
		
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
				GrupoSkins.transform.position = new Vector3((-i*15)+42.09f,GrupoSkins.transform.position.y,GrupoSkins.transform.position.z);
				return;
			}
		}
	}
}
