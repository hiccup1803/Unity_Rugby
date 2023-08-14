using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Rewarded : MonoBehaviour {
#if UNITY_ANDROID
    string GameID = "ca-app-pub-8577185961907109~8020857376";
    string bannerAdId = "ca-app-pub-8577185961907109/4081612368";
    string InterstitialAdID = "ca-app-pub-8577185961907109/9881374588";
    string rewarded_Ad_ID = "ca-app-pub-8577185961907109/4838314352";
#elif UNITY_IPHONE
    string GameID = "ca-app-pub-8577185961907109~5670666830";
    string bannerAdId = "ca-app-pub-8577185961907109/5750557887";
    string InterstitialAdID = "ca-app-pub-8577185961907109/5959824335";
    string rewarded_Ad_ID = "ca-app-pub-8577185961907109/2741251163";
#endif
    public int snumber;
    public BannerView bannerAd;
    public InterstitialAd interstitial;
    public RewardBasedVideoAd rewardedAd;

    public static Rewarded instance;

    public void openscene(){

        SceneManager.LoadScene (snumber);
    }
    private void Awake () {
        //Optional 
        //if (instance != null && instance != this) {
           // Destroy (gameObject);
           // return;
       // }
       // instance = this;
        //DontDestroyOnLoad (this);

        rewardedAd = RewardBasedVideoAd.Instance;

        // Banner Ad

        Banner ();

        // Requiest interstitial ad and reward ad

        requestInterstital ();
        loadRewardVideo ();

    }

    // Start is called before the first frame update
    void Start () {
        MobileAds.Initialize (GameID);

    }

    public void Banner () {
        Debug.Log ("Banner");
        reqBannerAd ();
    }
    public void ran () {
        int Rand = UnityEngine.Random.Range (1, 3);
        if (Rand == 1) {
            Interstitial ();
        } else {
            Reward ();
        }
    }
    public void Interstitial () {
        Debug.Log ("Interstitial");
        ShowInterstitialAd ();

    }

    public void Reward () {
        Debug.Log ("Reward");
        showVideoAd ();
    }

    #region rewarded Video Ads

    public void loadRewardVideo () {
        rewardedAd.LoadAd (new AdRequest.Builder ().Build (), rewarded_Ad_ID);

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdRewarded += HandleUserEarnedReward;
        rewardedAd.OnAdLeavingApplication += HandleOnRewardAdleavingApp;

    }

    /// rewarded video events //////////////////////////////////////////////

    public event EventHandler<EventArgs> OnAdLoaded;

    public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

    public event EventHandler<EventArgs> OnAdOpening;

    public event EventHandler<EventArgs> OnAdStarted;

    public event EventHandler<EventArgs> OnAdClosed;

    public event EventHandler<Reward> OnAdRewarded;

    public event EventHandler<EventArgs> OnAdLeavingApplication;

    public event EventHandler<EventArgs> OnAdCompleted;

    /// Rewared events //////////////////////////

    public void HandleRewardedAdLoaded (object sender, EventArgs args) {
        Debug.Log ("Video Loaded");
    }

    public void HandleRewardedAdFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
        Debug.Log ("Video not loaded");

    }

    public void HandleRewardedAdOpening (object sender, EventArgs args) {
        Debug.Log ("Video Loading");

    }

    public void HandleRewardedAdFailedToShow (object sender, AdErrorEventArgs args) {
        Debug.Log ("Video Loading failed");

    }

    public void HandleRewardedAdClosed (object sender, EventArgs args) {
        Debug.Log ("Video Loading failed");
        SceneManager.LoadScene (snumber);

    }

    public void HandleUserEarnedReward (object sender, Reward args) {

    }

    public void HandleOnRewardAdleavingApp (object sender, EventArgs args) {
        Debug.Log ("when user clicks the video and open a new window");
    }

    public void showVideoAd () {
        if (rewardedAd.IsLoaded ()) {
            rewardedAd.Show ();
        } else {
            Debug.Log ("Rewarded Video ad not loaded");
            SceneManager.LoadScene (snumber);
        }
    }

    #endregion

    #region banner

    public void reqBannerAd () {
        this.bannerAd = new BannerView (bannerAdId, AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        this.bannerAd.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerAd.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;

        AdRequest request = new AdRequest.Builder ().Build ();

        this.bannerAd.LoadAd (request);

    }

    public void hideBanner () {
        this.bannerAd.Hide ();

    }

    #endregion

    #region interstitial

    public void requestInterstital () {
        this.interstitial = new InterstitialAd (InterstitialAdID);

        this.interstitial.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.interstitial.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.interstitial.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder ().Build ();

        this.interstitial.LoadAd (request);
    }

    public void ShowInterstitialAd () {
        if (this.interstitial.IsLoaded ()) {
            this.interstitial.Show ();
            Debug.Log ("banner");
        SceneManager.LoadScene (snumber);

        }    }

    #endregion

    #region adDelegates

    public void HandleOnAdLoaded (object sender, EventArgs args) {
        Debug.Log ("Ad Loaded");
    
    }

    public void HandleOnAdFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
        Debug.Log ("couldnt load ad" + args.Message);
    }

    public void HandleOnAdOpened (object sender, EventArgs args) {
        MonoBehaviour.print ("HandleAdOpened event received");

    }

    public void HandleOnAdClosed (object sender, EventArgs args) {
        Debug.Log ("Ad Closed");
        requestInterstital (); // Optional : in case you want to load another interstial ad rightaway
        SceneManager.LoadScene (snumber);
    }

    public void HandleOnAdLeavingApplication (object sender, EventArgs args) {
        MonoBehaviour.print ("HandleAdLeavingApplication event received");
    }

    #endregion

}