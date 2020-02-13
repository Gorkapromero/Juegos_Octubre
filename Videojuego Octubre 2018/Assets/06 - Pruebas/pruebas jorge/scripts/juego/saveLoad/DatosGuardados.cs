using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosGuardados : MonoBehaviour 
{
	public static DatosGuardados cont;
	public string Skin = "Normal";
	public int Monedas;
	public int puntuacion;
	public bool musica;
	public bool fx;
	public bool[] skinsdesbloqueadas;
	public ulong lastrewardOpen;
	public bool tutorialCompletado;
	public bool videovisto;

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
			puntuacion = data.Puntuacion;
			musica = data.Musica;
			fx = data.Fx;
			
			for (int i = 0; i < data.SkinsDesbloqueadas.Length; i++)
			{
				skinsdesbloqueadas[i] = data.SkinsDesbloqueadas[i];
			}
			lastrewardOpen = data.LastRewardOpen;
			tutorialCompletado = data.TutorialCompletado;
			videovisto = data.Videovisto;
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
