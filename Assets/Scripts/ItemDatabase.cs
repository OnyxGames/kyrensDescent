using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{

	public List<Item> items = new List<Item>();
	public Sprite[] icons;

	private const int A_ARMOR_04_SPRITE_INDEX = 185;
	private const int A_ARMOR_05_SPRITE_INDEX = 187;
	private const int I_ANTIDOTE_SPRITE_01_INDEX = 33;
	private const int I_ANTIDOTE_SPRITE_02_INDEX = 34;
	

	// Use this for initialization
	void Start ()
	{
		icons = Resources.LoadAll<Sprite>("Sprites");
		items.Add(new Item("A_Armor04", 0, "Frickin sweet.", 10, 10, 1, Item.ItemType.Chest, icons[A_ARMOR_04_SPRITE_INDEX]));
		items.Add(new Item("A_Armor05", 1, "LUCKY.", 10, 10, 1, Item.ItemType.Chest, icons[A_ARMOR_05_SPRITE_INDEX]));
		items.Add(new Item("I_Antidote", 2, "Go make yourself a dang quesadilla!", 10, 10, 1, Item.ItemType.Consumeable, icons[I_ANTIDOTE_SPRITE_01_INDEX]));
		items.Add (new Item("I_Antidote02", 3, "Tina. Eat the FOOD.", 0, 0, 1, Item.ItemType.Consumeable, icons[I_ANTIDOTE_SPRITE_02_INDEX]));
	}
}

