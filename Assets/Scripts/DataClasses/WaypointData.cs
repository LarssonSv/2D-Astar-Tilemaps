using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointData : MonoBehaviour  {

    public string npcName;
    public Transform pos;
    public bool exit;

    private void Awake() {

        pos = transform;

    }

}
