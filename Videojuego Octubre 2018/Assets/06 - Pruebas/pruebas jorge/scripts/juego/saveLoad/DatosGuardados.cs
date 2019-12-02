using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosGuardados : MonoBehaviour 
{
	public static DatosGuardados cont;
	public string Skin;
	public int Monedas;

	private void Start() 
	{
		Load();
	}
	public void Save()
	{
		SaveSystem.Save(this);
	}

	public void Load()
	{
		PlayerData data = SaveSystem.Load();

		if(data!=null)
		{
			Skin = data.Skin;
			Monedas = data.Monedas;
		}

	}

	private void Awake() 
	{
		if (cont == null) 
		{
			cont = this;
			DontDestroyOnLoad (gameObject);
			
		} else if (cont != this) 
		{
			Destroy(gameObject);
		}
	}
}
