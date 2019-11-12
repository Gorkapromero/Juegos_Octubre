using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsJuego : MonoBehaviour {

	
	[System.Serializable]
	public class Skins
	{
		public string Nombre;
		public GameObject Skin;
	}
	public List <Skins> TablaSkins = new List<Skins>();

	DatosGuardados DatosGuardados;

	private void Start() 
	{
		if(GameObject.Find("Datosguardados"))
		{
			DatosGuardados = GameObject.Find("Datosguardados").GetComponent<DatosGuardados>();
			for (int i = 0; i < TablaSkins.Count; i++)
			{
				if(TablaSkins[i].Nombre==DatosGuardados.Skin)
				{
					TablaSkins[i].Skin.SetActive(true);
				}
				else
				{
					TablaSkins[i].Skin.SetActive(false);
				}
			}
		}
		
	}
}
