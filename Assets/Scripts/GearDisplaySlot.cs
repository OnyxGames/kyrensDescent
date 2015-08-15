using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GearDisplaySlot : MonoBehaviour, IPointerDownHandler, IDragHandler {
	public int index;
	public Item item;
	public GearDisplay characterScript;
	Inventory inventory;
	Image iconImage;
	Sprite iconSprite;

	// Use this for initialization
	void Start () {
		item = new Item ();
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		iconImage = transform.GetChild (0).GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Item in gear slot: " + item.itemType);
		if (item.itemType != Item.ItemType.None) {
			iconImage.enabled = true;
			iconImage.sprite = item.itemIcon;
		}
		else {
			iconImage.enabled = false;
		}
	
	}

	public void OnPointerDown(PointerEventData data) {
		Debug.Log ("ON_POINTER_DOWN() IN CHARACTER SLOT");
		Debug.Log ("Dragging an item? " + inventory.draggingItem);
		if(inventory.draggingItem) {
			Debug.Log ("index = " + index);
			Debug.Log ("itemType = " + inventory.draggedItem.itemType);
			Debug.Log("ITem type == helmet? " + (inventory.draggedItem.itemType == Item.ItemType.Helmet));
			
			if(index == 0 && inventory.draggedItem.itemType == Item.ItemType.Helmet) {
				addItemToGearPanel();
			}

			else if(index == 1 && inventory.draggedItem.itemType == Item.ItemType.Chest) {
				addItemToGearPanel();
			}

			else if(index == 2 && inventory.draggedItem.itemType == Item.ItemType.Weapon) {
				addItemToGearPanel();
			}

			else if(index == 3 && inventory.draggedItem.itemType == Item.ItemType.Legs) {
				addItemToGearPanel();
			}


		}


	}

	public void addItemToGearPanel() {
		if(item.itemType != Item.ItemType.None) {
			Item temp = item;
			item = inventory.draggedItem;
			inventory.draggedItem = temp;
			inventory.showDraggedItem(temp, -1);
		}
		
		else {
			item = inventory.draggedItem;
			inventory.disableDraggedItem();
		}
	}

	public void OnDrag(PointerEventData data) {
		if (item.itemType != Item.ItemType.None) {
			Debug.Log ("ON_DRAG IN GEAR_DISPLAY_SLOT.CS");
			Debug.Log ("Item being dragged: " + inventory.draggedItem.itemType);
			Debug.Log ("Item in gear slot: " + item.itemType);
			inventory.showDraggedItem (item, -1);
			item = new Item ();
		}
	}
}


