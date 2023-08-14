using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loading : MonoBehaviour {
    void Openscene (int scenenumber) {
        SceneManager.LoadScene (scenenumber);
    }

}