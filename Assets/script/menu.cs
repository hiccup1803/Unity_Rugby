using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menu : MonoBehaviour {
    public GameObject _menu;
    public GameObject setting;
    public GameObject loading;
    public GameObject exit;

    // Start is called before the first frame update
    public void Start () {
        _menu.SetActive (true);
        setting.SetActive (false);
        loading.SetActive (false);
        exit.SetActive (false);
    }

    // Update is called once per frame
    public void Update () {

    }
    public void show (GameObject _show) {
        _show.SetActive (true);
        _menu.SetActive (false);

    }
    public void hide (GameObject _hide) {
        _menu.SetActive (true);
        _hide.SetActive (false);

    }
    public void Openscene (int scenenumber) {
        SceneManager.LoadScene (scenenumber);
    }
    public void Exit () {
        Application.Quit ();
    }
}