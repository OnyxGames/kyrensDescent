using UnityEngine;
using System.Collections;

public class DestructionPopUp : MonoBehaviour {

	public Inventory inventory;
	public GearDisplay gearDisplay;

	// Use this for initialization
	void Start () {
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();	
		gearDisplay = GameObject.FindGameObjectWithTag ("GearDisplay").GetComponent<GearDisplay>();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("DestroyItemButton")) {
			if (inventory.draggedItem.numItems <= 0) {
				if (inventory.draggedItemIndex >= 0) {
					inventory.Items [inventory.draggedItemIndex] = new Item ();
					inventory.disableDraggedItem ();
				} else {
					if (inventory.draggedItem.itemType == Item.ItemType.Helmet) {
						gearDisplay.gearSlots [GearDisplay.HELMET_INDEX].item = new Item ();
						inventory.disableDraggedItem ();
						
					} else if (inventory.draggedItem.itemType == Item.ItemType.Chest) {
						gearDisplay.gearSlots [GearDisplay.CHEST_INDEX].item = new Item ();
						inventory.disableDraggedItem ();
					} else if (inventory.draggedItem.itemType == Item.ItemType.Weapon) {
						gearDisplay.gearSlots [GearDisplay.WEAPON_INDEX].item = new Item ();
						inventory.disableDraggedItem ();				
					} else if (inventory.draggedItem.itemType == Item.ItemType.Legs) {
						gearDisplay.gearSlots [GearDisplay.LEGS_INDEX].item = new Item ();
						inventory.disableDraggedItem ();				
					}
				}
				
			}
			
			this.gameObject.SetActive(false);
		} 

		else if (Input.GetMouseButtonDown(0)){
			this.gameObject.SetActive(false);
		}

		Debug.Log ("Clicked? " + Input.GetMouseButtonDown (0));
	
	}
}
