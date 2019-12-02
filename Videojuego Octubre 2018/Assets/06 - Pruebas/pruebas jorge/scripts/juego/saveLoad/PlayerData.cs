using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
	public string Skin;
	public int Monedas;

	public PlayerData(DatosGuardados Datos)
	{
		Skin = Datos.Skin;
		Monedas = Datos.Monedas;
	}
	
}
