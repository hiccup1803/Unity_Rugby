using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class time : MonoBehaviour {

    public UnityEngine.UI.Text timeVar;
    public UnityEngine.UI.Text win_lose;
    public int second;
    public int minutes;
    public GameObject[] bots;
    public GameObject player;
    public int[] bot_scores;
    public int player_score;
    private int i;
    public GameObject popup;
    public Button next_rep;
    public Sprite next;
    public Sprite restart;
    // Start is called before the first frame update
    void Start () {
        timeVar.text = "TIME:  " + minutes.ToString () + ":" + second.ToString ();
        InvokeRepeating ("times", 1f, 1f);
    }

    // Update is called once per frame
    void Update () {

    }
    void times () {

        if ((second == 0) && (minutes == 0)) {
            for (i = 0; i < bot_scores.Length; i++) {
                if (bot_scores[i] >= player_score) {
                    next_rep.image.sprite = restart;
                    popup.SetActive (true);
                    win_lose.text = "you lose";
                    next_rep.GetComponent<Rewarded> ().snumber = SceneManager.GetActiveScene ().buildIndex;
                } else {
                    next_rep.image.sprite = next;
                    popup.SetActive (true);
                    win_lose.text = "you win";
                    if(SceneManager.GetActiveScene ().buildIndex==3){
                    next_rep.GetComponent<Rewarded> ().snumber =0;
                    }else{
                    next_rep.GetComponent<Rewarded> ().snumber = SceneManager.GetActiveScene ().buildIndex + 1;
                    }
                }
            }
            for (i = 0; i < bots.Length; i++) {
                bots[i].GetComponent<bot> ().enabled = false;
                bots[i].GetComponent<bot> ().Bot.SetInteger ("change", 0);
                bots[i].GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
            }
            player.GetComponent<player> ().enabled = false;
        }
        if ((second == 0) && (minutes > 0)) {
            minutes -= 1;
            second = 60;
            timeVar.text = "TIME:  " + minutes.ToString () + ":" + second.ToString ();
        }
        if (second > 0) {

            second = second - 1;
            timeVar.text = "TIME:  " + minutes.ToString () + ":" + second.ToString ();
            for (i = 0; i < bots.Length; i++) {
                bot_scores[i] = bots[i].GetComponent<bot> ().Score;
            }
            player_score = player.GetComponent<player> ().Score;
        }
    }
}