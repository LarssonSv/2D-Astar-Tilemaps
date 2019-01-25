using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public void Load(int x) {
        SceneManager.LoadScene(x);
        Debug.Log("Loaded! Scene: " + x);
    }



}
