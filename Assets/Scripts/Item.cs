using UnityEngine;
using System.Collections;

public class Item {
	//Note that consumeables will need to be consumed by right-clicking

	public string itemName;
	public int itemId;
	public string itemDesc;
	public Sprite itemIcon;
	public GameObject itemModel;
	public int itemPower;
	public int itemSpeed;
	public int numItems;
	public ItemType itemType;

	public enum ItemType {
		None,
		Helmet,
		Chest,
		Legs,
		Weapon,
		Consumeable,
		Coin
	}

	public Item(string name, int id, string desc, int pwr, int speed, int value, ItemType type, Sprite icon) {
		itemName = name;
		itemId = id;
		itemDesc = desc;
		itemPower = pwr;
		itemSpeed = speed;
		numItems = value;
		itemType = type;
		itemIcon = icon;												
	}

	public Item() {				//Used for empty slots
		itemType = ItemType.None;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
