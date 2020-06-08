using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class AdsManager : MonoBehaviour, IRewardedVideoAdListener {
	
  private const string appKey = "7cbc55e6ba43bf2cbc9b226db9714de0c5f6686b329f1d37";
	//ManagerScript Manager;
  int timesTriedToShowInterstitial = 0;

  bool anuncioVisto;

  Ctrl_Botones Botones;

  public string BotonClicked;
  public GameObject loading;
 
 // Use this for initialization
 void Start () 
 	{   
        Appodeal.disableLocationPermissionCheck();
        Appodeal.setTesting(false);
        Appodeal.initialize(appKey, Appodeal.REWARDED_VIDEO);
        Appodeal.setRewardedVideoCallbacks(this);
    }
 
    public void ShowBanner()
    {
        if (Appodeal.isLoaded(Appodeal.BANNER))
            Appodeal.show(Appodeal.BANNER_TOP);
    }
    public void HideBanner()
    {
        Appodeal.hide(Appodeal.BANNER);
    }
 
    public void ShowInterstitial()
    {
        timesTriedToShowInterstitial++;
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL) && timesTriedToShowInterstitial >= 5)
        {
            Appodeal.show(Appodeal.INTERSTITIAL);
            timesTriedToShowInterstitial = 0;
        }
    }
 
    public void ShowRewarded()
    { 
      BotonClicked = EventSystem.current.currentSelectedGameObject.name;
        // Manager.ShowText("Cargamos video");
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
        else
        {
            GameObject.Find("Controlador").GetComponent<ToastMessage>().crearTexto("video not loaded!!");
        }
    }

    void IRewardedVideoAdListener.onRewardedVideoLoaded(bool precache)
    {
        if(loading!=null)
        {
          loading.SetActive(false);
        }
    }

    void IRewardedVideoAdListener.onRewardedVideoFailedToLoad()
    {
        GameObject.Find("Controlador").GetComponent<ToastMessage>().crearTexto("Failed to load video!!");
    }

    void IRewardedVideoAdListener.onRewardedVideoShown()
    {
    }

    void IRewardedVideoAdListener.onRewardedVideoFinished(double amount, string name)
    {
      anuncioVisto = true;
    }

    void IRewardedVideoAdListener.onRewardedVideoClosed(bool finished)
    {
      if(anuncioVisto)
      {
        Scene Escena = SceneManager.GetActiveScene();
        switch(Escena.name)
        {
          case "Menu":
            //iniciamos tiempo y damos recompensa
            GameObject.Find("RecompensaDiaria").GetComponent<RecompensaDiaria>().ClickRecompensa();
            GameObject.Find("Controlador").GetComponent<ControlBotonesMenu>().cuadroAnuncio.SetActive(false);

            //Activamos la animacion de recoger texto "Daily Gift"
            GameObject.Find("texto_daily").GetComponent<Animator>().SetBool("recogerTexto", true);

          break;

          case "02_Pruebas_Escenario_2":
            if (BotonClicked == "Button_check_yesContinue")
            {
              GameObject.Find("Canvas").GetComponent<Ctrl_Botones>().VerAnuncio();
              GameObject.Find("Recolctables").GetComponent<Premios>().CuadroAnuncioJugar.SetActive(false);
            }
            else
            {
              GameObject.Find("Analytics").GetComponent<AnaliticsEvents>().AnalyticsPremios("Premio Premium", "recivido");
              GameObject.Find("Recolctables").GetComponent<Premios>().PremiumLoot();
              GameObject.Find("Recolctables").GetComponent<Premios>().CuadroAnuncioPremio.SetActive(false);
            }
            anuncioVisto = false;
          break;
        }
      }
      
    }

    void IRewardedVideoAdListener.onRewardedVideoExpired()
    {
    }

    void IRewardedVideoAdListener.onRewardedVideoClicked()
    {
    }
}
