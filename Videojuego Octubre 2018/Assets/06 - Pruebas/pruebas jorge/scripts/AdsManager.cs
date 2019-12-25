using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour, IRewardedVideoAdListener {
	
  public string appKey = "Put your app key here.";
	//ManagerScript Manager;
  int timesTriedToShowInterstitial = 0;

  bool anuncioVisto;

  Ctrl_Botones Botones;
 
 // Use this for initialization
 void Start () 
 	{
		//Manager = gameObject.GetComponent<ManagerScript>();
    Botones = GameObject.Find("Canvas").GetComponent<Ctrl_Botones>();

        
        Appodeal.disableLocationPermissionCheck();
        Appodeal.setTesting(true);
        Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.BANNER | Appodeal.REWARDED_VIDEO);
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
       // Manager.ShowText("Cargamos video");
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
            Appodeal.show(Appodeal.REWARDED_VIDEO);
    }

    void IRewardedVideoAdListener.onRewardedVideoLoaded(bool precache)
    {
    }

    void IRewardedVideoAdListener.onRewardedVideoFailedToLoad()
    {

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
            //premio
          break;

          case "02_Pruebas_Escenario_2":
            Botones.VerAnuncio();
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
