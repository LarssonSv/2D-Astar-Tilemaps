using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ItemSlot : MonoBehaviour {

    Text itemName;
    Image itemSprite;

    public int slotIndex = 0;
 

    private void Start() {
        itemSprite = GetComponent<Image>();
        itemName = GetComponentInChildren<Text>();
        
    }

    public void RefreshSlot() {

        if (PlayerInventory.playerInv.invItems.Count > slotIndex) {

            ItemData x = PlayerInventory.playerInv.invItems[slotIndex];
            if (x != null) {
                itemSprite.sprite = x.itemSprite;
                itemName.text = x.itemName;
            }
        }
        else {
            itemName.text = null;
            itemSprite.sprite = null;
        }
    }

    public void SetEmpty() {

        itemName.text = "";
        itemSprite.sprite = null;


    }






}
