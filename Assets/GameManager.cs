using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

    [HideInInspector] public List<WaypointData> exits = new List<WaypointData>();
    [HideInInspector] public List<WaypointData> attractions = new List<WaypointData>();
    [HideInInspector] private List<GameObject> currentNpcs = new List<GameObject>();
    [HideInInspector] public int coins;
    [HideInInspector] public TextMeshProUGUI coinsText;
    [HideInInspector] public TextMeshProUGUI visitorsText;

    [Range(0.0f, 100.0f)]
    public float npcSpawnRate = 1f;

    public static GameManager GM;
    public GameObject npcPrefab;

    private void Awake() {

        coinsText = GameObject.FindGameObjectWithTag("coins").GetComponent<TextMeshProUGUI>();
        visitorsText = GameObject.FindGameObjectWithTag("visitors").GetComponent<TextMeshProUGUI>();

    }


    private void Start () {
        GameObject[] tempExit = GameObject.FindGameObjectsWithTag("exit");
        foreach(GameObject x in tempExit) {
          
            exits.Add(x.GetComponent<WaypointData>());

        }

        GameObject[] tempAttractions = GameObject.FindGameObjectsWithTag("attraction");
        foreach (GameObject y in tempAttractions) {
            attractions.Add(y.GetComponent<WaypointData>());
        }

        GM = this;


        coins = 10;
        RefreshCoins();
        NpcSpawn();
        SetNpcSpawn(true);


    }

    public void AddCoins(int x) {

        coins += x;

    }

    public void RefreshVisitors() {

        visitorsText.text = currentNpcs.Count.ToString();

    }

    public bool SubtractCoins (int x) {

        int copy = coins;

        if((copy - x) >= 0) {
            coins -= x;
            return true;
        }
        return false;
    }

    public void SetNpcSpawn(bool x) {
        if (x == true) {
            InvokeRepeating("NpcSpawn", 1f, 2f);
        }
        else {
            CancelInvoke("NpcSpawn");
        }

    }

    public void RefreshCoins() {
        coinsText.text = coins.ToString();
    }

    private void NpcSpawn() {
    
        if (Random.Range(0.0f, 100.0f) <= npcSpawnRate) {
            int currentExit = Random.Range(0, exits.Count);
            GameObject npc = Instantiate(npcPrefab, exits[currentExit].pos.position, exits[currentExit].pos.rotation);

            npc.transform.position = exits[currentExit].pos.position;


            currentNpcs.Add(npc);
            RefreshVisitors();
        }

    }


    

    
	
	
}
