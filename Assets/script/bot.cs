using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class bot : MonoBehaviour {
    public Text ss;
    private float distance;
    public NavMeshAgent agent;
    public float lookRadius = 10f;
    public float range = 2f;
    public Animator Bot;
    public float rotationSpeed = 20f;
    public GameObject ball;
    public GameObject prefab;
    public int Score;
    //
    public float longray = 0.8f;
    public bool next;
    public GameObject goal1;
    public GameObject goal2;
    public int switch_side;
    public string bot_name;
    void Start () {
        Bot = this.gameObject.GetComponent<Animator> ();
        Score = 0;
        ss.text = this.gameObject.name + " : " + Score.ToString ();
        next = false;
        bot_name = this.gameObject.name;
    }
    public GameObject instantiate_ball () {
        GameObject test = null;
        test = Instantiate (prefab, new Vector3 (Random.Range (this.transform.position.x, this.transform.position.x + 2f), this.transform.position.y, Random.Range (this.transform.position.z, this.transform.position.z + 2f)), Quaternion.identity);
        Bot.SetInteger ("change", 1);
        return test;
    }
    public GameObject FindClosestEnemy (string target) {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag (target);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        for (int i = 0; i < gos.Length; i++) {
            Vector3 diff = gos[i].transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = gos[i];
                distance = curDistance;
            }

        }

        return closest;
    }

    void Update () {
        if (this.gameObject.tag == "carrier") {
           
            agent.radius = 5.3f;
            if (next == false) {
                agent.SetDestination (FindClosestEnemy ("target").transform.position);
                Vector3 direction = (FindClosestEnemy ("target").transform.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation (direction);
                Bot.SetInteger ("change", 1);
            } else {
                if (switch_side == 1) {
                    agent.SetDestination (goal1.transform.position);
                    Vector3 direction = (goal1.transform.position - transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation (direction);
                    Bot.SetInteger ("change", 1);
                } else if (switch_side == 2) {
                    agent.SetDestination (goal2.transform.position);
                    Vector3 direction = (goal2.transform.position - transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation (direction);
                    Bot.SetInteger ("change", 1);
                }
            }
        }
        else if (GameObject.Find ("carrier") != null) {
            agent.radius = 3.3f;
            Ray ray = new Ray (transform.position + new Vector3 (0f, 1f, 0f), transform.forward);
            Quaternion spreadAngle1 = Quaternion.AngleAxis (-90, new Vector3 (0, 1, 0));
            Quaternion spreadAngle2 = Quaternion.AngleAxis (-45, new Vector3 (0, 1, 0));
            Quaternion spreadAngle3 = Quaternion.AngleAxis (-22.5f, new Vector3 (0, 1, 0));
            Quaternion spreadAngle4 = Quaternion.AngleAxis (22.5f, new Vector3 (0, 1, 0));
            Quaternion spreadAngle5 = Quaternion.AngleAxis (45, new Vector3 (0, 1, 0));
            Quaternion spreadAngle6 = Quaternion.AngleAxis (90, new Vector3 (0, 1, 0));
            Vector3 newVector1 = spreadAngle1 * (transform.forward);
            Vector3 newVector2 = spreadAngle2 * (transform.forward);
            Vector3 newVector3 = spreadAngle3 * (transform.forward);
            Vector3 newVector4 = spreadAngle4 * (transform.forward);
            Vector3 newVector5 = spreadAngle5 * (transform.forward);
            Vector3 newVector6 = spreadAngle6 * (transform.forward);
            Ray ray1 = new Ray (transform.position + new Vector3 (0f, 1f, 0f), newVector1 * longray);
            Ray ray2 = new Ray (transform.position + new Vector3 (0f, 1f, 0f), newVector2 * longray);
            Ray ray3 = new Ray (transform.position + new Vector3 (0f, 1f, 0f), newVector3 * longray);
            Ray ray4 = new Ray (transform.position + new Vector3 (0f, 1f, 0f), newVector4 * longray);
            Ray ray5 = new Ray (transform.position + new Vector3 (0f, 1f, 0f), newVector5 * longray);
            Ray ray6 = new Ray (transform.position + new Vector3 (0f, 1f, 0f), newVector6 * longray);
            Debug.DrawRay (transform.position + new Vector3 (0f, 1f, 0f), newVector1 * longray, Color.red);
            Debug.DrawRay (transform.position + new Vector3 (0f, 1f, 0f), newVector2 * longray, Color.red);
            Debug.DrawRay (transform.position + new Vector3 (0f, 1f, 0f), newVector3 * longray, Color.red);
            Debug.DrawRay (transform.position + new Vector3 (0f, 1f, 0f), newVector4 * longray, Color.red);
            Debug.DrawRay (transform.position + new Vector3 (0f, 1f, 0f), newVector5 * longray, Color.red);
            Debug.DrawRay (transform.position + new Vector3 (0f, 1f, 0f), newVector6 * longray, Color.red);
            RaycastHit hit;
            Debug.DrawRay (transform.position + new Vector3 (0f, 1f, 0f), transform.TransformDirection (Vector3.forward) * longray, Color.black);
            if ((Physics.Raycast (ray, out hit, longray)) || (Physics.Raycast (ray1, out hit, longray)) || (Physics.Raycast (ray2, out hit, longray)) || (Physics.Raycast (ray3, out hit, longray)) || (Physics.Raycast (ray4, out hit, longray)) || (Physics.Raycast (ray5, out hit, longray)) || (Physics.Raycast (ray6, out hit, longray))) {

                if (hit.collider.gameObject.tag == ("carrier")) {
                    if (hit.collider.gameObject.layer == 9) {
                        hit.collider.gameObject.name = hit.collider.gameObject.GetComponent<bot> ().bot_name;
                        hit.collider.gameObject.tag = "enemy";
                        hit.collider.gameObject.GetComponent<bot> ().ball.SetActive (false);
                        hit.collider.gameObject.GetComponent<bot> ().next = false;
                        hit.collider.gameObject.GetComponent<bot> ().switch_side = 0;
                        Bot.SetInteger ("change", 2);
                    } else if (hit.collider.gameObject.layer == 10) {
                        if (hit.collider.gameObject.GetComponent<player> ().breakclicked == false) {
                            hit.collider.gameObject.name = "Player";
                            hit.collider.gameObject.tag = "Player";
                            hit.collider.gameObject.GetComponent<player> ().ball.SetActive (false);
                            Bot.SetInteger ("change", 2);
                        }
                    }
                } else {
                    Bot.SetInteger ("change", 1);
                }
            }
            agent.SetDestination (FindClosestEnemy ("carrier").transform.position);
            Vector3 direction = (FindClosestEnemy ("carrier").transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation (direction);
            transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        } else if (GameObject.Find ("ball(Clone)") != null) {
            agent.radius = 3.3f;
            agent.SetDestination (FindClosestEnemy ("ball").transform.position);
            Vector3 direction = (FindClosestEnemy ("ball").transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation (direction);
            transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            Bot.SetInteger ("change", 1);

        }
    }
    void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.tag == "ball") {
            ball.SetActive (true);
            this.gameObject.name = "carrier";
            this.gameObject.tag = "carrier";
            Destroy (collision.gameObject);
        }

    }
    void OnTriggerEnter (Collider other) {
        if (ball.activeSelf == true) {
            if (other.gameObject.tag == "target") {
                other.gameObject.tag = "Untagged";
                StartCoroutine (reset (other.gameObject));
                next = true;
                Score += 1;
                ss.text = bot_name + ": " + Score.ToString ();
                if (other.gameObject.name == "target1") {
                    switch_side = 2;
                } else if (other.gameObject.name == "target2") {
                    switch_side = 1;
                }
            }
        }
    }
    IEnumerator reset (GameObject test) {
        yield return new WaitForSeconds (2f);
        test.tag = "target";

    }

}