using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventory : MonoBehaviour
{

    public static PlayerInventory playerInv;

    [SerializeField]
    int invSize = 20;

    List<ItemSlot> slots = new List<ItemSlot>();
    public List<ItemData> invItems = new List<ItemData>();

    public ItemData tempItem;

    private void Start() {

        playerInv = this;

        GameObject[] temp = GameObject.FindGameObjectsWithTag("slot");
        foreach(GameObject i in temp) {
            slots.Add(i.GetComponent<ItemSlot>());
        }

    }

    private void PrimeSlots() {

        foreach(ItemSlot i in slots) {

            i.RefreshSlot();

        }

    }

    public void AddItem(ItemData x) {

        if(invItems.Count < invSize) { 
        invItems.Add(x);
        PrimeSlots();
        }
        else {
            Debug.Log("No space left!");
        }
    }

    public void RemoveItem(ItemData x) {

        if (invItems.Count > 0) {
            invItems.Remove(x);
            PrimeSlots();
        }
        else {
            Debug.Log("Nothing to remove!");
        }
    }





    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {

            AddItem(tempItem);
           

        }
        else if (Input.GetKeyDown(KeyCode.K)) {

            RemoveItem(tempItem);

        }
    }



}
