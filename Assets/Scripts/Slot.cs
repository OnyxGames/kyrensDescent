using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler {

	public Item item;
	Image itemImage;
	public int slotNumber;
	Inventory inventory;
	public GameObject itemAmountObject;
	Text itemAmountText;

	// Use this for initialization
	void Start () {
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		itemImage = gameObject.transform.GetChild (0).GetComponent<Image> ();
		//itemAmountObject = this.gameObject.transform.GetChild (1);
		itemAmountText = gameObject.transform.GetChild (1).GetComponent<Text> ();
		itemAmountText.enabled = false;
		//itemAmountObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (inventory.Items[slotNumber].itemName != null) {

			item = inventory.Items[slotNumber];
			itemImage.enabled = true;
			itemImage.sprite = inventory.Items[slotNumber].itemIcon;

			if(inventory.Items[slotNumber].itemType  == Item.ItemType.Consumeable || inventory.Items[slotNumber].itemType == Item.ItemType.Coin) {
				//itemAmountObject.SetActive(true
				itemAmountText.enabled = true;
				itemAmountText.text = "" + inventory.Items[slotNumber].numItems;
			}

			gameObject.transform.GetChild (0).GetComponent<Image> ().sprite = itemImage.sprite;

		} 

		else {
			itemImage.enabled = false;
		}

	}

	public void OnPointerDown(PointerEventData data) {
		Debug.Log ("Clicked " + name);
		if (inventory.draggingItem) {
				if (inventory.Items [slotNumber].itemName != null) {				//If there's already an item in the slot being clicked on		
					Debug.Log("draggedItemIndex = " + inventory.draggedItemIndex);
					if(inventory.draggedItemIndex >= 0) {							//If the item being dragged is from another inventory slot
						inventory.Items [inventory.draggedItemIndex] = inventory.Items [slotNumber];
						inventory.Items [slotNumber] = inventory.draggedItem;
						itemAmountText.enabled = false;
						inventory.disableDraggedItem ();
					}

					else {															//If the item being dragged is from a gear slot
						Item temp = inventory.draggedItem;
						inventory.draggedItem = inventory.Items[slotNumber];
						inventory.Items[slotNumber] = temp;
						inventory.showDraggedItem(inventory.draggedItem, slotNumber);
					}

				} 

				else if (inventory.Items [slotNumber].itemName == null) {			//If the slot being clicked on is empty
					inventory.Items [slotNumber] = inventory.draggedItem;
					inventory.disableDraggedItem ();
				}

				

		}

		else if (data.button == PointerEventData.InputButton.Right) {
			if(inventory.Items [slotNumber].itemType  == Item.ItemType.Consumeable) {
				--inventory.Items[slotNumber].numItems;
				if(inventory.Items[slotNumber].numItems == 0) {
					inventory.Items[slotNumber] = new Item();
					itemAmountText.enabled = false;
					inventory.closeToolTip();
				}
			}
		}
	}

	public void OnPointerEnter(PointerEventData data) {
		if (inventory.Items[slotNumber].itemName != null) {			
			inventory.showToolTip(inventory.SlotsList[slotNumber].GetComponent<RectTransform>().localPosition, inventory.Items[slotNumber]);
		}

		if (inventory.draggingItem) {
			inventory.closeToolTip();
		}
	}

	public void OnPointerExit(PointerEventData data) {
		if (inventory.Items[slotNumber].itemName != null) {
			inventory.closeToolTip ();
		}
	}

	public void OnDrag(PointerEventData data) {
		Debug.Log ("ON_DRAG()");
		Debug.Log ("Condition: " + inventory.Items [slotNumber].itemName != null);

		if (inventory.Items [slotNumber].itemName != null) {
			inventory.showDraggedItem(inventory.Items[slotNumber], slotNumber);
			inventory.Items[slotNumber] = new Item(); 
			itemAmountText.enabled = false;
		}
	}
}
