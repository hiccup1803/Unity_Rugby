using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random : MonoBehaviour {
    public GameObject[] bots;
    public GameObject player;
    public GameObject time;
    public GameObject ball;
    public int rand;
    public bool ball_h;
    private int i;
    public int scen;
    // Start is called before the first frame update
    void Start () {
        ball_h = false;
        rand = Random.Range (1, 4);
        for (i = 0; i < bots.Length; i++) {
            bots[i].GetComponent<bot> ().enabled = false;
            bots[i].GetComponent<bot> ().Bot.SetInteger ("change", 0);
            bots[i].GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
        }
        player.GetComponent<player> ().enabled = false;
        player.GetComponent<player> ().main.SetInteger ("change", 0);
        time.GetComponent<time> ().enabled = false;
        switch (rand) {
            case 1:
                player.GetComponent<player> ().ball.SetActive (true);
                player.GetComponent<player> ().main.SetInteger ("change", 5);
                through (player.GetComponent<player> ().ball.gameObject);
                break;
            case 2:
                bots[1].GetComponent<bot> ().ball.SetActive (true);
                bots[1].GetComponent<bot> ().Bot.SetInteger ("change", 5);
                through (bots[1].GetComponent<bot> ().ball.gameObject);
                break;
            case 3:
                bots[2].GetComponent<bot> ().ball.SetActive (true);
                bots[2].GetComponent<bot> ().Bot.SetInteger ("change", 5);
                through (bots[2].GetComponent<bot> ().ball.gameObject);
                break;
            case 4:
                bots[3].GetComponent<bot> ().ball.SetActive (true);
                bots[3].GetComponent<bot> ().Bot.SetInteger ("change", 5);
                through (bots[3].GetComponent<bot> ().ball.gameObject);
                break;

        }
    }

    // Update is called once per frame
    void Update () {
        switch (rand) {
            case 1:
                if (ball_h == true) {
                    player.GetComponent<player> ().ball.SetActive (false);
                    ball_h = false;
                }
                break;
            case 2:
                if (ball_h == true) {
                    bots[1].GetComponent<bot> ().ball.SetActive (false);
                    ball_h = false;
                }
                break;
            case 3:
                if (ball_h == true) {
                    bots[2].GetComponent<bot> ().ball.SetActive (false);
                    ball_h = false;
                }
                break;
            case 4:
                if (ball_h == true) {
                    bots[3].GetComponent<bot> ().ball.SetActive (false);
                    ball_h = false;
                }
                break;

        }
    }
    void through (GameObject direction) {
        GameObject test = null;
        Vector3 dir = Vector3.zero;
        test = Instantiate (ball, new Vector3 (direction.transform.position.x, direction.transform.position.y, direction.transform.position.z), Quaternion.identity);
        dir = new Vector3 (direction.transform.position.x, direction.transform.position.y + 5f, direction.transform.position.z);
        if(scen==1){
        test.GetComponent<Rigidbody> ().AddForce (dir * 50f);
        }else if(scen==2){
        test.GetComponent<Rigidbody> ().AddForce (direction.transform.forward*1500f);
        }
        ball_h = true;
        StartCoroutine (reset ());
    }
    IEnumerator reset () {
        yield return new WaitForSeconds (2f);
        for (i = 0; i < bots.Length; i++) {
            bots[i].GetComponent<bot> ().enabled = true;
            bots[i].GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = true;
            bots[i].GetComponent<bot> ().Bot.SetInteger ("change", 1);

        }
        player.GetComponent<player> ().enabled = true;
        time.GetComponent<time> ().enabled = true;
        player.GetComponent<player> ().main.SetInteger ("change", 1);

    }
}