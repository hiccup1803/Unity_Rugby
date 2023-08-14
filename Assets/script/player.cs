using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour {
    private Joystick joystick;
    private Rigidbody rb;
    public int movementSpeed;
    public Animator main;
    private Vector3 mouvement = Vector3.zero;
    public GameObject prefab;
    public int Score;
    public Text ss;
    public GameObject ball;
    public float longray = 1f;
    public bool attack;
    public bool prepare;
    public Button tackle;
    public bool breakclicked;
    public Button break_tackle;
    public bool test_break;
    // Start is called before the first frame update
    void Start () {
        test_break = false;
        Button btn1 = tackle.GetComponent<Button> ();
        btn1.onClick.AddListener (TackleOnClick);
        breakclicked = false;

        main = this.gameObject.GetComponent<Animator> ();
        joystick = FindObjectOfType<Joystick> ();
        rb = GetComponent<Rigidbody> ();
    }
       
    void TackleOnClick () {
         Ray ray = new Ray (transform.position + new Vector3 (0f, 1f, 0f), transform.forward);
        Quaternion spreadAngle1 = Quaternion.AngleAxis(-90, new Vector3(0, 1, 0));
        Quaternion spreadAngle2 = Quaternion.AngleAxis(-45, new Vector3(0, 1, 0));
        Quaternion spreadAngle3 = Quaternion.AngleAxis(-22.5f, new Vector3(0, 1, 0));
        Quaternion spreadAngle4 = Quaternion.AngleAxis(22.5f, new Vector3(0, 1, 0));
        Quaternion spreadAngle5 = Quaternion.AngleAxis(45, new Vector3(0, 1, 0));
        Quaternion spreadAngle6 = Quaternion.AngleAxis(90, new Vector3(0, 1, 0));
        Vector3 newVector1 = spreadAngle1 * (transform.forward);
        Vector3 newVector2 = spreadAngle2 * (transform.forward);
        Vector3 newVector3 = spreadAngle3 * (transform.forward);
        Vector3 newVector4 = spreadAngle4 * (transform.forward);
        Vector3 newVector5 = spreadAngle5 * (transform.forward);
        Vector3 newVector6 = spreadAngle6 * (transform.forward);
        Ray ray1= new Ray (transform.position+ new Vector3 (0f, 1f, 0f), newVector1*1.5f);
        Ray ray2= new Ray (transform.position+ new Vector3 (0f, 1f, 0f), newVector2*1.5f);
        Ray ray3= new Ray (transform.position+ new Vector3 (0f, 1f, 0f), newVector3*1.5f);
        Ray ray4= new Ray (transform.position+ new Vector3 (0f, 1f, 0f), newVector4*1.5f);
        Ray ray5= new Ray (transform.position+ new Vector3 (0f, 1f, 0f), newVector5*1.5f);
        Ray ray6= new Ray (transform.position+ new Vector3 (0f, 1f, 0f), newVector6*1.5f);
        Debug.DrawRay (transform.position+ new Vector3 (0f, 1f, 0f), newVector1*1.5f,Color.red);
        Debug.DrawRay (transform.position+ new Vector3 (0f, 1f, 0f), newVector2*1.5f,Color.red);
        Debug.DrawRay (transform.position+ new Vector3 (0f, 1f, 0f), newVector3*1.5f,Color.red);
        Debug.DrawRay (transform.position+ new Vector3 (0f, 1f, 0f), newVector4*1.5f,Color.red);
        Debug.DrawRay (transform.position+ new Vector3 (0f, 1f, 0f), newVector5*1.5f,Color.red);
        Debug.DrawRay (transform.position+ new Vector3 (0f, 1f, 0f), newVector6*1.5f,Color.red);
        RaycastHit hit;
        Debug.DrawRay (transform.position + new Vector3 (0f, 1f, 0f), transform.TransformDirection (Vector3.forward) * 1.5f, Color.black);
        if ((Physics.Raycast (ray, out hit, 1.5f))||(Physics.Raycast (ray1, out hit, 1.5f))||(Physics.Raycast (ray2, out hit, 1.5f))||(Physics.Raycast (ray3, out hit, 1.5f))||(Physics.Raycast (ray4, out hit, 1.5f))||(Physics.Raycast (ray5, out hit, 1.5f))||(Physics.Raycast (ray6, out hit, 1.5f))){
         
            if (hit.collider.gameObject.tag == ("carrier")) {
                attack = true;
                hit.collider.gameObject.tag = "enemy";
                hit.collider.gameObject.GetComponent<bot> ().ball.SetActive (false);
                hit.collider.gameObject.GetComponent<bot> ().next = false;
                hit.collider.gameObject.GetComponent<bot> ().switch_side = 0;
                hit.collider.gameObject.name = hit.collider.gameObject.GetComponent<bot> ().bot_name;
            }
        }
    }

    public void Wrapper () {
        if (ball.activeSelf == true) {
            breakclicked = true;
            StartCoroutine (clicking ());
        }
    }
    public GameObject instantiate_ball () {
        GameObject test = null;
        test = Instantiate (prefab, new Vector3 (Random.Range (this.transform.position.x, this.transform.position.x + 1f), this.transform.position.y, Random.Range (this.transform.position.z, this.transform.position.z + 6f)), Quaternion.identity);
        main.SetInteger ("change", 1);
        attack = false;
        prepare = false;
        return test;
    }

    void FixedUpdate () {
        Ray ray = new Ray (transform.position + new Vector3 (0f, 1f, 0f), -transform.forward);
        Quaternion spreadAngle1 = Quaternion.AngleAxis(-90, new Vector3(0, 1, 0));
        Quaternion spreadAngle2 = Quaternion.AngleAxis(-45, new Vector3(0, 1, 0));
        Quaternion spreadAngle3 = Quaternion.AngleAxis(-22.5f, new Vector3(0, 1, 0));
        Quaternion spreadAngle4 = Quaternion.AngleAxis(22.5f, new Vector3(0, 1, 0));
        Quaternion spreadAngle5 = Quaternion.AngleAxis(45, new Vector3(0, 1, 0));
        Quaternion spreadAngle6 = Quaternion.AngleAxis(90, new Vector3(0, 1, 0));
        Vector3 newVector1 = spreadAngle1 * (-transform.forward);
        Vector3 newVector2 = spreadAngle2 * (-transform.forward);
        Vector3 newVector3 = spreadAngle3 * (-transform.forward);
        Vector3 newVector4 = spreadAngle4 * (-transform.forward);
        Vector3 newVector5 = spreadAngle5 * (-transform.forward);
        Vector3 newVector6 = spreadAngle6 * (-transform.forward);
        Ray ray1= new Ray (transform.position+ new Vector3 (0f, 1f, 0f), newVector1*1.5f);
        Ray ray2= new Ray (transform.position+ new Vector3 (0f, 1f, 0f), newVector2*1.5f);
        Ray ray3= new Ray (transform.position+ new Vector3 (0f, 1f, 0f), newVector3*1.5f);
        Ray ray4= new Ray (transform.position+ new Vector3 (0f, 1f, 0f), newVector4*1.5f);
        Ray ray5= new Ray (transform.position+ new Vector3 (0f, 1f, 0f), newVector5*1.5f);
        Ray ray6= new Ray (transform.position+ new Vector3 (0f, 1f, 0f), newVector6*1.5f);
        Debug.DrawRay (transform.position+ new Vector3 (0f, 1f, 0f), newVector1*1.5f,Color.red);
        Debug.DrawRay (transform.position+ new Vector3 (0f, 1f, 0f), newVector2*1.5f,Color.red);
        Debug.DrawRay (transform.position+ new Vector3 (0f, 1f, 0f), newVector3*1.5f,Color.red);
        Debug.DrawRay (transform.position+ new Vector3 (0f, 1f, 0f), newVector4*1.5f,Color.red);
        Debug.DrawRay (transform.position+ new Vector3 (0f, 1f, 0f), newVector5*1.5f,Color.red);
        Debug.DrawRay (transform.position+ new Vector3 (0f, 1f, 0f), newVector6*1.5f,Color.red);
        RaycastHit hit;
        Debug.DrawRay (transform.position + new Vector3 (0f, 1f, 0f), transform.TransformDirection (-Vector3.forward) * 1.5f, Color.black);
        if ((Physics.Raycast (ray, out hit, 1.5f))||(Physics.Raycast (ray1, out hit, 1.5f))||(Physics.Raycast (ray2, out hit, 1.5f))||(Physics.Raycast (ray3, out hit, 1.5f))||(Physics.Raycast (ray4, out hit, 1.5f))||(Physics.Raycast (ray5, out hit, 1.5f))||(Physics.Raycast (ray6, out hit, 1.5f))){
            if (hit.collider.gameObject.tag == ("enemy")) {
                test_break = true;
            } 
        }else{
            test_break = false;
        }
        

        mouvement = new Vector3 (joystick.Vertical * movementSpeed * Time.deltaTime, 0.0f, -joystick.Horizontal * movementSpeed * Time.deltaTime);
        if (attack == false) {

            if ((test_break == true) && (breakclicked == true)) {
                transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (-mouvement), 5.25f);
                rb.MovePosition (transform.position - mouvement * Time.deltaTime);
                main.SetInteger ("change", 4);
            } else {
                if (mouvement != Vector3.zero) {
                    transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (-mouvement), 5.25f);
                    rb.MovePosition (transform.position - mouvement * Time.deltaTime);
                    main.SetInteger ("change", 1);
                } else {
                    main.SetInteger ("change", 0);
                }
            }
        } else {
            main.SetInteger ("change", 2);
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
                Score += 1;
                ss.text = "Player : " + Score.ToString ();

            }
        }
    }
    IEnumerator reset (GameObject test) {
        yield return new WaitForSeconds (2f);
        test.tag = "target";

    }
    private IEnumerator clicking () {
        yield return new WaitForSeconds (2f);
        breakclicked = false;
    }
}