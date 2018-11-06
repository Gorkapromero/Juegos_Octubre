using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour 
	{
	public GameObject LoadingScreen;
	public Slider slider; 
	public Text Progresstext;


	public void LoadLevel (int sceneIndex)
	{
		StartCoroutine (LoadAsynchtonosly (sceneIndex));

		
	}

	IEnumerator LoadAsynchtonosly (int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneIndex);

		LoadingScreen.SetActive (true);

		while (!operation.isDone) 
		{
			float progress = Mathf.Clamp01 (operation.progress / .9f);
			slider.value = progress;
			Progresstext.text = progress * 100f + "%"; 
			yield return null;

		}
	}


		
	}

