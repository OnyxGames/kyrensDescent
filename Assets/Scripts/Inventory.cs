using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	public List<GameObject> SlotsList = new List<GameObject>();
	public List<Item> Items = new List<Item> ();

	ItemDatabase database;

	public GameObject slots;
	public GameObject toolTip;
	public GameObject draggedItemGameObject;
	public Item draggedItem;
	public int draggedItemIndex;

	public bool draggingItem = false;
	float xOffsetDraggedItem = 15;					//Offsets dragged item icon from cursor
	float yOffsetDraggedItem = 15;
		
	int slotRow = 1;
	int slotCol = 1;
	int slotAmt = 0;

	Transform slotsTransform;
	RectTransform slotsRectTransform;
	Transform inventoryTransform;
	RectTransform inventoryRectTransform;

	//Allows the programmer to choose the number of rows and columns of slots
	Vector3 INVENTORY_TOP_LEFT_CORNER;      	//Corner of inventory
	Vector3 STARTING_POST;				//Stores starting position of slots
	float SLOT_WIDTH;							
	float SLOT_HEIGHT; 	
	int numCols;						//Hard-code desired value in this var (in Start() )
	int numRows;						//Hard-code desired value in this var (in Start() )					
	float HOR_INCREMENT;
	float VER_INCREMENT;

	float xPos;						
	float yPos;
	const float ZPOS = 0;

	float INVENTORY_WIDTH;
	float INVENTORY_HEIGHT;

	float TOOL_TIP_VERT_OFFSET_AMT; 			//Amt of space between inventory slot and tool tip




	//const int NUM_COLUMNS = (float) (INVENTORY_WIDTH - GAP) / (SLOT_WIDTH + GAP);		
	//const int NUM_ROWS = (float)(INVENTORY_WIDTH - GAP) / (SLOT_HEIGHT + GAP);


	// Use this for initialization
	void Start () {
		database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase> ();

		//Vars to determine number and spacing of slots
		int numCols = 3;
	    int numRows = 4;
		float horGap;
		float verGap;

		float minInventoryXCoord;
		float maxInventoryYCoord;

		int numHorGaps = numCols + 1;
		int numVerGaps = numRows + 1;	

		inventoryRectTransform = gameObject.GetComponent<RectTransform> (); 
		slotsRectTransform = slots.GetComponent<RectTransform> ();
			
		SLOT_WIDTH = slotsRectTransform.rect.width;
		SLOT_HEIGHT = slotsRectTransform.rect.height;
		INVENTORY_WIDTH = inventoryRectTransform.rect.width;
		INVENTORY_HEIGHT = inventoryRectTransform.rect.height;

		float upperLeftCornerXCoord = inventoryRectTransform.position.x - (INVENTORY_WIDTH / 2);
		float upperLeftCornerYCoord = inventoryRectTransform.position.y + (INVENTORY_HEIGHT / 2);
		INVENTORY_TOP_LEFT_CORNER= new Vector3 (upperLeftCornerXCoord, upperLeftCornerYCoord, ZPOS);
		
		horGap = (INVENTORY_WIDTH - (SLOT_WIDTH * numCols)) / numHorGaps;

		if (horGap <= 0) {															//If too many columns, decrease number until appropriate number selected
			while(horGap < 0) {
				--numCols;
				--numHorGaps;
				horGap = (INVENTORY_WIDTH - (SLOT_WIDTH * (numCols))) / (numHorGaps);
			}
		}


		verGap = (INVENTORY_HEIGHT - (SLOT_HEIGHT * (numRows))) / (numVerGaps);
		if (verGap <= 0) {															//If too many rows, decrease number until appropriate number selected
			while(verGap <= 0) {
				--numRows;
				--numVerGaps;
				verGap = (INVENTORY_HEIGHT - (SLOT_HEIGHT * (numRows))) / (numVerGaps);
			}
		}
		
		TOOL_TIP_VERT_OFFSET_AMT = -(SLOT_WIDTH + horGap);

		//Set position of first slot to ensure rest are placed properly
		/*
		minInventoryXCoord = inventoryTransform.position.x -(INVENTORY_WIDTH / 2) + (SLOT_WIDTH / 2);				//Assumes pivot point is in center of inventory
		maxInventoryYCoord = inventoryTransform.position.y -(INVENTORY_HEIGHT / 2) + (SLOT_HEIGHT / 2);				//Assumes pivot point is in center of inventory


		INVENTORY_TOP_LEFT_CORNER = new Vector3 (minInventoryXCoord, maxInventoryYCoord, ZPOS);
		*/

		Vector3 STARTING_POS = new Vector3 (INVENTORY_TOP_LEFT_CORNER.x + (SLOT_WIDTH / 2) + horGap, INVENTORY_TOP_LEFT_CORNER.y - (SLOT_HEIGHT / 2) - verGap, ZPOS);

		xPos = STARTING_POS.x;
		yPos = STARTING_POS.y;

		HOR_INCREMENT = SLOT_WIDTH + horGap;
		VER_INCREMENT = SLOT_HEIGHT + verGap;

		int i = 0;
		float heightTracker = VER_INCREMENT;

		while (i < numRows && heightTracker < INVENTORY_HEIGHT) {
			int j = 0;
			float widthTracker = HOR_INCREMENT;

			while(j < numCols && widthTracker < INVENTORY_WIDTH) {
				GameObject slot = (GameObject) Instantiate(slots);
				slot.GetComponent<Slot>().slotNumber = slotAmt;
				SlotsList.Add(slot);

				Items.Add (new Item()); 								
				slot.transform.SetParent(GameObject.FindObjectOfType<Inventory>().gameObject.transform, false);
				slot.GetComponent<RectTransform>().position = new Vector3(xPos, yPos, ZPOS);
				xPos += (HOR_INCREMENT);

				slot.name = "slot " + slotRow + "." + slotCol;

				Debug.Log (slot.name);
				Debug.Log (slot.GetComponent<RectTransform>().position);

				++slotCol;
				++slotAmt;
				widthTracker += HOR_INCREMENT;
			}
			xPos = STARTING_POS.x;
			yPos -= (VER_INCREMENT);
			slotCol = 1;
			++slotRow;
			heightTracker += VER_INCREMENT;
		}


		addItem (0);
		addItem (1);
		addItem (2);
		addItem (2);
	}

	void Update() {
		if (draggingItem) {
			Vector3 draggedItemPos = (Input.mousePosition - GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>().localPosition);
			draggedItemGameObject.GetComponent<RectTransform>().localPosition = new Vector3(draggedItemPos.x + xOffsetDraggedItem, draggedItemPos.y + yOffsetDraggedItem, ZPOS);;
		}
	}

	void addItem(int id) {
		for (int i = 0; i < database.items.Count; i++) {
			if (database.items [i].itemId == id) {
				Item item = database.items [i];

				if(database.items [i].itemType  == Item.ItemType.Consumeable || database.items[i].itemType == Item.ItemType.Coin) {
					checkIfItemExists(id, item);
				}

				else {
					addItemAtEmptySlot(item);
				}

				break;	
			}
		}
	}
		/*
		int i = 0;

		while (i < database.items.Count && database.items[i].itemId != id) {
			i++;
		}

		if(database.items[i].itemId == id) {
			Item item = database.items[i];
			addItemAtEmptySlot(item);
		}
		*/

	public void checkIfItemExists(int id, Item item) {
		bool duplicateFound = false;
		int i = 0;
		while (i < Items.Count && !duplicateFound) {
			if(Items[i].itemId == id) {
				++Items[i].numItems;
				duplicateFound = true;
			}

			else {
				++i;
			}

		}

		if (!duplicateFound) {
			addItemAtEmptySlot(item);
		}
	}

	void addItemAtEmptySlot(Item item) {
		
		for (int i = 0; i < Items.Count; i++) {
			if(Items [i].itemName == null) {
				Items[i] = item;
				break;
			}
			
		}
	}

	public void showToolTip(Vector3 toolPosition, Item item) {
		toolTip.SetActive (true);
		toolTip.GetComponent<RectTransform> ().localPosition = new Vector3 (toolPosition.x + TOOL_TIP_VERT_OFFSET_AMT, toolPosition.y, toolPosition.z );

		toolTip.transform.GetChild (0).GetComponent<Text> ().text = item.itemName;			//Display text of tool tip
		toolTip.transform.GetChild (1).GetComponent<Text> ().text = "" + item.numItems;
		toolTip.transform.GetChild (2).GetComponent<Text> ().text = item.itemDesc;
		
		
	}

	public void closeToolTip() {
		toolTip.SetActive (false);
	}

	public void showDraggedItem(Item item, int slotNumber) {
		Debug.Log ("SHOW_DRAGGED_ITEM");
		draggingItem = true;
		draggedItemIndex = slotNumber;
		draggedItemGameObject.SetActive (true);
		draggedItemGameObject.GetComponent<Image> ().sprite = item.itemIcon;
		draggedItem = item;
		closeToolTip ();
	}

	public void disableDraggedItem(){
		Debug.Log ("DISABLE_DRAGGED_ITEM()");
		draggingItem = false;
		draggedItemGameObject.SetActive (false);
	}
	
	/*
	void addItemAtEmptySlot(Item item) {
		Debug.Log ("I'M IN ADD_ITEM_AT_EMPTY_SLOT");
		Debug.Log ("Item description (from Inventory script): " + item.itemDesc);
		Debug.Log ("Items.Count = " + Items.Count);

		for (int i = 0; i < Items.Count; i++) {
			Debug.Log ("i = " + i);
			Debug.Log ("In addItemAtEmptySlot()  " + Items [i] == null);			//The code is only going up to where there have been items entered into the Items list. If it is going to find a spot that's null, it needs to go beyond where the last item is. If an item is extracted from the middle of the list, will the list rearrange to fill this gap? What did PiDi do to code this portion?
			if(Items [i] == null) {
				Debug.Log ("Item description (from inside for-loop in addItemAtEmptySlot() ): " + item.itemDesc);
				Items[i] = item;
				break;
			}

		}
	}
	*/


}


