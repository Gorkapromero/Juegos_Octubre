using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;
public class AdsManager : MonoBehaviour{

    bool anuncioVisto;

    Ctrl_Botones Botones;

    public string BotonClicked;
    public GameObject loading;

    //string appId = "ca-app-pub-9090842572495182~9736558171";

    string rewardedAdId = "ca-app-pub-9090842572495182/2556497183";

    private RewardBasedVideoAd rewardBasedVideo;

    //private RewardedAd rewardedAd;

    // Use this for initialization
    void Start ()
    {
        MobileAds.Initialize(initStatus => { });

        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        this.RequestRewardBasedVideo();
    }
    public void RequestRewardBasedVideo()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, rewardedAdId);

    }

    public void ShowRewarded()
    { 
      BotonClicked = EventSystem.current.currentSelectedGameObject.name;
        // Manager.ShowText("Cargamos video");
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
        else
        {
            GameObject.Find("Controlador").GetComponent<ToastMessage>().crearTexto("video not loaded!!");
        }
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        if(loading!=null)
        {
          loading.SetActive(false);
        }
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        GameObject.Find("Controlador").GetComponent<ToastMessage>().crearTexto("Failed to load video!!");
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {

    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {

    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardBasedVideo();

    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        //anuncioVisto = true;
        Scene Escena = SceneManager.GetActiveScene();
        switch (Escena.name)
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
                else if (BotonClicked == "Button_check_yes")
                {
                    GameObject.Find("Analytics").GetComponent<AnaliticsEvents>().AnalyticsPremios("Premio Premium", "recivido");
                    GameObject.Find("Recolctables").GetComponent<Premios>().PremiumLoot();
                    GameObject.Find("Recolctables").GetComponent<Premios>().CuadroAnuncioPremio.SetActive(false);
                }
                anuncioVisto = false;
                break;
        }
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {

    }
}
