using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class BuildData : MonoBehaviour
{
    public int x = 0;
    public int y = 0;
    public int cost = 0;
    public string buildingName = "Give a name";


    public int sum {
        get {
            return x * y;
        }
    }

}
