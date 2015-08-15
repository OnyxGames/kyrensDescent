using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DeleteItemZone : MonoBehaviour, IPointerDownHandler {

	Inventory inventory;
	public GameObject destructionPopUp;
	string destructionQuestion;
	Button keepItemButton, destroyItemButton;

	// Use this for initialization
	void Awake () {
		destructionPopUp = GameObject.FindGameObjectWithTag ("DestructionPopUp");
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		destructionPopUp.SetActive (false);
		destroyItemButton = destructionPopUp.transform.GetChild (1).GetComponent<Button> ();
		keepItemButton = destructionPopUp.transform.GetChild (2).GetComponent<Button> ();
		/*
		popUpButtonsStyle.name = "destructionPopUpButton";
		popUpButtonsStyle.alignment = TextAnchor.MiddleCenter;
		popUpButtonsStyle.wordWrap = true;
		popUpButtonsStyle.fontSize = 14;
		popUpButtonsStyle.clipping = TextClipping.Clip;	
		*/
	}

	public void OnPointerDown(PointerEventData data) {
		Item draggedItem = inventory.draggedItem;
		string popUpTxt = ("Do you want to destroy 1 " + draggedItem.itemName + "?");
		Debug.Log ("ON_POINTER_DOWN in DeleteItemZone script");
		Debug.Log ("Dragging an item?  " + inventory.draggingItem);
		if (inventory.draggingItem) {
			Debug.Log (draggedItem);
			destructionPopUp.transform.GetChild (0).GetComponent<Text>().text = ("Do you want to destroy 1 " + draggedItem.itemName + "?");
			destructionPopUp.transform.GetChild (3).GetComponent<Image>().sprite = draggedItem.itemIcon;
			destructionPopUp.SetActive(true);

			//If destroy button pressed, destroy one of item

			//Else, if anything else is pressed, disable popup and continue on


/*			Debug.Log ("destroyItemButton == true?  " + destroyItemButton == true);

			if(GUILayout.Button(popUpTxt, popUpButtonsLayout, )) {
				--inventory.draggedItem.numItems;
				if(inventory.draggedItem.numItems <= 0) {
					inventory.Items[inventory.draggedItemIndex] = new Item();
					inventory.disableDraggedItem();
				}
				
				destructionPopUp.SetActive(false);
			}
			

			if(destructionPopUp.transform.GetChild (2).GetComponent<Button> ()) {
				destructionPopUp.SetActive(false);
			}
			*/


		}
	}
	
}
