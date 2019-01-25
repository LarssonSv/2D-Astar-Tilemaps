using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {

    public GameObject currentCanvas;


    public void OpenCanvas (GameObject x) {

        currentCanvas.SetActive(false);
        currentCanvas = x;
        x.SetActive(true);

    }



}
