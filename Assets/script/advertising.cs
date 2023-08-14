using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class advertising : MonoBehaviour {
    public int scenenumber;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame

    public void play_add () {
        if (Advertisement.IsReady ("add")) {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show ("add", options);
        }
    }
    void Openscene () {
        SceneManager.LoadScene (scenenumber);
    }
    public void HandleShowResult (ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
                Debug.Log ("The ad was successfully shown.");
                Openscene ();
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log ("The ad was skipped before reaching the end.");
                Openscene ();
                break;
            case ShowResult.Failed:
                Debug.LogError ("The ad failed to be shown.");
                break;
        }
    }
}